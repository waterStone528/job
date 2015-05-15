using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P4
{
    //已购资产
    public class CP404
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P40401 初始化
        public string FP40401(int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.VP404001s
                                where c.purchaserUserSN == userSN
                                 && c.purchaserDeleteDate == null
                                orderby c.purchaseDate descending
                                select c).ToList();
                string dataListStr = C101.FC10107(dataList);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dataListStr, DateTime.Now);
            }
        }
        #endregion

        #region P40402 滚动加载
        public string FP40402(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.VP404001s
                                where c.purchaserUserSN == userSN
                                 && c.purchaserDeleteDate == null
                                 && maxDatetime > c.purchaseDate
                                orderby c.purchaseDate descending
                                select c).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion 

        #region P40403 出售方信息
        public string FP40403(string userSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var sellerData = (from c in dbma1.VP801001s
                                  where c.userSN == userSN
                                  select new
                                  {
                                      c.userSN,
                                      c.name,
                                      c.birthday,
                                      c.gender,
                                      c.registeredResidence,
                                      idCard = c.idCard.Substring(0, 6) + "*",
                                      //phone = c.phone.Substring(0, 3) + "****" + c.phone.Substring(7, 4),
                                      phone = c.phone,
                                      c.email,
                                      c.maritalStatusType,
                                      c.procreateStatus,
                                      c.currentAddressProvince,
                                      c.currentAddressCity,
                                      c.currentAddressDetails
                                  }).First();

                string sellerDataStr = C101.FC10107(sellerData);

                int amount = (from c in dbma1.P300s.Where(c => c.cancelDate == null)
                              from o in c.P400s.Where(o => o.senderCancelReserveDate == null && o.receiverRefuseReserveDate == null)
                              from p in o.P401s
                              select c).Count();

                return string.Format("{{\"data\":{0},\"amount\":\"{1}\"}}", sellerDataStr, amount);
            }
        }
        #endregion

        #region P40404 删除
        public void FP40404(string purchaseSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                P401 p401 = dbma1.P401s.Where(c => c.purchaseSN == purchaseSN).First();
                p401.purchaserDeleteDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion 
    }
}
