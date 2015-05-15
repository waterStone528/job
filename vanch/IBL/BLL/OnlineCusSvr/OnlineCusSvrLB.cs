﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Data;

namespace BLL.OnlineCusSvrLB
{
    //负载均衡模式

    public class CusSvr
    {
        private HttpApplicationState application = HttpContext.Current.Application;
        private OpeTable opeTable = new OpeTable();

        /// <summary>
        /// Init cusSvrConditionTable for one customer server if inner user is customer server.
        /// </summary>
        public void InitCusSvr(string cusSvrNum)
        {
            List<string> cusSvrNumList = application["cusSvrNumList"] as List<string>;

            //如果是客服的话，设置客服已经登录
            if (cusSvrNumList.Contains(cusSvrNum))
            {
                application.Lock();
                try
                {
                    //init cusSvrConditionTable for one customer server
                    opeTable.InitCusSvrCondTableOne(application, cusSvrNum);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                    Log.WriteToLog(st.GetFrame(0).ToString(), ex.Message);
                    throw ex;
                    throw ex;
                }
                finally
                {
                    application.UnLock();
                }
            }
        }

        /// <summary>
        /// Customer server send message.
        /// </summary>
        public void SendMsg(string cusSvrNum, string userNum, string msg)
        {
            application.Lock();

            try
            {
                //update communication table
                if (opeTable.CusSvrUpdateCommTable(application, cusSvrNum, userNum, msg) == false)
                {
                    return;
                }

                // Add record to chat record table
                opeTable.AddRecordToCRTable(application, cusSvrNum, userNum, msg);

            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                Log.WriteToLog(st.GetFrame(0).ToString(), ex.Message);

                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }

        /// <summary>
        /// Customer server receive message.
        /// </summary>
        public string ReceiveMsg(string cusSvrNum)
        {
            application.Lock();
            try
            {
                //mark customer server is not disconnected
                DataRow dr = opeTable.MarkCusSvrConnected(application, cusSvrNum);

                DataTable dt = application["commMaintainTable"] as DataTable;

                var linq = from c in dt.AsEnumerable()
                            where c.Field<string>("cusSvrNum") == cusSvrNum
                            select c;

                //maintain user
                opeTable.MaintainUser(linq, dt, dr);

                //return message
                return opeTable.CusSvrGetMsg(linq);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                Log.WriteToLog(st.GetFrame(0).ToString(), ex.Message);

                throw ex;

                //return string.Format("{{'error':{0}}}",ex.ToString());
            }
            finally
            {
                application.UnLock();
            }
        }
    }

    public class User
    {
        private HttpApplicationState application = HttpContext.Current.Application;
        private OpeTable opeTable = new OpeTable();

        //用户是否建立连接成功（即是否有可分配的客服）
        public string IsConnected(string userNum)
        {
            application.Lock();

            try
            {
                //get user phone by user number
                DAL.Client.Client client = new DAL.Client.Client();
                string userName = client.GetClientName(userNum);

                //客服分配负载均衡
                string assignRes = opeTable.LoadBalancing(application);
                if (assignRes == "lbDisconnected")
                {
                    return string.Format("{{\"isTimeIdentityRight\":\"true\",\"userName\":\"{0}\",\"isConnected\":\"lbDisconnected\"}}", userName);
                }

                if (assignRes == "false")
                {
                    return string.Format("{{\"isTimeIdentityRight\":\"true\",\"userName\":\"{0}\",\"isConnected\":\"false\"}}", userName);
                }
                string cusSvrNum = assignRes;

                //向通信维护表中添加新的纪录
                opeTable.AddNewRecordToCommTable(application, userNum, cusSvrNum);

                return string.Format("{{\"isTimeIdentityRight\":\"true\",\"userName\":\"{0}\",\"isConnected\":\"true\",\"cusSvrNum\":\"{1}\"}}", userName, cusSvrNum);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                Log.WriteToLog(st.GetFrame(0).ToString(), ex.Message);
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }

        /// <summary>
        /// 客户发送消息
        /// </summary>
        public void SendMsg(string userNum, string msg)
        {
            //string userNum = HttpContext.Current.Session["userNum"].ToString();

            application.Lock();
            try
            {
                //update communication table
                DataRow drComm = opeTable.UserUpdateCommTable(application, userNum, msg);
                if (drComm == null)
                {
                    return;
                }

                //为了标记客户是否会话超时
                drComm["commLevel"] = 0;

                //add record to chat record table
                opeTable.AddRecordToCRTable(application, drComm["cusSvrNum"].ToString(), userNum, msg);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                Log.WriteToLog(st.GetFrame(0).ToString(), ex.Message);
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }

        /// <summary>
        /// 用户接收消息
        /// </summary>
        /// <remarks>&* 为无效符号</remarks>
        public string ReceiveMsg(string userNum)
        {
            application.Lock();

            try
            {
                //check user record is existed in communication table
                DataRow drUseless = opeTable.CheckUserRecordExistInCommTable(application, userNum);
                if (drUseless == null)
                {
                    return "&*";
                }

                //为了标记用户是否已经断线
                drUseless["connLevel"] = 0;

                //User get and delete record from communication table
                return opeTable.UserGetAndDelRecord(application, userNum);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                Log.WriteToLog(st.GetFrame(0).ToString(), ex.Message);

                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }
    }

    public class OpeTable
    {
        private int maxCusSvrConnLevel = Convert.ToInt32(HttpContext.Current.Session["maxCusSvrConnLevel"]);
        private int cusSvrUserMaxAmount = Convert.ToInt32(HttpContext.Current.Session["cusSvrUserMaxAmount"]);
        private int maxUserConnNum = Convert.ToInt32(HttpContext.Current.Session["maxUserConnNum"]);

        /// <summary>分配客服负载均衡
        ///     先：当前服务客户数最少的客服
        ///     后：随机客服
        ///     <returns> string
        ///         1、"false":没有空闲客服，客服分配失败。
        ///         2、cusSvrNum:客服分配成功，返回客服工号。
        ///     </returns>
        /// </summary>
        public string LoadBalancing(HttpApplicationState application)
        {
            DataTable dt = application["cusSvrConditionTable"] as DataTable;

            var linqDate1 = dt.AsEnumerable().Where(c => c.Field<int>("connLevel") < maxCusSvrConnLevel);
            //all customer server is disconnected
            if(linqDate1.Count() == 0)
            {
                return "lbDisconnected";
            }

            var linqData2 = dt.AsEnumerable().Where(c => c.Field<int>("userAmount") < cusSvrUserMaxAmount);

            //没有空闲客服
            if (linqData2.Count() == 0)
            {
                return "false";
            }

            //HttpContext.Current.Session["userNum"] = userNum;

            int minUserAmount = Convert.ToInt32(linqData2.Min(c => c.Field<int>("userAmount")));
            DataRow cusSvrDr = dt.AsEnumerable().Where(c => c.Field<int>("userAmount") == minUserAmount).OrderBy(c => Guid.NewGuid()).FirstOrDefault() as DataRow;
            cusSvrDr["userAmount"] = Convert.ToInt32(cusSvrDr["userAmount"]) + 1;

            return cusSvrDr["cusSvrNum"].ToString();
        }

        /// <summary>
        /// 向通信维护表中添加新的纪录
        /// </summary>
        public void AddNewRecordToCommTable(HttpApplicationState application, string userNum, string cusSvrNum)
        {
            DataTable dtCommunication = application["commMaintainTable"] as DataTable;
            DataRow dr = dtCommunication.NewRow();
            dr["userNum"] = userNum;
            dr["cusSvrNum"] = cusSvrNum;
            dr["userSendMsg"] = null;
            dr["cusSvrSendMsg"] = null;
            dr["commLevel"] = 0;
            dr["connLevel"] = 0;
            dtCommunication.Rows.Add(dr);
        }

        /// <summary>
        /// User update communication table.
        /// </summary>
        public DataRow UserUpdateCommTable(HttpApplicationState application,string userNum,string msg)
        {
            DataTable dtComm = application["commMaintainTable"] as DataTable;
            DataRow drComm = dtComm.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum).FirstOrDefault();
            if (drComm == null)
            {
                return null;
            }
            if (drComm["userSendMsg"] == DBNull.Value)
            {
                drComm["userSendMsg"] = msg;
            }
            else
            {
                drComm["userSendMsg"] = string.Format("{0}&&{1}", drComm["userSendMsg"], msg);
            }

            return drComm;
        }

        /// <summary>
        /// Add record to chat record table.
        /// </summary>
        public void AddRecordToCRTable(HttpApplicationState application,string cusSvrNum,string userNum,string msg)
        {
            DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
            DataRow drChatRecords = dtChatRecords.NewRow();
            drChatRecords["userNum"] = userNum;
            drChatRecords["cusSvrNum"] = cusSvrNum;
            drChatRecords["userSendMsg"] = msg;
            drChatRecords["cusSvrSendMsg"] = null;
            drChatRecords["dateTime"] = DateTime.Now;
            dtChatRecords.Rows.Add(drChatRecords);
        }

        /// <summary>
        /// Check user record is existed in communication table
        /// </summary>
        public DataRow CheckUserRecordExistInCommTable(HttpApplicationState application,string userNum)
        {
            DataTable dt = application["commMaintainTable"] as DataTable;
            DataRow drUseless = dt.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum).FirstOrDefault();
            return drUseless;
        }

        /// <summary>
        /// User get and delete record from communication table.
        /// </summary>
        public string UserGetAndDelRecord(HttpApplicationState application,string userNum)
        {
            DataTable dt = application["commMaintainTable"] as DataTable;
            DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum && c.Field<string>("cusSvrSendMsg") != null).FirstOrDefault();
            if (dr != null)
            {
                string msg = dr["cusSvrSendMsg"].ToString();
                dr["cusSvrSendMsg"] = null;

                return msg;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// init cusSvrConditionTable for one customer server
        /// </summary>
        public void InitCusSvrCondTableOne(HttpApplicationState application,string cusSvrNum)
        {
            DataTable dt = application["cusSvrConditionTable"] as DataTable;
            DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum).First() as DataRow;
            dr["userAmount"] = 0;
            dr["connLevel"] = 0;
        }

        /// <summary>
        /// Customer server update communication table.
        /// </summary>
        public bool CusSvrUpdateCommTable(HttpApplicationState application,string cusSvrNum,string userNum,string msg)
        {
            DataTable dtComm = application["commMaintainTable"] as DataTable;
            DataRow drComm = dtComm.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum && c.Field<string>("userNum") == userNum).FirstOrDefault();
            if (drComm == null)
            {
                return false;
            }
            if (drComm["cusSvrSendMsg"] == DBNull.Value)
            {
                drComm["cusSvrSendMsg"] = msg;
            }
            else
            {
                drComm["cusSvrSendMsg"] = string.Format("{0}&&{1}", drComm["cusSvrSendMsg"], msg);
            }

            return true;
        }
        
        /// <summary>
        /// Mark customer server is connected.
        /// </summary>
        public DataRow MarkCusSvrConnected(HttpApplicationState application,string cusSvrNum)
        {
            DataTable dt = application["cusSvrConditionTable"] as DataTable;
            DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum).First() as DataRow;
            //mark 
            dr["connLevel"] = 0;

            return dr;
        }

        /// <summary>
        /// Maintain user.
        /// </summary>
        public void MaintainUser(System.Data.EnumerableRowCollection<System.Data.DataRow> linq,DataTable dt, DataRow drCusSvrCond)
        {
            foreach (var dr in linq)
            {
                //删除通信维护表中的已经断线的用户
                //客服维护表中的相应客服的客服维护客户数减1
                if (Convert.ToInt32(dr["connLevel"]) == maxUserConnNum)
                {
                    dr.Delete();
                    drCusSvrCond["userAmount"] = Convert.ToInt32(drCusSvrCond["userAmount"]) - 1;
                    continue;
                }

                dr["connLevel"] = Convert.ToInt32(dr["connLevel"]) + 1;
            }

            dt.AcceptChanges();
        }

        /// <summary>
        /// Customer server get message.
        /// </summary>
        public string CusSvrGetMsg(System.Data.EnumerableRowCollection<System.Data.DataRow> linq)
        {
            var list = (from c in linq
                        where c.Field<int>("connLevel") < maxUserConnNum
                        select new
                        {
                            userNum = c["userNum"],
                            msg = c["userSendMsg"]
                        }).ToList();

            foreach (var dr in linq)
            {
                dr["userSendMsg"] = null;
            }

            return Helper.Serialize(list);
        }
    }

    public class Log
    {
        //发生错误时，写入日志中
        public static void WriteToLog(string funcName, string errorInfo)
        {
            HttpApplicationState application = HttpContext.Current.Application;

            application.Lock();
            DataTable dt = null;
            string dtContent = string.Empty;
            dtContent += string.Format("######################################{0}#################################\r\n", DateTime.Now);
            dtContent += string.Format("error in {0}\r\n", funcName);
            dtContent += string.Format("error:{0}\r\n", errorInfo);
            dtContent += "start print cusSvrConditionTable\r\n";
            dt = application["cusSvrConditionTable"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
                }

                dtContent += "\r\n";
            }

            dtContent += "\r\n\r\n";

            dtContent += "start print commMaintainTable\r\n";
            dt = application["commMaintainTable"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
                }

                dtContent += "\r\n";
            }

            dtContent += "\r\n\r\n\r\n\r\n\r\n";

            Helper.WriteLog(dtContent);

            application.UnLock();
        }

        //查看客服维护表和通信维护表
        public static void WriteToFile()
        {
            HttpApplicationState application = HttpContext.Current.Application;

            application.Lock();
            DataTable dt = null;
            string dtContent = string.Empty;
            dtContent += string.Format("######################################{0}#################################\r\n", DateTime.Now);
            dtContent += "start print cusSvrConditionTable\r\n";
            dt = application["cusSvrConditionTable"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
                }

                dtContent += "\r\n";
            }

            dtContent += "\r\n\r\n";

            dtContent += "start print commMaintainTable\r\n";
            dt = application["commMaintainTable"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
                }

                dtContent += "\r\n";
            }

            dtContent += "\r\n\r\n\r\n\r\n\r\n";

            Helper.WriteToFile(@"c:\debug\showCusSvrTable.txt", dtContent);

            application.UnLock();
        }
    }
}
