using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL
{
    //index页面
    public class Index
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region 记录userAgent
        public void RecordUserAgent(string userAgent)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                T003 t003 = new T003();
                t003.userAgent = userAgent;

                dbma1.T003s.InsertOnSubmit(t003);
                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region Init 判断是否登录，并获得用户名
        public string Init()
        {
            var userSNSession = session["userSN"];
            if (userSNSession == null)
            {
                return "{\"isLogin\":\"false\"}";
            }
            else
            {
                using (DBMA1DataContext dbma1 = new DBMA1DataContext())
                {
                    string userName = dbma1.U002s.Where(c => c.userSN == userSNSession.ToString()).First().name;

                    return string.Format("{{\"isLogin\":\"true\",\"userName\":\"{0}\",\"userSN\":\"{1}\"}}", userName, userSNSession);
                }
            }
        }
        #endregion

        #region LoadSvr 加载模块
        public string LoadSvr(string whichSvr)
        {
            var userSNSession = session["userSN"];
            if (userSNSession == null)
            {
                return "{\"isLogin\":\"false\"}";
            }
            else
            {
                using(DBMA1DataContext dbma1 = new DBMA1DataContext())
                {
                    U001 u001 = dbma1.U001s.Where(c => c.userSN == userSNSession.ToString()).First();

                    bool isAvailable = false;

                    switch(whichSvr)
                    {
                        case "1":
                            isAvailable = u001.creditRightInvestStatus == true ? true : false;
                            break;
                        case "2":
                            isAvailable = u001.creditRightFinancingStatus == true ? true : false;
                            break;
                        case "5":
                            isAvailable = u001.consultantStatus == true ? true : false;
                            break;
                        case "3":
                            isAvailable = u001.assetsSellingStatus == true ? true : false;
                            break;
                        case "4":
                            isAvailable = u001.assetsPruchaseStatus == true ? true : false;
                            break;
                    }

                    return string.Format("{{\"isLogin\":\"true\",\"isAvailable\":\"{0}\"}}", isAvailable.ToString());
                }
            }
        }
        #endregion 

        #region 判断是否已经登录，并获得用户编号
        public string GetUserSN()
        {
            var userSNSession = session["userSN"];

            if(userSNSession == null)
            {
                return "false";
            }
            else
            {
                return userSNSession.ToString();
            }
        }
        #endregion 
    }
}
