using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P1
{
    public class CP103
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP10303 初始化加载
        /// <summary>
        /// 初始化加载
        /// </summary>
        public string FP10303(int pageSize)
        {
            string investorUserSN = session["userSN"].ToString();
            //string investorUserSN = "U00002";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string dataStr = GetInvestCrFirst(dbma1, investorUserSN, pageSize);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dataStr, DateTime.Now);
            }
        }

        //首次加载债权信息
        private string GetInvestCrFirst(DBMA1DataContext dbma1, string investorUserSN, int pageSize)
        {
            var crDataList = (from c in dbma1.VP105001s
                              where c.investorUserSN == investorUserSN
                              orderby c.investDate descending
                              select c).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion

        #region FP10304 滚动加载
        /// <summary>
        /// 滚动加载
        /// </summary>
        public string FP10304(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string investorUserSN = session["userSN"].ToString();
            //string investorUserSN = "U00002";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetInvestCrNotFirst(dbma1, investorUserSN, maxDatetime, pageFrom, pageSize);
            }
        }

        //非首次加载债权信息
        private string GetInvestCrNotFirst(DBMA1DataContext dbma1, string investorUserSN, DateTime maxDatetime, int pageFrom, int pageSize)
        {
            var crDataList = (from c in dbma1.VP105001s
                              where c.investorUserSN == investorUserSN
                               && maxDatetime >= c.investDate
                              orderby c.investDate descending
                              select c).Skip(pageFrom).Take(pageSize).ToList();

            return C101.FC10107(crDataList);
        }
        #endregion

        #region FP10307 融资方历史信息
        /// <summary>
        /// 融资方历史信息
        /// </summary>
        public string FP10307(string crSN,string financierUserSN)
        {
            string financierHistoryDataStr = string.Empty;
            string regDatetimeStr = string.Empty;

            //基本信息
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var financierHistoryData = (from c in dbma1.VP105011s
                                          where c.userSN == financierUserSN
                                          && c.creditRightSN == crSN
                                          select c).First();
                financierHistoryDataStr = C101.FC10107(financierHistoryData);

                //注册时间
                DateTime regDatetime = dbma1.U000s.Where(c => c.userSN == financierUserSN).First().registerDate;
                regDatetimeStr = C101.FC10107(regDatetime);
            }

            return string.Format("{{\"historyData\":{0},\"regDatetime\":{1}}}",financierHistoryDataStr,regDatetimeStr);
        }
        #endregion

        #region FP10309 结案
        /// <summary>
        /// 结案
        /// </summary>
        public void FP10309(string crSN,DateTime repaymentDate)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、是否已经结案）
                var data = dbma1.P102s.Where(c => c.creditRightSN == crSN && c.closeCaseDate != null).FirstOrDefault();
                if(data != null)
                {
                    return;
                }

                var investCrData = dbma1.P102s.Where(c => c.creditRightSN == crSN).First();
                investCrData.closeCaseDate = DateTime.Now;

                string max33SN = C101.FC10102("P103",6,"D");
                P103 p103 = new P103();
                p103.paymentSN = max33SN;
                p103.investSN = investCrData.investSN;
                p103.repayDate = repaymentDate;
                p103.ifOverdue = repaymentDate > investCrData.deadlineDate ? true : false;

                dbma1.P103s.InsertOnSubmit(p103);
                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
