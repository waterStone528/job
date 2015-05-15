using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P2
{
    public class CP202
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP20203 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public string FP20203(int pageSize)
        {
            string financierUserSN = session["userSN"].ToString();
            //string financierUserSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string dateStr = GetReserveCrFirst(dbma1, financierUserSN, pageSize);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dateStr, DateTime.Now);
            }
        }

        //首次加载预约
        private string GetReserveCrFirst(DBMA1DataContext dbma1, string financierUserSN, int pageSize)
        {
            var reserveCrList = (from c in dbma1.VP202011s
                                 where c.financierUserSN == financierUserSN
                                 orderby c.reserveDate descending
                                 select c).Take(pageSize).ToList();

            return C101.FC10107(reserveCrList);
        }
        #endregion 

        #region FP20205 查看财务顾问信息
        //查看财务顾问信息
        public string FP20205(string consultantUserSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //基本信息
                var consultantBasicData = (from c in dbma1.VP801001s
                                           where c.userSN == consultantUserSN
                                           select new
                                           {
                                               c.userSN,
                                               c.name,
                                               c.birthday,
                                               c.gender,
                                               c.registeredResidence,
                                               idCard = c.idCard.Substring(0, 6),
                                               c.phone,
                                               c.email,
                                               c.maritalStatusType,
                                               c.procreateStatus,
                                               c.currentAddressProvince,
                                               c.currentAddressCity,
                                               c.currentAddressDetails,
                                               c.graduateSchool,
                                               c.degreeType,
                                               c.industryType,
                                               c.enterpriseType,
                                               c.serviceProvince,
                                               c.serviceCity,
                                               c.investigate,
                                               c.assetsEvaluate,
                                               c.badLoanCollect,
                                               c.creditRightGuarantee,
                                               c.consultantDetails
                                           }).First();
                string consultantBasicDataStr = C101.FC10107(consultantBasicData);

                //注册时间
                DateTime regDatetime = dbma1.U000s.Where(c => c.userSN == consultantUserSN).First().registerDate;
                string regDatetimeStr = C101.FC10107(regDatetime);

                //案例数量
                int caseAmount = (from c in dbma1.P500s
                                  where c.consultantUserSN == consultantUserSN
                                  select c).Count();

                //成功案例
                int successCaseAmount = (from c in dbma1.P102s
                                         join o in dbma1.P103s.Where(oo => oo.ifOverdue == false) on c.investSN equals o.investSN
                                         join p in dbma1.P500s.Where(pp => pp.consultantUserSN == consultantUserSN && pp.auditStatus == true) on c.reserveSN equals p.reserveSN
                                         select c).Count();

                //当前坏账
                int currentBadLoanCaseAmount = (from c in dbma1.P500s.Where(cc => cc.consultantUserSN == consultantUserSN && cc.auditStatus != false)
                                                join o in dbma1.P102s.Where(oo => oo.closeCaseDate == null && oo.deadlineDate.Date < DateTime.Now.Date)
                                                on c.reserveSN equals o.reserveSN
                                                select c).Count();

                //历史坏账
                int historyBadLoanCaseAmount = (from c in dbma1.P102s
                                                join o in dbma1.P103s.Where(oo => oo.ifOverdue == true) on c.investSN equals o.investSN
                                                join p in dbma1.P500s.Where(pp => pp.consultantUserSN == consultantUserSN && pp.auditStatus != false) on c.reserveSN equals p.reserveSN
                                                select c).Count();

                //坏账数量
                int badLoanCaseAmount = currentBadLoanCaseAmount + historyBadLoanCaseAmount;

                return string.Format("{{\"consultantBasicData\":{0},\"regDatetime\":{1},\"caseAmount\":\"{2}\",\"successCaseAmount\":\"{3}\",\"badLoanCaseAmount\":\"{4}\",\"currentBadLoanCaseAmount\":\"{5}\"}}", consultantBasicDataStr, regDatetimeStr, caseAmount, successCaseAmount, badLoanCaseAmount, currentBadLoanCaseAmount);
            }
        }
        #endregion 

        #region FP20209 滚动加载
        /// <summary>
        /// 滚动加载
        /// </summary>
        public string FP20209(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string financierUserSN = session["userSN"].ToString();
            //string financierUserSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetReserveCrNotFirst(dbma1, financierUserSN, maxDatetime, pageFrom, pageSize);
            }
        }

        //非首次加载预约
        private string GetReserveCrNotFirst(DBMA1DataContext dbma1, string financierUserSN, DateTime maxDatetime, int pageFrom, int pageSize)
        {
            var reserveCrList = (from c in dbma1.VP202011s
                                 where c.financierUserSN == financierUserSN
                                  && maxDatetime >= c.reserveDate
                                 orderby c.reserveDate descending
                                 select c).Skip(pageFrom).Take(pageSize).ToList();

            return C101.FC10107(reserveCrList);
        }
        #endregion

        #region FP20207 融资方拒绝债权预约
        /// <summary>
        /// 融资方拒绝债权预约
        /// </summary>
        public void FP20207(string crSN,string refuseReasonTypeSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、没有被投资。2、没有被拒绝）
                var data1 = dbma1.P102s.Where(c => c.creditRightSN == crSN).FirstOrDefault();
                if(data1 != null)
                {
                    return;
                }

                P100 p100 = (from c in dbma1.P100s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                             select c).First();

                p100.receiverRefuseReserveDate = DateTime.Now;
                p100.receiverRefuseReserveReasonTypeSN = refuseReasonTypeSN;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region FP20208 融资取消预约
        /// <summary>
        /// 融资取消预约
        /// </summary>
        public void FP20208(string crSN,string cancelReasonTypeSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、没有被投资。2、没有被取消）
                var data1 = dbma1.P102s.Where(c => c.creditRightSN == crSN).FirstOrDefault();
                if (data1 != null)
                {
                    return;
                }

                P203 p203 = (from c in dbma1.P203s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                             select c).First();

                p203.senderCancelReserveDate = DateTime.Now;
                p203.senderCancelReserveReasonTypeSN = cancelReasonTypeSN;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region FP20210 删除预约（投资方拒绝）
        /// <summary>
        /// 删除预约（投资方拒绝）
        /// </summary>
        public void FP20210(string crSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                P203 p203 = (from c in dbma1.P203s
                             where c.creditRightSN == crSN
                                 && c.receiverRefuseReserveDate != null
                                 && c.senderDeleteReserveDate == null
                             select c).FirstOrDefault();

                if(p203 == null)
                {
                    return;
                }

                p203.senderDeleteReserveDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion 

        #region FP20211 删除预约（投资方取消）
        /// <summary>
        /// 删除预约（投资方取消）
        /// </summary>
        public void FP20211(string crSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                P100 p100 = (from c in dbma1.P100s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate != null
                                 && c.receiverDeleteReserveDate == null
                             select c).FirstOrDefault();

                if (p100 == null)
                {
                    return;
                }

                p100.receiverDeleteReserveDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
