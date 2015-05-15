using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P3
{
    //预约中资产
    public class CP302
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P30201 初始化
        public string FP30201(int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00003";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string assetsType = C201.FC20108(dbma1);

                var dataList = (from c in dbma1.VP402011s
                                where c.receiverUserSN == userSN
                                orderby c.reserveDate descending
                                select c).Take(pageSize).ToList();

                string dataListStr = C101.FC10107(dataList);

                return string.Format("{{\"SltConfigData\":{0},\"data\":{1},\"maxDatetime\":\"{2}\"}}", assetsType, dataListStr, DateTime.Now);
            }
        }
        #endregion

        #region P30202 滚动加载
        public string FP30202(DateTime maxDatetime,int pageFrom,int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00003";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.VP402011s
                                where c.receiverUserSN == userSN
                                    && maxDatetime > c.reserveDate
                                orderby c.reserveDate descending
                                select c).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region P30203 购买方信息
        public string FP30203(string purchaserUserSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var purchaserData = (from c in dbma1.VP801001s
                                     where c.userSN == purchaserUserSN
                                     select new
                                     {
                                         c.userSN,
                                         c.name,
                                         c.birthday,
                                         c.gender,
                                         c.registeredResidence,
                                         idCard = c.idCard.Substring(0, 6) + "*",
                                         //phone = c.phone.Substring(0, 3) + "****" + c.phone.Substring(7, 4),
                                         phone = c.phone.Substring(0, 3),
                                         c.email,
                                         c.maritalStatusType,
                                         c.procreateStatus,
                                         c.currentAddressProvince,
                                         c.currentAddressCity,
                                         c.currentAddressDetails,
                                         c.assetsProvince,
                                         c.minPurchasePrice,
                                         c.maxPurchasePrice,
                                         c.assetsType
                                     }).First();

                return C101.FC10107(purchaserData);
            }
        }
        #endregion

        #region P30204 拒绝预约
        public void FP30204(string reserveSN,string refuseReasonTypeSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、已拒绝或者取消。2、已购买）
                var data1 = dbma1.P400s.Where(c => c.reserveSN == reserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var data2 = dbma1.P401s.Where(c => c.reserveSN == reserveSN).FirstOrDefault();
                if(data1 != null || data2 != null)
                {
                    return;
                }

                P400 p400 = dbma1.P400s.Where(c => c.reserveSN == reserveSN).First();
                p400.receiverRefuseReserveDate = DateTime.Now;
                p400.receiverRefuseReserveReasonTypeSN = refuseReasonTypeSN;
                dbma1.SubmitChanges();
            }
        }
        #endregion 
    }
}
