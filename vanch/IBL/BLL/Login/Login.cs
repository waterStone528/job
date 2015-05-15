using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Data;

namespace BLL.Login
{
    public class Login
    {
        private static DAL.Login.Login dalLogin = new DAL.Login.Login();
        private static DAL.CommCtl dalCommCtl = new DAL.CommCtl();

        //登录
        public string IsLogin(string workNum, string pwd)
        {
            string isLogin = string.Empty;
            int internalUserId = dalLogin.IsLogin(workNum, pwd);

            if (internalUserId == 0)
            {
                return "false";
            }
            else
            {
                HttpContext.Current.Session["internalUserId"] = internalUserId;
                HttpContext.Current.Session["workNum"] = workNum;
                return "true";
            }
        }

        //根据是否已经登录，来判断是否出现登录框
        public string LoginOrNot()
        {
            if (HttpContext.Current.Session["internalUserId"] == null)
            {
                return "false";
            }

            int internalUserId = Convert.ToInt32(HttpContext.Current.Session["internalUserId"].ToString());
            string workNum = HttpContext.Current.Session["workNum"].ToString();
            string FirstPermissionMenu = dalCommCtl.GetFirstLevelPermissionMenu(internalUserId);
            if (FirstPermissionMenu == "")
            {
                FirstPermissionMenu = "[]";
            }
            return string.Format("{{\"workNum\":\"{0}\",\"menu\":{1}}}", workNum, FirstPermissionMenu);
        }
    }
}
