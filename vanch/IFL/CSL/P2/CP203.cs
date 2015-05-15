using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P2
{
    //还款中债权
    public class CP203
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP2030101 初始化加载
        /// <summary>
        /// 初始化加载
        /// </summary>
        public string FP2030101(int pageSize)
        {
            string financierUserSN = session["userSN"].ToString();
            //string financierUserSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string dateStr = GetInvestCrFirst(dbma1, financierUserSN, pageSize);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dateStr, DateTime.Now);
            }
        }

        //首次加载债权信息
        private string GetInvestCrFirst(DBMA1DataContext dbma1, string financierUserSN, int pageSize)
        {
            var crDataList = (from c in dbma1.VP203011s
                              where c.financierUserSN == financierUserSN
                              orderby c.investDate descending
                              select c).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion

        #region FP2030102 滚动加载
        /// <summary>
        /// 滚动加载
        /// </summary>
        public string FP2030102(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string financierUserSN = session["userSN"].ToString();
            //string financierUserSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetInvestCrNotFirst(dbma1, financierUserSN, maxDatetime, pageFrom, pageSize);
            }
        } 

        //非首次加载债权信息
        private string GetInvestCrNotFirst(DBMA1DataContext dbma1, string financierUserSN, DateTime maxDatetime, int pageFrom, int pageSize)
        {
            var crDataList = (from c in dbma1.VP105001s
                              where c.financierUserSN == financierUserSN
                               && maxDatetime >= c.investDate
                              orderby c.investDate descending
                              select c).Skip(pageFrom).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion
    }
}
