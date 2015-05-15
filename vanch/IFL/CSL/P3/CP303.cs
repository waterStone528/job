using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P3
{
    //已售资产
    public class CP303
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P30301 初始化
        public string FP30301(int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00003";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.VP404001s
                                where c.sellerUserSN == userSN
                                    && c.sellerDeleteDate == null
                                orderby c.purchaseDate descending
                                select c).Take(pageSize).ToList();

                string dataListStr = C101.FC10107(dataList);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dataListStr, DateTime.Now);
            }
        }
        #endregion

        #region P30302 滚动加载
        public string FP30302(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00003";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.VP404001s
                                where c.sellerUserSN == userSN
                                    && c.sellerDeleteDate == null
                                    && maxDatetime >　c.purchaseDate
                                orderby c.purchaseDate descending
                                select c).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region P30303 删除
        public void FP30303(string purchaseSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                P401 p401 = dbma1.P401s.Where(c => c.purchaseSN == purchaseSN).First();
                p401.sellerDeleteDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
