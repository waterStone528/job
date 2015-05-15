using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P4
{
    //预约中资产
    public class CP402
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P40201 预约
        //0：成功。1：密码错误。2：余额不足。 3：资产已被取消。4：资产已被预约
        public string FP40201(string assetsSN, string pwd)
        {
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、已被取消。2、已被预约）
                var data1 = dbma1.P300s.Where(c => c.assetsSN == assetsSN && c.cancelDate != null).FirstOrDefault();
                var data2 = dbma1.P400s.Where(c => c.assetsSN == assetsSN && c.senderCancelReserveDate == null && c.receiverRefuseReserveDate == null).FirstOrDefault();
                if(data1 != null)
                {
                    return "3";
                }
                if(data2 != null)
                {
                    return "4";
                }

                //密码是否正确
                if (C201.FC20146(dbma1, userSN, pwd) == false)
                {
                    return "1";
                }

                //余额是否充足
                decimal assetsReserveFee = Convert.ToDecimal(dbma1.A026s.First().assetsReserveCost);
                //扣款
                if (C201.FC20147(dbma1, userSN, assetsReserveFee, "资产预约", null) == false)
                {
                    return "2";
                }

                //加入资产预约表 P400
                string sellerUserSN = dbma1.P300s.Where(c => c.assetsSN == assetsSN).First().publisherUserSN;
                string max33SN = C101.FC10102("P400",7,"H");
                P400 p400 = new P400();
                p400.reserveSN = max33SN;
                p400.senderUserSN = userSN;
                p400.receiverUserSN = sellerUserSN;
                p400.assetsSN = assetsSN;
                p400.reserveDate = DateTime.Now;
                dbma1.P400s.InsertOnSubmit(p400);

                //加入成长值表 F006
                F006 f006 = new F006();
                f006.groupUpSN = C101.FC10102("F006", 7, "UD");
                f006.userSN = userSN;
                f006.businessSN = max33SN;
                f006.businessType = "资产预约";
                f006.transactionMoneyAmount = assetsReserveFee;
                f006.groupUpValue = assetsReserveFee;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                U003 u003 = dbma1.U003s.Where(c => c.userSN == sellerUserSN).First();

                //如有邮箱提醒
                if (u003.billGenerate_email == true)
                {
                    ReserveSendByEmail(dbma1, sellerUserSN, assetsSN);
                }
                //如有短信提醒
                if (u003.billGenerate_shortMessage == true)
                {
                    ReserveSendBySM(dbma1, sellerUserSN, assetsSN);
                }

                dbma1.SubmitChanges();

                return "0";
            }
        }

        //发送产生账单邮件
        private int ReserveSendByEmail(DBMA1DataContext dbma1, string sellerUserSN, string assetsSN)
        {
            string subject = "【凡奇金融】资产预约";
            string content = string.Format("亲爱的凡奇用户，您的资产({0})已经被预约，请您查看。", assetsSN);
            return C101.FC10105(dbma1, sellerUserSN, subject, content);
        }

        //发送产生账单短信
        private void ReserveSendBySM(DBMA1DataContext dbma1, string sellerUserSN, string assetsSN)
        {
            string phoneNum = dbma1.U000s.Where(c => c.userSN == sellerUserSN).First().phone;

            string content = string.Format("亲爱的凡奇用户，您的资产({0})已经被预约，请您查看。", assetsSN);
            Comm.SM.F001(dbma1, sellerUserSN, phoneNum, content, 1, true);
        }
        #endregion

        #region P40202 初始化
        public string FP40202(int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.VP402011s
                                where c.senderUserSN == userSN
                                orderby c.reserveDate descending
                                select c).Take(pageSize).ToList();

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", C101.FC10107(dataList), DateTime.Now);
            }
        }
        #endregion

        #region P40203 滚动加载
        public string FP40203(DateTime maxDatetime,int pageFrom,int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.VP402011s
                                where c.senderUserSN == userSN
                                    && maxDatetime > c.reserveDate
                                orderby c.reserveDate descending
                                select c).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region P40204 出售方信息
        public string FP40204(string userSN)
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

        #region P40205 购买
        public void FP40205(string reserveSN,decimal purcharsePrice)
        {
            string userSN = session["userSN"].ToString();

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、已被取消或者拒绝。2、已被购买）
                var data1 = dbma1.P400s.Where(c => c.reserveSN == reserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var data2 = dbma1.P401s.Where(c => c.reserveSN == reserveSN).FirstOrDefault();
                if(data1 != null || data2 != null)
                {
                    return;
                }

                //加入资产购买表 P401
                string max33SN = C101.FC10102("P401", 6, "J");
                P400 p400 = dbma1.P400s.Where(c => c.reserveSN == reserveSN).First();
                P401 p401 = new P401();

                p401.purchaseSN = max33SN;
                p401.reserveSN = reserveSN;
                p401.purchaserUserSN = userSN;
                p401.sellerUserSN = p400.receiverUserSN;
                p401.assetsSN = p400.assetsSN;
                p401.purchasePrice = purcharsePrice;
                p401.purchaseDate = DateTime.Now;
                dbma1.P401s.InsertOnSubmit(p401);

                //加入账单表 F001
                decimal serviceRate = Convert.ToDecimal(dbma1.A027s.First().serviceRate);

                string F001max33SN = C101.FC10102("F001", 6, "L");
                F001 f001 = new F001();
                f001.billSN = F001max33SN;
                f001.payerUserSN = p400.receiverUserSN;
                f001.businessSN = p400.assetsSN;
                f001.billType = "资产出售";
                f001.MoneyAmount = purcharsePrice * serviceRate * 10000;
                f001.generateDate = DateTime.Now;
                dbma1.F001s.InsertOnSubmit(f001);

                dbma1.SubmitChanges();

                U003 u003 = dbma1.U003s.Where(c => c.userSN == p400.receiverUserSN).First();

                //如有邮箱提醒
                if (u003.billGenerate_email == true)
                {
                    BillSendByEmail(dbma1, p400.receiverUserSN, p400.assetsSN);
                }
                //如有短信提醒
                if (u003.billGenerate_shortMessage == true)
                {
                    BillSendBySM(dbma1, p400.receiverUserSN, p400.assetsSN);
                }

                dbma1.SubmitChanges();
            }
        }

        //发送产生账单邮件
        private int BillSendByEmail(DBMA1DataContext dbma1, string sellerUserSN, string assetsSN)
        {
            string subject = "【凡奇金融】资产购买";
            string content = string.Format("亲爱的凡奇用户，您的资产({0})已经被购买，并产生账单，请您查看。", assetsSN);
            return C101.FC10105(dbma1, sellerUserSN, subject, content);
        }

        //发送产生账单短信
        private void BillSendBySM(DBMA1DataContext dbma1, string sellerUserSN, string assetsSN)
        {
            string phoneNum = dbma1.U000s.Where(c => c.userSN == sellerUserSN).First().phone;

            string content = string.Format("亲爱的凡奇用户，您的资产({0})已经被购买，并产生账单，请您查看。", assetsSN);
            Comm.SM.F001(dbma1, sellerUserSN, phoneNum, content, 1, true);
        }
        #endregion

        #region P40206 取消预约
        public void FP40206(string reserveSN,string cancelReasonTypeSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、已被取消或者拒绝。2、已被购买）
                var data1 = dbma1.P400s.Where(c => c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null).FirstOrDefault();
                var data2 = dbma1.P401s.Where(c => c.reserveSN == reserveSN).FirstOrDefault();
                if (data1 != null || data2 != null)
                {
                    return;
                }

                P400 p400 = dbma1.P400s.Where(c => c.reserveSN == reserveSN).First();
                p400.senderCancelReserveDate = DateTime.Now;
                p400.senderCancelReserveReasonTypeSN = cancelReasonTypeSN;

                dbma1.SubmitChanges();
            }
        }
        #endregion 

        #region P40207 获取预约费用，余额
        public string FP40207()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //获取预约费用
                decimal reserveFee = Convert.ToDecimal(dbma1.A026s.First().assetsReserveCost);

                //获取余额
                decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                return string.Format("{{\"reserveFee\":\"{0}\",\"balance\":\"{1}\"}}", reserveFee, balance);
            }
        }
        #endregion 
    }
}
