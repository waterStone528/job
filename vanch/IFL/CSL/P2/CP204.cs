using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P2
{
    //已还款债权
    public class CP204
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP2030201 初始化加载
        /// <summary>
        /// 初始化加载
        /// </summary>
        public string FP2030201(int pageSize)
        {
            string financierUserSN = session["userSN"].ToString();
            //string financierUserSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string dateStr = GetHasRepayedCrFirst(dbma1, financierUserSN, pageSize);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dateStr, DateTime.Now);
            }
        }

        //首次加载债权信息
        private string GetHasRepayedCrFirst(DBMA1DataContext dbma1, string financierUserSN, int pageSize)
        {
            var crDataList = (from c in dbma1.VP203021s
                              where c.financierUserSN == financierUserSN
                              orderby c.repayDate descending
                              select c).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion

        #region FP2030202 滚动加载
        /// <summary>
        /// 滚动加载
        /// </summary>
        public string FP2030202(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string financierUserSN = session["userSN"].ToString();
            //string financierUserSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetHasRepayedCrNotFirst(dbma1, financierUserSN, maxDatetime, pageFrom, pageSize);
            }
        }

        //非首次加载债权信息
        private string GetHasRepayedCrNotFirst(DBMA1DataContext dbma1, string financierUserSN, DateTime maxDatetime, int pageFrom, int pageSize)
        {
            var crDataList = (from c in dbma1.VP203021s
                              where c.financierUserSN == financierUserSN
                              && maxDatetime >= c.repayDate
                              orderby c.repayDate descending
                              select c).Skip(pageFrom).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion

        #region FP2030203 删除
        /// <summary>
        /// 删除
        /// </summary>
        public void FP2030203(string crSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var closedCaseCr = dbma1.P102s.Where(c => c.creditRightSN == crSN).First();
                closedCaseCr.fundSeekerDeleteDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
