using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P5
{
    //预约中债权
    public class CP502
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P50201 初始化
        public string FP50201(int pageSize)
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
                if (data.consultantStatus != true || ifBillOverdue == true)
                {
                    return "{\"status\":\"false\"}";
                }

                //用户名
                string name = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                //select信息
                string assetsType = C201.FC20108(dbma1).Replace("[", "").Replace("]", "");
                string consultantRefuseReserveReasonType = C201.FC20135(dbma1).Replace("[", "").Replace("]", "");
                string consultantAuditNotPassReasonType = C201.FC20136(dbma1).Replace("[", "").Replace("]", "");

                //预约中债权
                var crDataList = (from c in dbma1.VP502001s
                                  where c.consultantUserSN == userSN
                                  orderby c.sendTime descending
                                  select c).Take(pageSize).ToList();
                string crDataListStr = C101.FC10107(crDataList);

                return string.Format("{{\"status\":\"true\",\"name\":\"{0}\",\"SltConfigData\":[{1},{2},{3}],\"crDataList\":{4},\"maxDatetime\":\"{5}\",\"userSN\":\"{6}\"}}", name, assetsType, consultantRefuseReserveReasonType, consultantAuditNotPassReasonType, crDataListStr, DateTime.Now, userSN);
            }
        }
        #endregion

        #region P50203 滚动加载
        public string FP50203(DateTime maxDatetime,int pageFrom,int pageSize)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var crDataList = (from c in dbma1.VP502001s
                                  where c.consultantUserSN == consultantUserSN
                                    && maxDatetime >= c.sendTime
                                  orderby c.sendTime descending
                                  select c).Skip(pageFrom).Take(pageSize).ToList();
                return C101.FC10107(crDataList);
            }
        }
        #endregion

        #region P50202 接受预约
        public void FP50202(string reserveSN)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、未被接受。2、顾问预约未被取消或者拒绝。3、债权预约未被取消或者拒绝。）
                var data1 = dbma1.P500s.Where(c => c.reserveSN == reserveSN).FirstOrDefault();
                var data2 = dbma1.P101s.Where(c => c.reserveSN == reserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var crReserveSN = dbma1.P101s.Where(c => c.reserveSN == reserveSN).First().creditRightReserveSN;
                var data3 = dbma1.P203s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var data4 = dbma1.P100s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                if(data1 != null || data2 != null || data3 != null || data4 != null)
                {
                    return;
                }

                var linqData = dbma1.P101s.Where(c => c.reserveSN == reserveSN).First();
                string investorUserSN = linqData.senderUserSN;
                string crSN = linqData.creditRightSN;

                string max33SN = C101.FC10102("P500", 6, "K");
                P500 p500 = new P500();
                p500.serverSN = max33SN;
                p500.reserveSN = reserveSN;
                p500.consultantUserSN = consultantUserSN;
                p500.investorUserSN = investorUserSN;
                p500.creditRightSN = crSN;
                p500.acceptReserveDate = DateTime.Now;

                dbma1.P500s.InsertOnSubmit(p500);
                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region P50208 拒绝预约
        public void FP50208(string reserveSN,string refuseReasonTypeSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、未被接受。2、顾问预约未被取消或者拒绝。3、债权预约未被取消或者拒绝。）
                var data1 = dbma1.P500s.Where(c => c.reserveSN == reserveSN).FirstOrDefault();
                var data2 = dbma1.P101s.Where(c => c.reserveSN == reserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var crReserveSN = dbma1.P101s.Where(c => c.reserveSN == reserveSN).First().creditRightReserveSN;
                var data3 = dbma1.P203s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var data4 = dbma1.P100s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                if (data1 != null || data2 != null || data3 != null || data4 != null)
                {
                    return;
                }

                var p101 = dbma1.P101s.Where(c => c.reserveSN == reserveSN).First();
                p101.receiverRefuseReserveReasonTypeSN = refuseReasonTypeSN;
                p101.receiverRefuseReserveDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region P50205 抵押物信息
        public string FP50205(string reserveSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string crSN = dbma1.P101s.Where(c => c.reserveSN == reserveSN).First().creditRightSN;

                return IFL.Comm.C201.FC20141(crSN);
            }
        }
        #endregion

        #region P50207 企业信息
        public string FP50207(string reserveSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string crSN = dbma1.P101s.Where(c => c.reserveSN == reserveSN).First().creditRightSN;

                return IFL.Comm.C201.FC20142(crSN);
            }
        }
        #endregion
    }
}
