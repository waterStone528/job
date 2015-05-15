using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Web;

namespace BLL.OnlineCusSvr
{
    public class OnlineCusSvr
    {
        private static DAL.Config.OnlineCusSvrConfig dal = new DAL.Config.OnlineCusSvrConfig();
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
        private int whichMode;

        public OnlineCusSvr()
        {
            if(session["cusSvrMode"] == null || session["cusSvrUserMaxAmount"] == null || session["cusSvrUserDelLevel"] == null || session["maxCusSvrConnLevel"] == null || session["maxUserConnNum"] == null || session["countSizeLevel"] == null || session["showCountDownSizeLevel"] == null || session["branchCountDownAjaxSizeLevel"] == null || session["levelSeconds"] == null)
            {
                DAL.config configData = dal.GetConfigData();
                session["cusSvrMode"] = configData.cusSvrMode;
                session["cusSvrUserMaxAmount"] = configData.cusSvrUserMaxAmount;
                session["cusSvrUserDelLevel"] = configData.cusSvrUserDelLevel;
                session["maxCusSvrConnLevel"] = configData.maxCusSvrConnLevel;
                session["maxUserConnNum"] = configData.maxUserConnNum;
                session["countSizeLevel"] = configData.countSizeLevel;
                session["showCountDownSizeLevel"] = configData.showCountDownSizeLevel;
                //session["branchCountDownAjaxSizeLevel"] = configData.branchCountDownAjaxSizeLevel;
                session["levelSeconds"] = configData.levelSeconds;
            }

            whichMode = Convert.ToInt32(session["cusSvrMode"]);
        }

        public bool VerifyTimeIdentity(int intervalMinites,string timeIdentity)
        {
            List<string> timeIdentityList = Helper.GenerateTimeIdentity(intervalMinites);

            return timeIdentityList.Contains(timeIdentity);
        }

        //用户是否建立连接成功（即是否有可分配的客服）
        public string IsConnected(int intervalMinites,string timeIdentity, string userNum,bool cusSvrBusy)
        {
            //if (cusSvrBusy == false)
            //{
            //    if (VerifyTimeIdentity(intervalMinites, timeIdentity) == false)
            //    {
            //        return "{\"isTimeIdentityRight\":\"false\"}";
            //    }
            //}

            if (whichMode == 0)
            {
                BLL.OnlineCusSvrLB.User bll = new OnlineCusSvrLB.User();
                return bll.IsConnected(userNum);
            }
            else
            {
                BLL.OnlineCusSvrCM.User bll = new OnlineCusSvrCM.User();
                return bll.IsConnected(userNum);
            }
        }

        ///<summary>客服初始化
        ///1、设置客服状态表的用户服务数为0
        ///2、设置客服状态表的通信level为0
        /// </summary>
        public void InitCusSvr(string cusSvrNum)
        {
            if (whichMode == 0)
            {
                BLL.OnlineCusSvrLB.CusSvr bll = new OnlineCusSvrLB.CusSvr();
                bll.InitCusSvr(cusSvrNum);
            }
            else
            {
                BLL.OnlineCusSvrCM.CusSvr bll = new OnlineCusSvrCM.CusSvr();
                bll.InitCusSvr(cusSvrNum);
            }
        }

        ///<summary>
        ///用户发送消息
        /// </summary>
        public void UserSendMsg(string userNum, string msg)
        {
            if (whichMode == 0)
            {
                BLL.OnlineCusSvrLB.User bll = new OnlineCusSvrLB.User();
                bll.SendMsg(userNum, msg);
            }
            else
            {
                BLL.OnlineCusSvrCM.User bll = new OnlineCusSvrCM.User();
                bll.SendMsg(userNum, msg);
            }
        }

        /// <summary>
        /// 客服发送消息
        /// </summary>
        public void CusSvrSendMsg(string cusSvrNum, string userNum, string msg)
        {
            if (whichMode == 0)
            {
                BLL.OnlineCusSvrLB.CusSvr bll = new OnlineCusSvrLB.CusSvr();
                bll.SendMsg(cusSvrNum, userNum, msg);
            }
            else
            {
                BLL.OnlineCusSvrCM.CusSvr bll = new OnlineCusSvrCM.CusSvr();
                bll.SendMsg(cusSvrNum, userNum, msg);
            }
        }

        /// <summary>
        /// 用户接收消息
        /// </summary>
        /// <remarks>&* 为无效符号</remarks>
        public string UserReceiveMsg(string userNum)
        {
            if (whichMode == 0)
            {
                BLL.OnlineCusSvrLB.User bll = new OnlineCusSvrLB.User();
                return bll.ReceiveMsg(userNum);
            }
            else
            {
                BLL.OnlineCusSvrCM.User bll = new OnlineCusSvrCM.User();
                return bll.ReceiveMsg(userNum);
            }
        }

        /// <summary>
        /// 客服接收消息
        /// </summary>
        public string CusSvrReceiveMsg(string cusSvrNum)
        {
            if (whichMode == 0)
            {
                BLL.OnlineCusSvrLB.CusSvr bll = new OnlineCusSvrLB.CusSvr();
                return bll.ReceiveMsg(cusSvrNum);
            }
            else
            {
                BLL.OnlineCusSvrCM.CusSvr bll = new OnlineCusSvrCM.CusSvr();
                return bll.ReceiveMsg(cusSvrNum);
            }
        }

        /// <summary>
        /// 保持客户连接状态
        /// </summary>
        public void KeepUserConnect(string userNum)
        {
            HttpApplicationState application = HttpContext.Current.Application;
            application.Lock();
            try
            {
                DataTable dt = application["commMaintainTable"] as DataTable;
                DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("userNum") == userNum).FirstOrDefault();
                if (dr == null)
                {
                    return;
                }
                else
                {
                    dr["commLevel"] = 0;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                application.UnLock();
            }

        }

        //查看客服维护表和通信维护表
        public void WriteToFile()
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

    public class CusSvrGlobal
    {
        private static DAL.OnlineCusSvr.OnlineCusSvr dal = new DAL.OnlineCusSvr.OnlineCusSvr();

        public List<string> GetCusSvrWorkNum()
        {
            return dal.GetCusSvrWorkNum();
        }
    }
}
