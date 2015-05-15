using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P9
{
    //历史明细
    public class CP903
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P90301 初始化
        public string FP90301(int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var linqDataList = (from c in dbma1.F003s
                                    where c.generetorUserSN == userSN
                                    select new
                                    {
                                        c.revenueExpenditureSN,
                                        c.generateDate,
                                        c.type,
                                        c.revenue,
                                        c.expenditure,
                                        c.balance
                                    }
                                    ).Take(pageSize).ToList();
                string linqDataListStr = C101.FC10107(linqDataList);

                return string.Format("{{\"dataList\":{0},\"maxDatetime\":\"{1}\"}}", linqDataListStr, DateTime.Now);
            }
        }
        #endregion

        #region P90302 滚动加载
        public string FP90302(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var linqDataList = (from c in dbma1.F003s
                                    where c.generetorUserSN == userSN
                                        && maxDatetime >= c.generateDate
                                    select new
                                    {
                                        c.revenueExpenditureSN,
                                        c.generateDate,
                                        c.type,
                                        c.revenue,
                                        c.expenditure,
                                        c.balance
                                    }
                                    ).Skip(pageFrom).Take(pageSize).ToList();
                  return C101.FC10107(linqDataList);
            }
        }
        #endregion
    }
}
