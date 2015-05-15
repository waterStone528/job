using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P1
{
    //已结案债权
    public class CP104
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP10501 初始化加载 
        /// <summary>
        /// 初始化加载
        /// </summary>
        public string FP10501(int pageSize)
        {
            string investorUserSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string dataStr = GetClosedCaseCrFirst(dbma1, investorUserSN, pageSize);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dataStr, DateTime.Now);
            }
        }

        //首次加载债权信息
        private string GetClosedCaseCrFirst(DBMA1DataContext dbma1, string investorUserSN, int pageSize)
        {
            var crDataList = (from c in dbma1.VP105021s
                              where c.investorUserSN == investorUserSN
                              orderby c.verifyInvestDate descending
                              select c).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion

        #region FP10502 滚动加载
        /// <summary>
        /// 滚动加载
        /// </summary>
        public string FP10502(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string investorUserSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetClosedCaseCrNotFirst(dbma1, investorUserSN, maxDatetime, pageFrom, pageSize);
            }
        }

        //非首次加载债权信息
        private string GetClosedCaseCrNotFirst(DBMA1DataContext dbma1, string investorUserSN, DateTime maxDatetime, int pageFrom, int pageSize)
        {
            var crDataList = (from c in dbma1.VP105021s
                              where c.investorUserSN == investorUserSN
                              && maxDatetime >= c.repayDate
                              orderby c.verifyInvestDate descending
                              select c).Skip(pageFrom).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion

        #region FP10503 删除
        /// <summary>
        /// 删除
        /// </summary>
        public void FP10503(string crSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var closedCaseCr = dbma1.P102s.Where(c => c.creditRightSN == crSN).First();
                closedCaseCr.investorDeleteDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
