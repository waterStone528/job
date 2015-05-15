using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Data;
using System.Web.SessionState;

namespace BLL.OnlineCusSvrBak
{
    ////在线客服
    //public class OnlineCusSvr : IHttpHandler, IRequiresSessionState
    //{
    //    #region IHttpHandler Members

    //    public bool IsReusable
    //    {
    //        // Return false in case your Managed Handler cannot be reused for another request.
    //        // Usually this would be false in case you have some state information preserved per request.
    //        get { return true; }
    //    }

    //    public void ProcessRequest(HttpContext context)
    //    {
    //        //write your handler implementation here.
    //    }

    //    #endregion

    //    private HttpApplicationState application = HttpContext.Current.Application;
    //    private int cusSvrUserMaxAmount = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["cusSvrUserMaxAmount"]); 
    //    private int uselessLevel = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["cusSvrUserUselessLevel"]);
    //    private int maxUserConnLevel = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["maxUserConnLevel"]);

    //    //用户是否建立连接成功（即是否有可分配的客服）
    //    public string IsConnected(string userNum)
    //    {
    //        application.Lock();

    //        try
    //        {
    //            DataTable dt = application["cusSvrConditionTable"] as DataTable;

    //            var linqData1 = dt.AsEnumerable().Where(c => c.Field<int>("userAmount") < cusSvrUserMaxAmount);

    //            //客服没有空闲
    //            if (linqData1.Count() == 0)
    //            {
    //                return "{'isConnected':'false'}";
    //            }

    //            //HttpContext.Current.Session["userNum"] = userNum;

    //            int minUserAmount = Convert.ToInt32(linqData1.Min(c => c.Field<int>("userAmount")));

    //            DataRow cusSvrDr = dt.AsEnumerable().Where(c => c.Field<int>("userAmount") == minUserAmount).OrderBy(c => Guid.NewGuid()).FirstOrDefault() as DataRow;

    //            cusSvrDr["userAmount"] = Convert.ToInt32(cusSvrDr["userAmount"]) + 1;

    //            //向通信维护表中添加新的纪录
    //            DataTable dtCommunication = application["commMaintainTable"] as DataTable;
    //            DataRow dr = dtCommunication.NewRow();
    //            dr["userNum"] = userNum;
    //            dr["cusSvrNum"] = cusSvrDr["cusSvrNum"];
    //            dr["userSendMsg"] = null;
    //            dr["cusSvrSendMsg"] = null;
    //            dr["commLevel"] = 0;
    //            dr["connLevel"] = 0;
    //            dtCommunication.Rows.Add(dr);

    //            return string.Format("{{'isConnected':'true','cusSvrNum':'{0}'}}", cusSvrDr["cusSvrNum"].ToString());
    //        }
    //        catch(Exception ex)
    //        {
    //            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
    //            WriteToLog(st.GetFrame(0).ToString(), ex.Message);
    //            throw ex;
    //        }
    //        finally
    //        {
    //            application.UnLock();
    //        }
    //    }

    //    ///<summary>客服初始化
    //    ///1、设置客服状态表的用户服务数为0
    //    ///2、设置客服状态表的通信level为0
    //    /// </summary>
    //    public void InitCusSvr(string cusSvrNum)
    //    {
    //        List<string> cusSvrNumList = application["cusSvrNumList"] as List<string>;

    //        //如果是客服的话，设置客服已经登录
    //        if (cusSvrNumList.Contains(cusSvrNum))
    //        {
    //            application.Lock();
    //            try
    //            {
    //                DataTable dt = application["cusSvrConditionTable"] as DataTable;
    //                DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum).First() as DataRow;
    //                dr["userAmount"] = 0;
    //                dr["connLevel"] = 0;
    //            }
    //            catch(Exception ex)
    //            {
    //                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
    //                WriteToLog(st.GetFrame(0).ToString(), ex.Message);
    //                throw ex;
    //            }
    //            finally
    //            {
    //                application.UnLock();
    //            }
    //        }
    //    }

    //    ///<summary>
    //    ///用户发送消息
    //    /// </summary>
    //    public void UserSendMsg(string userNum,string msg)
    //    {
    //        //string userNum = HttpContext.Current.Session["userNum"].ToString();

    //        application.Lock();
    //        try
    //        {
    //            //更新通信表
    //            DataTable dtComm = application["commMaintainTable"] as DataTable;
    //            DataRow drComm = dtComm.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum).FirstOrDefault();
    //            if (drComm == null)
    //            {
    //                return;
    //            }
    //            if (drComm["userSendMsg"] == DBNull.Value)
    //            {
    //                drComm["userSendMsg"] = msg;
    //            }
    //            else
    //            {
    //                drComm["userSendMsg"] = string.Format("{0}&&{1}", drComm["userSendMsg"], msg);
    //            }

    //            //为了标记客户是否会话超时
    //            drComm["commLevel"] = 0;

    //            string cusSvrNum = drComm["cusSvrNum"].ToString();

    //            //更新聊天记录表
    //            DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
    //            DataRow drChatRecords = dtChatRecords.NewRow();
    //            drChatRecords["userNum"] = userNum;
    //            drChatRecords["cusSvrNum"] = cusSvrNum;
    //            drChatRecords["userSendMsg"] = msg;
    //            drChatRecords["cusSvrSendMsg"] = null;
    //            drChatRecords["dateTime"] = DateTime.Now;
    //            dtChatRecords.Rows.Add(drChatRecords);
    //        }
    //        catch(Exception ex)
    //        {
    //            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
    //            WriteToLog(st.GetFrame(0).ToString(), ex.Message);
    //            throw ex;
    //        }
    //        finally
    //        {
    //            application.UnLock();
    //        }

    //    }

    //    /// <summary>
    //    /// 客服发送消息
    //    /// </summary>
    //    /// <param name="cusSvrNum"></param>
    //    /// <param name="userNum"></param>
    //    /// <param name="msg"></param>
    //    public void CusSvrSendMsg(string cusSvrNum,string userNum,string msg)
    //    {
    //        try
    //        {
    //            //更新通信表
    //            DataTable dtComm = application["commMaintainTable"] as DataTable;
    //            DataRow drComm = dtComm.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum && c.Field<string>("userNum") == userNum).FirstOrDefault();
    //            if (drComm == null)
    //            {
    //                return;
    //            }
    //            if (drComm["cusSvrSendMsg"] == DBNull.Value)
    //            {
    //                drComm["cusSvrSendMsg"] = msg;
    //            }
    //            else
    //            {
    //                drComm["cusSvrSendMsg"] = string.Format("{0}&&{1}", drComm["cusSvrSendMsg"], msg);
    //            }

    //            //更新聊天记录表
    //            DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
    //            DataRow drChatRecords = dtChatRecords.NewRow();
    //            drChatRecords["userNum"] = userNum;
    //            drChatRecords["cusSvrNum"] = cusSvrNum;
    //            drChatRecords["userSendMsg"] = null;
    //            drChatRecords["cusSvrSendMsg"] = msg;
    //            drChatRecords["dateTime"] = DateTime.Now;
    //            dtChatRecords.Rows.Add(drChatRecords);
    //        }
    //        catch(Exception ex)
    //        {
    //            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
    //            WriteToLog(st.GetFrame(0).ToString(), ex.Message);

    //            throw ex;
    //        }
    //    }

    //    /// <summary>
    //    /// 用户接收消息
    //    /// </summary>
    //    /// <remarks>&* 为无效符号</remarks>
    //    public string UserReceiveMsg(string userNum)
    //    {
    //        application.Lock();

    //        try
    //        {
    //            //更新通信表
    //            DataTable dt = application["commMaintainTable"] as DataTable;
    //            DataRow drUseless = dt.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum).FirstOrDefault();
    //            if (drUseless == null)
    //            {
    //                return "&*";
    //            }
    //            else
    //            {
    //                //为了标记用户是否已经断线
    //                drUseless["connLevel"] = 0;
    //            }
    //            DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum && c.Field<string>("cusSvrSendMsg") != null).FirstOrDefault();
    //            if (dr != null)
    //            {
    //                string msg = dr["cusSvrSendMsg"].ToString();
    //                dr["cusSvrSendMsg"] = null;

    //                return msg;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        catch(Exception ex)
    //        {
    //            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
    //            WriteToLog(st.GetFrame(0).ToString(), ex.Message);

    //            throw ex;
    //        }
    //        finally
    //        {
    //            application.UnLock(); 
    //        }
    //    }

    //    /// <summary>
    //    /// 客服接收消息
    //    /// </summary>
    //    public string CusSvrReceiveMsg(string cusSvrNum)
    //    {
    //        application.Lock();
    //        try
    //        {
    //            DataTable dtCusSvrCondition = application["cusSvrConditionTable"] as DataTable;
    //            DataRow drCusSvrCondition = dtCusSvrCondition.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum).First() as DataRow;
    //            //为了标记客服是否已经断线
    //            drCusSvrCondition["connLevel"] = 0;

    //            //更新通信表
    //            DataTable dtCommMaintainTable = application["commMaintainTable"] as DataTable;

    //            var linq = from c in dtCommMaintainTable.AsEnumerable()
    //                       where c.Field<string>("cusSvrNum") == cusSvrNum
    //                       select c;

    //            //维护断线客户
    //            foreach (var dr in linq)
    //            {
    //                //删除通信维护表中的已经断线的用户
    //                //客服维护表中的相应客服的客服维护客户数减1
    //                if (Convert.ToInt32(dr["connLevel"]) == maxUserConnLevel)
    //                {
    //                    dr.Delete();
    //                    drCusSvrCondition["userAmount"] = Convert.ToInt32(drCusSvrCondition["userAmount"]) - 1;
    //                    continue;
    //                }

    //                dr["connLevel"] = Convert.ToInt32(dr["connLevel"]) + 1;
    //            }

    //            dtCommMaintainTable.AcceptChanges();

    //            var list = (from c in linq
    //                        where c.Field<int>("connLevel") < maxUserConnLevel
    //                        select new
    //                        {
    //                            userNum = c["userNum"],
    //                            msg = c["userSendMsg"]
    //                        }).ToList();

    //            foreach (var dr in linq)
    //            {
    //                dr["userSendMsg"] = null;
    //            }

    //            return Helper.Serialize(list);
    //        }
    //        catch (Exception ex)
    //        {
    //            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
    //            WriteToLog(st.GetFrame(0).ToString(),ex.Message);

    //            throw ex;

    //            //return string.Format("{{'error':{0}}}",ex.ToString());
    //        }
    //        finally
    //        {
    //            application.UnLock();
    //        }
    //    }

    //    //发生错误时，写入日志中
    //    private void WriteToLog(string funcName,string errorInfo)
    //    {
    //        application.Lock();
    //        DataTable dt = null;
    //        string dtContent = string.Empty;
    //        dtContent += string.Format("######################################{0}#################################\r\n", DateTime.Now);
    //        dtContent += string.Format("error in {0}\r\n", funcName);
    //        dtContent += string.Format("error:{0}\r\n", errorInfo);
    //        dtContent += "start print cusSvrConditionTable\r\n";
    //        dt = application["cusSvrConditionTable"] as DataTable;
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            for (int i = 0; i < dt.Columns.Count; i++)
    //            {
    //                dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
    //            }

    //            dtContent += "\r\n";
    //        }

    //        dtContent += "\r\n\r\n";

    //        dtContent += "start print commMaintainTable\r\n";
    //        dt = application["commMaintainTable"] as DataTable;
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            for (int i = 0; i < dt.Columns.Count; i++)
    //            {
    //                dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
    //            }

    //            dtContent += "\r\n";
    //        }

    //        dtContent += "\r\n\r\n\r\n\r\n\r\n";

    //        Helper.WriteLog(dtContent);

    //        application.UnLock();
    //    }

    //    //查看客服维护表和通信维护表
    //    public void WriteToFile()
    //    {
    //        application.Lock();
    //        DataTable dt = null;
    //        string dtContent = string.Empty;
    //        dtContent += string.Format("######################################{0}#################################\r\n", DateTime.Now);
    //        dtContent += "start print cusSvrConditionTable\r\n";
    //        dt = application["cusSvrConditionTable"] as DataTable;
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            for (int i = 0; i < dt.Columns.Count; i++)
    //            {
    //                dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
    //            }

    //            dtContent += "\r\n";
    //        }

    //        dtContent += "\r\n\r\n";

    //        dtContent += "start print commMaintainTable\r\n";
    //        dt = application["commMaintainTable"] as DataTable;
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            for (int i = 0; i < dt.Columns.Count; i++)
    //            {
    //                dtContent += string.Format("{0} : {1}      ", dt.Columns[i].ColumnName, dr[i]);
    //            }

    //            dtContent += "\r\n";
    //        }

    //        dtContent += "\r\n\r\n\r\n\r\n\r\n";

    //        Helper.WriteToFile(@"c:\debug\showCusSvrTable.txt", dtContent);

    //        application.UnLock();
    //    }
    //}



    

    //保存聊天记录

    //在线客服

    public class OnlineCusSvr
    {
        private HttpApplicationState application = HttpContext.Current.Application;
        private int cusSvrUserMaxAmount = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["cusSvrUserMaxAmount"]);
        private int uselessLevel = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["cusSvrUserUselessLevel"]);
        private int maxUserConnLevel = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["maxUserConnLevel"]);

        //用户是否建立连接成功（即是否有可分配的客服）
        public string IsConnected(string userNum)
        {
            application.Lock();

            try
            {
                DataTable dt = application["cusSvrConditionTable"] as DataTable;

                #region 客服负载均衡，规则为当前服务人数最少的客服
                //var linqData1 = dt.AsEnumerable().Where(c => c.Field<int>("userAmount") < cusSvrUserMaxAmount);

                ////客服没有空闲
                //if (linqData1.Count() == 0)
                //{
                //    return "{'isConnected':'false'}";
                //}

                ////HttpContext.Current.Session["userNum"] = userNum;

                //int minUserAmount = Convert.ToInt32(linqData1.Min(c => c.Field<int>("userAmount")));

                //DataRow cusSvrDr = dt.AsEnumerable().Where(c => c.Field<int>("userAmount") == minUserAmount).OrderBy(c => Guid.NewGuid()).FirstOrDefault() as DataRow;
                #endregion

                #region 为客户分配自己的客户经理，在线客服中，每个客户经理服务的客户人数不限
                //模拟数据库查找客户自己的客户经理
                string cusSvrNum = (Convert.ToInt32(userNum) % 2 == 1) ? "001" : "002";

                DataRow cusSvrDr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum && c.Field<int>("userAmount") != cusSvrUserMaxAmount).FirstOrDefault();

                if(cusSvrDr == null)
                {
                    return "{'isConnected':'false'}";
                }
                
                #endregion

                cusSvrDr["userAmount"] = Convert.ToInt32(cusSvrDr["userAmount"]) + 1;

                //向通信维护表中添加新的纪录
                DataTable dtCommunication = application["commMaintainTable"] as DataTable;
                DataRow dr = dtCommunication.NewRow();
                dr["userNum"] = userNum;
                dr["cusSvrNum"] = cusSvrDr["cusSvrNum"];
                dr["userSendMsg"] = null;
                dr["cusSvrSendMsg"] = null;
                dr["commLevel"] = 0;
                dr["connLevel"] = 0;
                dtCommunication.Rows.Add(dr);

                return string.Format("{{'isConnected':'true','cusSvrNum':'{0}'}}", cusSvrDr["cusSvrNum"].ToString());
            }
            catch (Exception ex) 
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                WriteToLog(st.GetFrame(0).ToString(), ex.Message);
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }

        ///<summary>客服初始化
        ///1、设置客服状态表的用户服务数为0
        ///2、设置客服状态表的通信level为0
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
                    DataTable dt = application["cusSvrConditionTable"] as DataTable;
                    DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum).First() as DataRow;
                    dr["userAmount"] = 0;
                    dr["connLevel"] = 0;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                    WriteToLog(st.GetFrame(0).ToString(), ex.Message);
                    throw ex;
                }
                finally
                {
                    application.UnLock();
                }
            }
        }

        ///<summary>
        ///用户发送消息
        /// </summary>
        public void UserSendMsg(string userNum, string msg)
        {
            //string userNum = HttpContext.Current.Session["userNum"].ToString();

            application.Lock();
            try
            {
                //更新通信表
                DataTable dtComm = application["commMaintainTable"] as DataTable;
                DataRow drComm = dtComm.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum).FirstOrDefault();
                if (drComm == null)
                {
                    return;
                }
                if (drComm["userSendMsg"] == DBNull.Value)
                {
                    drComm["userSendMsg"] = msg;
                }
                else
                {
                    drComm["userSendMsg"] = string.Format("{0}&&{1}", drComm["userSendMsg"], msg);
                }

                //为了标记客户是否会话超时
                drComm["commLevel"] = 0;

                string cusSvrNum = drComm["cusSvrNum"].ToString();

                //更新聊天记录表
                DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
                DataRow drChatRecords = dtChatRecords.NewRow();
                drChatRecords["userNum"] = userNum;
                drChatRecords["cusSvrNum"] = cusSvrNum;
                drChatRecords["userSendMsg"] = msg;
                drChatRecords["cusSvrSendMsg"] = null;
                drChatRecords["dateTime"] = DateTime.Now;
                dtChatRecords.Rows.Add(drChatRecords);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                WriteToLog(st.GetFrame(0).ToString(), ex.Message);
                throw ex;
            }
            finally
            {
                application.UnLock();
            }

        }

        /// <summary>
        /// 客服发送消息
        /// </summary>
        public void CusSvrSendMsg(string cusSvrNum, string userNum, string msg)
        {
            application.Lock();

            try
            {
                //更新通信表
                DataTable dtComm = application["commMaintainTable"] as DataTable;
                DataRow drComm = dtComm.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum && c.Field<string>("userNum") == userNum).FirstOrDefault();
                if (drComm == null)
                {
                    return;
                }
                if (drComm["cusSvrSendMsg"] == DBNull.Value)
                {
                    drComm["cusSvrSendMsg"] = msg;
                }
                else
                {
                    drComm["cusSvrSendMsg"] = string.Format("{0}&&{1}", drComm["cusSvrSendMsg"], msg);
                }

                //更新聊天记录表
                DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
                DataRow drChatRecords = dtChatRecords.NewRow();
                drChatRecords["userNum"] = userNum;
                drChatRecords["cusSvrNum"] = cusSvrNum;
                drChatRecords["userSendMsg"] = null;
                drChatRecords["cusSvrSendMsg"] = msg;
                drChatRecords["dateTime"] = DateTime.Now;
                dtChatRecords.Rows.Add(drChatRecords);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                WriteToLog(st.GetFrame(0).ToString(), ex.Message);

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
        public string UserReceiveMsg(string userNum)
        {
            application.Lock();

            try
            {
                //更新通信表
                DataTable dt = application["commMaintainTable"] as DataTable;
                DataRow drUseless = dt.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum).FirstOrDefault();
                if (drUseless == null)
                {
                    return "&*";
                }
                else
                {
                    //为了标记用户是否已经断线
                    drUseless["connLevel"] = 0;
                }
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
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                WriteToLog(st.GetFrame(0).ToString(), ex.Message);

                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }

        /// <summary>
        /// 客服接收消息
        /// </summary>
        public string CusSvrReceiveMsg(string cusSvrNum)
        {
            application.Lock();
            try
            {
                DataTable dtCusSvrCondition = application["cusSvrConditionTable"] as DataTable;
                DataRow drCusSvrCondition = dtCusSvrCondition.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == cusSvrNum).First() as DataRow;
                //为了标记客服是否已经断线
                drCusSvrCondition["connLevel"] = 0;

                //更新通信表
                DataTable dtCommMaintainTable = application["commMaintainTable"] as DataTable;

                var linq = from c in dtCommMaintainTable.AsEnumerable()
                           where c.Field<string>("cusSvrNum") == cusSvrNum
                           select c;

                //维护断线客户
                foreach (var dr in linq)
                {
                    //删除通信维护表中的已经断线的用户
                    //客服维护表中的相应客服的客服维护客户数减1
                    if (Convert.ToInt32(dr["connLevel"]) == maxUserConnLevel)
                    {
                        dr.Delete();
                        drCusSvrCondition["userAmount"] = Convert.ToInt32(drCusSvrCondition["userAmount"]) - 1;
                        continue;
                    }

                    dr["connLevel"] = Convert.ToInt32(dr["connLevel"]) + 1;
                }

                dtCommMaintainTable.AcceptChanges();

                var list = (from c in linq
                            where c.Field<int>("connLevel") < maxUserConnLevel
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
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                WriteToLog(st.GetFrame(0).ToString(), ex.Message);

                throw ex;

                //return string.Format("{{'error':{0}}}",ex.ToString());
            }
            finally
            {
                application.UnLock();
            }
        }

        //发生错误时，写入日志中
        private void WriteToLog(string funcName, string errorInfo)
        {
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
        public void WriteToFile()
        {
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

    public class ChatRecords
    {
        private static DAL.OnlineCusSvr.ChatRecords dal = new DAL.OnlineCusSvr.ChatRecords();

        //保存聊天记录
        public void SaveChatRecords(DataTable dt)
        {
            dal.SaveChatRecords(dt);

            dt.Rows.Clear();
        }
    }
}
