using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P5
{
    //已结案债权
    public class CP50402
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P5040201 初始化
        public string FP5040201(int pageSize)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var vp504021 = (from c in dbma1.VP504021s
                                     where c.consultantUserSN == consultantUserSN
                                     orderby c.sortDatetime descending
                                     select c).Take(pageSize).ToList();
                string vp504021Str = C101.FC10107(vp504021);

                return string.Format("{{\"crDataList\":{0},\"maxDatetime\":\"{1}\"}}", vp504021Str, DateTime.Now);
            }
        }
        #endregion

        #region P5040202 滚动加载
        public string FP5040202(DateTime maxDatetime,int pageFrom,int pageSize)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var vp504021 = (from c in dbma1.VP504021s
                                where c.consultantUserSN == consultantUserSN
                                    && maxDatetime >= c.sortDatetime
                                orderby c.sortDatetime descending
                                select c).Skip(pageSize).Take(pageSize).ToList();
                return C101.FC10107(vp504021);
            }
        }
        #endregion

        #region P5040203 删除
        public void FP5040203(string serverSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var p500 = dbma1.P500s.Where(c => c.serverSN == serverSN).First();
                p500.consultantDeleteCloseCreditRightDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion 
    }
}
