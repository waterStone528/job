using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P5
{
    //审核中
    public class CP503
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P50301 初始化
        public string FP50301(int pageSize)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003"; 

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var linqData = (from c in dbma1.VP503001s
                                where c.consultantUserSN == consultantUserSN
                                orderby c.acceptReserveDate descending
                                select c).Take(pageSize).ToList();

                return string.Format("{{\"crDataList\":{0},\"maxDatetime\":\"{1}\"}}", C101.FC10107(linqData),DateTime.Now);
            }
        }
        #endregion

        #region P50302 滚动加载
        public string FP50302(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003"; 

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var crDataList = (from c in dbma1.VP503001s
                                  where c.consultantUserSN == consultantUserSN
                                    && maxDatetime >= c.acceptReserveDate
                                  orderby c.acceptReserveDate descending
                                  select c).Skip(pageFrom).Take(pageSize).ToList();
                return C101.FC10107(crDataList);
            }
        }
        #endregion

        #region P50306 抵押物信息
        public string FP50306(string serverSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string crSN = dbma1.P500s.Where(c => c.serverSN == serverSN).First().creditRightSN;

                return IFL.Comm.C201.FC20141(crSN);
            }
        }
        #endregion

        #region P50307 投资方信息
        public string FP50307(string serverSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string investorUserSN = dbma1.P500s.Where(c => c.serverSN == serverSN).First().investorUserSN;

                return C201.FC20144(investorUserSN);
            }
        }
        #endregion

        #region P50308 企业信息
        public string FP50308(string serverSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string crSN = dbma1.P500s.Where(c => c.serverSN == serverSN).First().creditRightSN;

                return IFL.Comm.C201.FC20142(crSN);
            }
        }
        #endregion

        #region P50303 审核通过
        public void FP50303(string serverSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查(1、未审核。2、未取消顾问预约。3、未取消债权预约)
                var data1 = dbma1.P500s.Where(c => c.serverSN == serverSN && c.auditStatus != null).FirstOrDefault();
                var consultantReserveSN = dbma1.P500s.Where(c => c.serverSN == serverSN).First().reserveSN;
                var data2 = dbma1.P101s.Where(c => c.reserveSN == consultantReserveSN && c.senderCancelReserveDate != null).FirstOrDefault();
                var crReserveSN = dbma1.P101s.Where(c => c.reserveSN == consultantReserveSN).First().creditRightReserveSN;
                var data3 = dbma1.P203s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var data4 = dbma1.P100s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                if(data1 != null || data2 != null || data3 != null || data4 != null)
                {
                    return;
                }

                P500 p500 = dbma1.P500s.Where(c => c.serverSN == serverSN).First();
                p500.auditStatus = true;
                p500.serverDate = DateTime.Now;
                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region P50309 审核未过
        public void FP50309(string serverSN,string auditNotPassReasonTypeSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查(1、未审核。2、未取消顾问预约。3、未取消债权预约)
                var data1 = dbma1.P500s.Where(c => c.serverSN == serverSN && c.auditStatus != null).FirstOrDefault();
                var consultantReserveSN = dbma1.P500s.Where(c => c.serverSN == serverSN).First().reserveSN;
                var data2 = dbma1.P101s.Where(c => c.reserveSN == consultantReserveSN && c.senderCancelReserveDate != null).FirstOrDefault();
                var crReserveSN = dbma1.P101s.Where(c => c.reserveSN == consultantReserveSN).First().creditRightReserveSN;
                var data3 = dbma1.P203s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                var data4 = dbma1.P100s.Where(c => c.reserveSN == crReserveSN && (c.senderCancelReserveDate != null || c.receiverRefuseReserveDate != null)).FirstOrDefault();
                if (data1 != null || data2 != null || data3 != null || data4 != null)
                {
                    return;
                }

                P500 p500 = dbma1.P500s.Where(c => c.serverSN == serverSN).First();
                p500.auditStatus = false;
                p500.consultantAuditNotPassReasonTypeSN = auditNotPassReasonTypeSN;
                p500.serverDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region P50310 删除
        public void FP50310(string serverSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                P500 p500 = dbma1.P500s.Where(c => c.serverSN == serverSN).First();
                p500.consultantDeleteCreditRightDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
