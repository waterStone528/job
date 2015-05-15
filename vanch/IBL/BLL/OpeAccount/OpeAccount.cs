using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.SessionState;
using System.Data;

namespace BLL.OpeAccount
{
    public class OpeAccount
    {
        private static DAL.OpeAccount.OpeAccount dal = new DAL.OpeAccount.OpeAccount();

        //获得员工编号
        public string GetWorkNum()
        {
            return HttpContext.Current.Session["workNum"].ToString();
        }

        //修改密码
        //true-修改成功；false-原密码错误
        public bool ModifyPwd(string internalUserId, string oldPwd, string newPwd)
        {
            return dal.ModifyPwd(Convert.ToInt32(internalUserId), oldPwd, newPwd);
        }

        //注销账户，如果是客服，更新客服维护表和通信维护表
        public void LogOut(string workNum)
        {
            HttpContext.Current.Session["internalUserId"] = null;
            HttpApplicationState application = HttpContext.Current.Application;

            List<string> cusSvrNumList = application["cusSvrNumList"] as List<string>;
            if (cusSvrNumList.Contains(workNum))
            {
                application.Lock();
                try
                {
                    //客服维护表中的该客服的服务用户数设为最大值
                    DataTable dt = application["cusSvrConditionTable"] as DataTable;
                    DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == workNum).First() as DataRow;
                    dr["userAmount"] = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["cusSvrUserMaxAmount"]);

                    //删除通信维护表中的该客服的所有通信记录
                    DataTable dtCommMaintain = application["commMaintainTable"] as DataTable;
                    var linq = dtCommMaintain.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == workNum);
                    foreach (var drTemp in linq)
                    {
                        drTemp.Delete();
                    }
                    dtCommMaintain.AcceptChanges();

                    HttpContext.Current.Session.Clear();
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
}
