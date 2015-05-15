using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Data;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Web.Script.Serialization;

namespace VanchBg
{
    public class Global : System.Web.HttpApplication
    {
        private Timer cusSvrAssisterTimer;
        

        protected void Application_Start(object sender, EventArgs e)
        {
            //get online customer server config data
            BLL.Config.OnlineCusSvrConfig bll = new BLL.Config.OnlineCusSvrConfig();
            Dictionary<string, int> configData = bll.GetConfigDataG();

            BLL.OnlineCusSvr.CusSvrGlobal bllG = new BLL.OnlineCusSvr.CusSvrGlobal();
            List<string> cusSvrNumList = bllG.GetCusSvrWorkNum();

            CusSvrInit cusSvrInit = new CusSvrInit(configData);
            Application["cusSvrNumList"] = cusSvrNumList;
            Application["cusSvrConditionTable"] = cusSvrInit.CusSvrConditionTableInit(cusSvrNumList);
            Application["commMaintainTable"] = cusSvrInit.CommMaintainTableInit();
            Application["chatRecordsTable"] = cusSvrInit.ChatRecordsTableInit();
            cusSvrAssisterTimer = new Timer(new TimerCallback(cusSvrInit.StartAssisterThreading), Application, 0, configData["levelSeconds"] * 1000);

            //if (configData["cusSvrMode"] == 0)
            //{
            //    CusSvrInitLB cusSvrInit = new CusSvrInitLB(configData);
            //    Application["cusSvrNumList"] = cusSvrNumList;
            //    Application["cusSvrConditionTable"] = cusSvrInit.CusSvrConditionTableInit(cusSvrNumList);
            //    Application["commMaintainTable"] = cusSvrInit.CommMaintainTableInit();
            //    Application["chatRecordsTable"] = cusSvrInit.ChatRecordsTableInit();
            //    cusSvrAssisterTimer = new Timer(new TimerCallback(cusSvrInit.StartAssisterThreading), Application, 0, configData["levelSeconds"] * 1000);
            //}
            //else
            //{
            //    CusSvrInitCM cusSvrInit = new CusSvrInitCM(configData);
            //    Application["cusSvrNumList"] = cusSvrNumList;
            //    Application["cusSvrConditionTable"] = cusSvrInit.CusSvrConditionTableInit(cusSvrNumList);
            //    Application["commMaintainTable"] = cusSvrInit.CommMaintainTableInit();
            //    Application["chatRecordsTable"] = cusSvrInit.ChatRecordsTableInit();
            //    cusSvrAssisterTimer = new Timer(new TimerCallback(cusSvrInit.StartAssisterThreading), Application, 0, configData["levelSeconds"] * 1000);
            //}
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //FileStream fs = null;
            //StreamWriter sw = null;
            //string filePath = string.Format(@"c:\debug\{0}.txt", System.DateTime.Now.ToShortDateString());
            //Exception objErr = Server.GetLastError().GetBaseException();
            //string errorTime = string.Format("--------------------------------{0}------------------------------", System.DateTime.Now.ToString());
            //string errorPage = string.Format("发生异常页:{0}", Request.Url.ToString());
            //string errorMessage = string.Format("错误信息:{0}", objErr.Message);
            //string errorSource = string.Format("错误源:{0}", objErr.Source);
            //string errorTrace = string.Format("堆栈信息:{0}", objErr.StackTrace);

            //try
            //{
            //    lock (this)
            //    {
            //        fs = new FileStream(filePath, FileMode.Append);
            //        sw = new StreamWriter(fs);

            //        sw.WriteLine(errorTime);
            //        sw.WriteLine(errorPage);
            //        sw.WriteLine(errorMessage);
            //        sw.WriteLine(errorSource);
            //        sw.WriteLine(errorTrace);
            //        sw.WriteLine("");
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
            //finally
            //{
            //    if (sw != null)
            //    {
            //        sw.Close();
            //    }

            //    if (fs != null)
            //    {
            //        fs.Close();
            //    }
            //}
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }

    public class CusSvrInitLB :IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //write your handler implementation here.
        }

        #endregion
        private int cusSvrUserMaxAmount;
        private int delLeval;
        private int maxCusSvrConnLevel;

        public CusSvrInitLB(Dictionary<string,int> configData)
        {
            cusSvrUserMaxAmount = configData["cusSvrUserMaxAmount"];
            delLeval = configData["cusSvrUserDelLevel"];
            maxCusSvrConnLevel = configData["maxCusSvrConnLevel"];
        }

        public DataTable CusSvrConditionTableInit(List<string> cusSvrNumList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userAmount", typeof(Int32));
            dt.Columns.Add("connLevel", typeof(int));              //标记客服是否断线

            foreach (string cusSvrNum in cusSvrNumList)
            {
                DataRow dr = dt.NewRow();
                dr["cusSvrNum"] = cusSvrNum;
                dr["userAmount"] = cusSvrUserMaxAmount;
                dr["connLevel"] = maxCusSvrConnLevel;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataTable CommMaintainTableInit()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("userNum", typeof(string));
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userSendMsg", typeof(string));
            dt.Columns.Add("cusSvrSendMsg", typeof(string));
            dt.Columns.Add("commLevel", typeof(int));          //标记客户多久未回复
            dt.Columns.Add("connLeveL", typeof(int));          //标记客户是否断线

            return dt;
        }

        public DataTable ChatRecordsTableInit()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("userNum", typeof(string));
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userSendMsg", typeof(string));
            dt.Columns.Add("cusSvrSendMsg", typeof(string));
            dt.Columns.Add("dateTime", typeof(DateTime));

            return dt;
        }

        /// <summary>
        /// 在线客服的辅助线程
        /// 作用：1、去除超过规定时间的用户，更新客服维护表中客服服务客服数
        ///       2、把通信维护表中所有记录的commLevel加1
        ///       2、维护客服状态表,设置断开连接的客服，给connLevel加1
        ///       3、把聊天记录写入数据库，并清空聊天记录
        /// </summary>
        public void StartAssisterThreading(object obj)
        {
            HttpApplicationState application = obj as HttpApplicationState;
            application.Lock();
            try
            {
                DataTable dtComm = application["commMaintainTable"] as DataTable;
                DataTable dtCusSvrCondition = application["cusSvrConditionTable"] as DataTable;

                //删除超时用户
                var linq = from c in dtComm.AsEnumerable()
                           where c.Field<int>("commLevel") == delLeval
                           select c;
                foreach (DataRow dr in linq)
                {
                    var drTemp = dtCusSvrCondition.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == dr["cusSvrNum"]).First();
                    drTemp["userAmount"] = Convert.ToInt32(drTemp["userAmount"]) - 1;

                    dr.Delete();
                }
                //确认删除
                dtComm.AcceptChanges();

                foreach (DataRow dr in dtComm.Rows)
                {
                    dr["commLevel"] = Convert.ToInt32(dr["commLevel"]) + 1;
                }

                //维护客服状态表
                //客服是否已经下线或者断开
                foreach (DataRow dr in dtCusSvrCondition.Rows)
                {
                    if (Convert.ToInt32(dr["connLevel"]) == maxCusSvrConnLevel)
                    {
                        dr["userAmount"] = cusSvrUserMaxAmount;
                        continue;
                    }

                    dr["connLevel"] = Convert.ToInt32(dr["connLevel"]) + 1;
                }

                //把聊天记录写入数据库
                DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
                BLL.OnlineCusSvr.ChatRecords bll = new BLL.OnlineCusSvr.ChatRecords();
                bll.SaveChatRecords(dtChatRecords);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }
    }

    public class CusSvrInitCM : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //write your handler implementation here.
        }

        #endregion
        private int delLeval;
        private int maxCusSvrConnLevel;

        public CusSvrInitCM(Dictionary<string,int> configData)
        {
            delLeval = configData["cusSvrUserDelLevel"];
            maxCusSvrConnLevel = configData["maxCusSvrConnLevel"];
        }

        public DataTable CusSvrConditionTableInit(List<string> cusSvrNumList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("connLevel", typeof(int));              //标记客服是否断线

            foreach (string cusSvrNum in cusSvrNumList)
            {
                DataRow dr = dt.NewRow();
                dr["cusSvrNum"] = cusSvrNum;
                dr["connLevel"] = maxCusSvrConnLevel;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataTable CommMaintainTableInit()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("userNum", typeof(string));
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userSendMsg", typeof(string));
            dt.Columns.Add("cusSvrSendMsg", typeof(string));
            dt.Columns.Add("commLevel", typeof(int));          //标记客户多久未回复
            dt.Columns.Add("connLeveL", typeof(int));          //标记客户是否断线

            return dt;
        }

        public DataTable ChatRecordsTableInit()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("userNum", typeof(string));
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userSendMsg", typeof(string));
            dt.Columns.Add("cusSvrSendMsg", typeof(string));
            dt.Columns.Add("dateTime", typeof(DateTime));

            return dt;
        }

        /// <summary>
        /// 在线客服的辅助线程
        /// 作用：1、去除超过规定时间的用户
        ///       2、把通信维护表中所有记录的commLevel加1
        ///       2、维护客服状态表,设置断开连接的客服，给connLevel加1
        ///       3、把聊天记录写入数据库，并清空聊天记录
        /// </summary>
        public void StartAssisterThreading(object obj)
        {
            HttpApplicationState application = obj as HttpApplicationState;
            application.Lock();
            try
            {
                DataTable dtComm = application["commMaintainTable"] as DataTable;
                DataTable dtCusSvrCondition = application["cusSvrConditionTable"] as DataTable;

                //删除超时用户
                var linq = from c in dtComm.AsEnumerable()
                           where c.Field<int>("commLevel") == delLeval
                           select c;
                foreach (DataRow dr in linq)
                {
                    var drTemp = dtCusSvrCondition.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == dr["cusSvrNum"]).First();

                    dr.Delete();
                }
                //确认删除
                dtComm.AcceptChanges();

                foreach (DataRow dr in dtComm.Rows)
                {
                    dr["commLevel"] = Convert.ToInt32(dr["commLevel"]) + 1;
                }

                //维护客服状态表
                //客服是否已经下线或者断开
                foreach (DataRow dr in dtCusSvrCondition.Rows)
                {
                    if (Convert.ToInt32(dr["connLevel"]) == maxCusSvrConnLevel)
                    {
                        continue;
                    }

                    dr["connLevel"] = Convert.ToInt32(dr["connLevel"]) + 1;
                }

                //把聊天记录写入数据库
                DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
                BLL.OnlineCusSvr.ChatRecords bll = new BLL.OnlineCusSvr.ChatRecords();
                bll.SaveChatRecords(dtChatRecords);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }
    }

    public class CusSvrInit : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //write your handler implementation here.
        }

        #endregion
        private int cusSvrMode;
        private int cusSvrUserMaxAmount;
        private int delLeval;
        private int maxCusSvrConnLevel;

        public CusSvrInit(Dictionary<string, int> configData)
        {
            cusSvrMode = configData["cusSvrMode"];
            cusSvrUserMaxAmount = configData["cusSvrUserMaxAmount"];
            delLeval = configData["cusSvrUserDelLevel"];
            maxCusSvrConnLevel = configData["maxCusSvrConnLevel"];
        }

        public DataTable CusSvrConditionTableInit(List<string> cusSvrNumList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userAmount", typeof(Int32));
            dt.Columns.Add("connLevel", typeof(int));              //标记客服是否断线

            foreach (string cusSvrNum in cusSvrNumList)
            {
                DataRow dr = dt.NewRow();
                dr["cusSvrNum"] = cusSvrNum;
                dr["userAmount"] = cusSvrUserMaxAmount;
                dr["connLevel"] = maxCusSvrConnLevel;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataTable CommMaintainTableInit()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("userNum", typeof(string));
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userSendMsg", typeof(string));
            dt.Columns.Add("cusSvrSendMsg", typeof(string));
            dt.Columns.Add("commLevel", typeof(int));          //标记客户多久未回复
            dt.Columns.Add("connLeveL", typeof(int));          //标记客户是否断线

            return dt;
        }

        public DataTable ChatRecordsTableInit()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("userNum", typeof(string));
            dt.Columns.Add("cusSvrNum", typeof(string));
            dt.Columns.Add("userSendMsg", typeof(string));
            dt.Columns.Add("cusSvrSendMsg", typeof(string));
            dt.Columns.Add("dateTime", typeof(DateTime));

            return dt;
        }

        /// <summary>
        /// 在线客服的辅助线程
        /// 作用：1、去除超过规定时间的用户，更新客服维护表中客服服务客服数
        ///       2、把通信维护表中所有记录的commLevel加1
        ///       2、维护客服状态表,设置断开连接的客服，给connLevel加1
        ///       3、把聊天记录写入数据库，并清空聊天记录
        /// </summary>
        public void StartAssisterThreading(object obj)
        {
            //get online customer server config data
            BLL.Config.OnlineCusSvrConfig bllConfig = new BLL.Config.OnlineCusSvrConfig();
            Dictionary<string, int> configData = bllConfig.GetConfigDataG();

            cusSvrMode = configData["cusSvrMode"];
            cusSvrUserMaxAmount = configData["cusSvrUserMaxAmount"];
            delLeval = configData["cusSvrUserDelLevel"];
            maxCusSvrConnLevel = configData["maxCusSvrConnLevel"];

            HttpApplicationState application = obj as HttpApplicationState;
            application.Lock();
            try
            {
                if (cusSvrMode == 0)
                {
                    DataTable dtComm = application["commMaintainTable"] as DataTable;
                    DataTable dtCusSvrCondition = application["cusSvrConditionTable"] as DataTable;

                    //删除超时用户
                    var linq = from c in dtComm.AsEnumerable()
                               where c.Field<int>("commLevel") == delLeval
                               select c;
                    foreach (DataRow dr in linq)
                    {
                        var drTemp = dtCusSvrCondition.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == dr["cusSvrNum"]).First();
                        drTemp["userAmount"] = Convert.ToInt32(drTemp["userAmount"]) - 1;

                        dr.Delete();
                    }
                    //确认删除
                    dtComm.AcceptChanges();

                    foreach (DataRow dr in dtComm.Rows)
                    {
                        dr["commLevel"] = Convert.ToInt32(dr["commLevel"]) + 1;
                    }

                    //维护客服状态表
                    //客服是否已经下线或者断开
                    foreach (DataRow dr in dtCusSvrCondition.Rows)
                    {
                        if (Convert.ToInt32(dr["connLevel"]) == maxCusSvrConnLevel)
                        {
                            dr["userAmount"] = cusSvrUserMaxAmount;
                            continue;
                        }

                        dr["connLevel"] = Convert.ToInt32(dr["connLevel"]) + 1;
                    }

                    //把聊天记录写入数据库
                    DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
                    BLL.OnlineCusSvr.ChatRecords bll = new BLL.OnlineCusSvr.ChatRecords();
                    bll.SaveChatRecords(dtChatRecords);
                }
                else
                {
                    DataTable dtComm = application["commMaintainTable"] as DataTable;
                    DataTable dtCusSvrCondition = application["cusSvrConditionTable"] as DataTable;

                    //删除超时用户
                    var linq = from c in dtComm.AsEnumerable()
                               where c.Field<int>("commLevel") == delLeval
                               select c;
                    foreach (DataRow dr in linq)
                    {
                        var drTemp = dtCusSvrCondition.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == dr["cusSvrNum"]).First();

                        dr.Delete();
                    }
                    //确认删除
                    dtComm.AcceptChanges();

                    foreach (DataRow dr in dtComm.Rows)
                    {
                        dr["commLevel"] = Convert.ToInt32(dr["commLevel"]) + 1;
                    }

                    //维护客服状态表
                    //客服是否已经下线或者断开
                    foreach (DataRow dr in dtCusSvrCondition.Rows)
                    {
                        if (Convert.ToInt32(dr["connLevel"]) == maxCusSvrConnLevel)
                        {
                            continue;
                        }

                        dr["connLevel"] = Convert.ToInt32(dr["connLevel"]) + 1;
                    }

                    //把聊天记录写入数据库
                    DataTable dtChatRecords = application["chatRecordsTable"] as DataTable;
                    BLL.OnlineCusSvr.ChatRecords bll = new BLL.OnlineCusSvr.ChatRecords();
                    bll.SaveChatRecords(dtChatRecords);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }
    }
}