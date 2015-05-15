using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P4
{
    //寻找资产
    public class CP401
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P40101 初始化
        public string FP40101(int pageSize)
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //判断是否有权限
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                bool ifBillOverdue = C201.FC20153(dbma1, userSN);
                if (data.assetsPruchaseStatus != true || ifBillOverdue == true)
                {
                    return "{\"status\":\"false\"}";
                }

                string userName = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                string CancelReason = C201.FC20137(dbma1);

                var vp401001List = (from c in dbma1.VP401001s
                                    orderby c.publishDate descending
                                    select c).Take(pageSize).ToList();
                string vp401001ListStr = C101.FC10107(vp401001List);

                return string.Format("{{\"status\":\"true\",\"userName\":\"{0}\",\"CancelReason\":{1},\"dataList\":{2},\"maxDatetime\":\"{3}\",\"userSN\":\"{4}\"}}", userName, CancelReason, vp401001ListStr, DateTime.Now, userSN);
            }
        }
        #endregion

        #region P40102 滚动加载
        public string FP40102(DateTime maxDatetime,int pageFrom,int pageSize)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var vp401001List = (from c in dbma1.VP401001s
                                    where maxDatetime > c.publishDate
                                    orderby c.publishDate descending
                                    select c).Skip(pageFrom).Take(pageSize).ToList();
                string vp401001ListStr = C101.FC10107(vp401001List);

                return vp401001ListStr;
            }
        }
        #endregion 

        #region P40103 出售方信息
        public string FP40103(string sellerSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var sellerData = (from c in dbma1.VP801001s
                                  where c.userSN == sellerSN
                                  select new
                                  {
                                      c.userSN,
                                      c.birthday,
                                      c.gender,
                                      c.registeredResidence,
                                      idCard = c.idCard.Substring(0, 6) + "*",
                                      c.maritalStatusType,
                                      c.procreateStatus,
                                  }).First();
                string sellerDataStr = C101.FC10107(sellerData);

                int amount = SellerSuccessCaseAmount(dbma1, sellerSN);

                return string.Format("{{\"data\":{0},\"amount\":\"{1}\"}}", sellerDataStr, amount);
            }
        }
        #endregion

        //出售方信息
        private string SellerData(DBMA1DataContext dbma1, string userSN)
        {
            var sellerInfo = (from c in dbma1.VP801001s
                              where c.userSN == userSN
                              select new
                              {
                                  c.userSN,
                                  c.name,
                                  c.birthday,
                                  c.gender,
                                  c.registeredResidence,
                                  idCard = c.idCard.Substring(0, 6) + "*",
                                  phone = c.phone.Substring(0, 3) + "****" + c.phone.Substring(7, 4),
                                  c.email,
                                  c.maritalStatusType,
                                  c.procreateStatus,
                                  c.currentAddressProvince,
                                  c.currentAddressCity,
                                  c.currentAddressDetails
                              }).First();

            return C101.FC10107(sellerInfo);
        }

        //出售方成功案例
        public int SellerSuccessCaseAmount(DBMA1DataContext dbma1,string userSN)
        {
            int amount = (from c in dbma1.P300s.Where(c => c.cancelDate == null)
                          from o in c.P400s.Where(o => o.senderCancelReserveDate == null && o.receiverRefuseReserveDate == null)
                          from p in o.P401s
                          select c).Count();

            return amount;
        }
    }
}
