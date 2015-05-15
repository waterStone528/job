using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P1
{
    public class CP102
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP10203 初始化加载预约中债权
        /// <summary>
        /// 初始化加载预约中债权
        /// </summary>
        public string FP10203(int pageSize)
        {
            string investorUserSN = session["userSN"].ToString();
            //string investorUserSN = "U00002";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string dataStr = GetReserveCrFirst(dbma1, investorUserSN, pageSize);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dataStr, DateTime.Now);
            }
        }

        //首次获取预约中债权
        private string GetReserveCrFirst(DBMA1DataContext dbma1, string investorUserSN, int pageSize)
        {
            var reserveCrList = (from c in dbma1.VP102011s
                                 where c.investorUserSN == investorUserSN
                                 orderby c.reserveDate descending
                                 select c).Take(pageSize).ToList();

            return C101.FC10107(reserveCrList);
        }
        #endregion 

        #region FP10214 滚动加载
        /// <summary>
        /// 滚动加载
        /// </summary>
        public string FP10214(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string investorUserSN = session["userSN"].ToString();
            //string investorUserSN = "U00002";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetReserveCrNotFirst(dbma1, investorUserSN, maxDatetime, pageFrom, pageSize);
            }
        }

        //非首次加载预约
        private string GetReserveCrNotFirst(DBMA1DataContext dbma1, string investorUserSN, DateTime maxDatetime, int pageFrom, int pageSize)
        {
            var reserveCrList = (from c in dbma1.VP102011s
                                 where c.investorUserSN == investorUserSN
                                  && maxDatetime >= c.reserveDate
                                 orderby c.reserveDate descending
                                 select c).Skip(pageFrom).Take(pageSize).ToList();

            return C101.FC10107(reserveCrList);
        }
        #endregion 

        #region FP10207 取消预约
        /// <summary>
        /// 取消预约
        /// </summary>
        public void FP10207(string crSN,string cancelReasonTypeSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、没有被投资。）
                var data1 = dbma1.P102s.Where(c => c.creditRightSN == crSN).FirstOrDefault();
                if(data1 != null)
                {
                    return;
                }

                P100 p100 = (from c in dbma1.P100s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                             select c).FirstOrDefault();
                if(p100 == null)
                {
                    return;
                }
                p100.senderCancelReserveDate = DateTime.Now;
                p100.senderCancelReserveReasonTypeSN = cancelReasonTypeSN;

                //P101 p101 = (from c in dbma1.P101s
                //             where c.creditRightReserveSN == p100.reserveSN
                //                 && c.senderCancelReserveDate == null
                //             select c).FirstOrDefault();
                //if (p101 != null)
                //{
                //    p101.senderCancelReserveDate = DateTime.Now;
                //}

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region FP10216 拒绝预约
        /// <summary>
        /// 拒绝预约
        /// </summary>
        public void FP10216(string crSN, string refuseReasonTypeSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、没有被投资。）
                var data1 = dbma1.P102s.Where(c => c.creditRightSN == crSN).FirstOrDefault();
                if (data1 != null)
                {
                    return;
                }

                P203 p203 = (from c in dbma1.P203s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                             select c).FirstOrDefault();
                if(p203 == null)
                {
                    return;
                }
                p203.receiverRefuseReserveDate = DateTime.Now;
                p203.receiverRefuseReserveReasonTypeSN = refuseReasonTypeSN;

                //P101 p101 = (from c in dbma1.P101s
                //             where c.creditRightReserveSN == p203.reserveSN
                //                 && c.senderCancelReserveDate == null
                //             select c).FirstOrDefault();
                //if (p101 != null)
                //{
                //    p101.senderCancelReserveDate = DateTime.Now;
                //}

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region FP10215 删除预约 (融资方拒绝)
        /// <summary>
        /// 删除预约 (融资方拒绝)
        /// </summary>
        public void FP10215(string crSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                P100 p100 = (from c in dbma1.P100s
                             where c.creditRightSN == crSN
                                 && c.receiverRefuseReserveDate != null
                                 && c.senderDeleteReserveDate == null
                             select c).FirstOrDefault();
                if(p100 == null)
                {
                    return;
                }
                p100.senderDeleteReserveDate = DateTime.Now;

                //P101 p101 = (from c in dbma1.P101s
                //             where c.creditRightReserveSN == p100.reserveSN
                //                 && c.senderCancelReserveDate == null
                //             select c).FirstOrDefault();
                //if (p101 != null)
                //{
                //    p101.senderCancelReserveDate = DateTime.Now;
                //}

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region FP10217 删除拒绝预约 （融资方取消）
        /// <summary>
        /// 删除拒绝预约 （融资方取消）
        /// </summary>
        public void FP10217(string crSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                P203 p203 = (from c in dbma1.P203s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate != null
                                 && c.receiverDeleteReserveDate == null
                             select c).FirstOrDefault();
                if(p203 == null)
                {
                    return;
                }
                p203.receiverDeleteReserveDate = DateTime.Now;

                //P101 p101 = (from c in dbma1.P101s
                //             where c.creditRightReserveSN == p203.reserveSN
                //                 && c.senderCancelReserveDate == null
                //             select c).FirstOrDefault();
                //if (p101 != null)
                //{
                //    p101.senderCancelReserveDate = DateTime.Now;
                //}

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region FP10204 投资
        /// <summary>
        /// 投资
        /// </summary>
        public void FP10204(string financierUserSN,string crSN,decimal investMoneyAmount,DateTime investDate,DateTime dealineDate,string repaymentTypeSN,decimal dailyRate)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、未被投资。2、债权未取消或者拒绝.）
                var data1 = dbma1.P102s.Where(c => c.creditRightSN == crSN).FirstOrDefault();
                if(data1 != null)
                {
                    return;
                }
         
                string reserveSN;
                var reserveCrP203 = (from c in dbma1.P203s
                                 where c.creditRightSN == crSN
                                     && c.senderCancelReserveDate == null
                                     && c.receiverRefuseReserveDate == null
                                 select c).FirstOrDefault();
                if (reserveCrP203 == null)
                {
                    var reserveCrP100 = (from c in dbma1.P100s
                                         where c.creditRightSN == crSN
                                             && c.senderCancelReserveDate == null
                                             && c.receiverRefuseReserveDate == null
                                         select c).FirstOrDefault();
                    //债权已被取消或者拒绝
                    if (reserveCrP100 == null)
                    {
                        return;
                    }

                    reserveSN = reserveCrP100.reserveSN;
                }
                else
                {
                    reserveSN = reserveCrP203.reserveSN;
                }

                string investorUserSN = session["userSN"].ToString();

                string max33 = C101.FC10102("P102", 6, "C");
                P102 p102 = new P102();
                p102.investSN = max33;
                p102.reserveSN = reserveSN;
                p102.investorUserSN = investorUserSN;
                p102.financierUserSN = financierUserSN;
                p102.creditRightSN = crSN;
                p102.investMoneyAmount = investMoneyAmount;
                p102.investDate = investDate;
                p102.deadlineDate = dealineDate;
                p102.repaymentTypeSN = repaymentTypeSN;
                p102.dailyRate = dailyRate;
                p102.verifyInvestDate = DateTime.Now;
                dbma1.P102s.InsertOnSubmit(p102);

                //产生账单
                A024 a024 = dbma1.A024s.First();
                decimal feeRate = Convert.ToDecimal(a024.serviceRateDaily);
                decimal minRate = Convert.ToDecimal(a024.minServiceRateTotel);
                decimal maxRate = Convert.ToDecimal(a024.maxServiceRateTotel);

                int loanDays = (dealineDate - investDate).Days;
                int loanMonth = loanDays / 30 + (loanDays % 30 == 0 ? 0 : 1);
                decimal serverRate = Convert.ToDecimal(feeRate * Convert.ToDecimal(loanMonth));
                serverRate = serverRate < minRate ? minRate : serverRate;
                serverRate = serverRate > maxRate ? maxRate : serverRate;

                string F001max33SN = C101.FC10102("F001", 6, "L");
                F001 f001 = new F001();
                f001.billSN = F001max33SN;
                f001.payerUserSN = financierUserSN;
                f001.businessSN = crSN;
                f001.billType = "债权融资";
                f001.MoneyAmount = serverRate * investMoneyAmount * 10000;
                f001.generateDate = DateTime.Now;
                dbma1.F001s.InsertOnSubmit(f001);

                //融资方历史信息
                U002 u002 = dbma1.U002s.Where(c => c.userSN == financierUserSN).First();
                P105 p105 = new P105();
                string max33SN = C101.FC10102("P105",7,"CB");
                //案例情况
                var caseStatus = dbma1.P102s.Where(c => c.financierUserSN == financierUserSN);
                short caseAmount = Convert.ToInt16(caseStatus.Count());
                decimal caseMoneyAmount = caseAmount == 0 ? 0 : caseStatus.Sum(c => c.investMoneyAmount);

                //当前债务情况
                var debtStatus = dbma1.P102s.Where(c => c.financierUserSN == financierUserSN && c.closeCaseDate == null);
                short debtAmount = Convert.ToInt16(debtStatus.Count());
                decimal debtMoneyAmount = debtAmount == 0 ? 0 : debtStatus.Sum(c => c.investMoneyAmount);

                //当前逾期情况
                var currentOverdueStatus = from c in dbma1.P102s
                                           where c.financierUserSN == financierUserSN
                                            && c.closeCaseDate == null
                                            && DateTime.Now > c.deadlineDate
                                           select c;
                short currentOverdueAmount = Convert.ToInt16(currentOverdueStatus.Count());
                decimal currentOverdueMoneyAmount = currentOverdueAmount == 0 ? 0 : currentOverdueStatus.Sum(c => c.investMoneyAmount);

                //历史逾期数量
                var historyOverdueStatus = from c in dbma1.P102s
                                           from p in c.P103s
                                           where c.financierUserSN == financierUserSN
                                            && c.deadlineDate < p.repayDate
                                            && c.investSN == p.investSN
                                           select c;
                short historyOverdueAmount = Convert.ToInt16(historyOverdueStatus.Count());

                p105.financierHistoryInfoSN = max33SN;
                p105.creditRightSN = crSN;
                p105.investSN = max33;
                p105.historyDate = DateTime.Now;
                p105.userSN = u002.userSN;
                p105.name = u002.name;
                p105.birthday = u002.birthday;
                p105.gender = u002.gender;
                p105.registeredResidence= u002.registeredResidence;
                p105.idCard = u002.idCard;
                p105.phone = u002.phone;
                p105.email = u002.email;
                p105.maritalStatusTypeSN = u002.maritalStatusTypeSN;
                p105.procreateStatus= u002.procreateStatus;
                p105.healthyStatusTypeSN = u002.healthyStatusTypeSN;
                p105.ifBasicLivingAllowance= u002.ifBasicLivingAllowance;
                p105.currentAddressProvinceSN = u002.currentAddressProvinceSN;
                p105.currentAddressCitySN = u002.currentAddressCitySN;
                p105.currentAddressDetails = u002.currentAddressDetails;
                p105.graduateSchool = u002.graduateSchool;
                p105.degreeTypeSN = u002.degreeTypeSN;
                p105.degreeCard = u002.degreeCard;
                p105.friendName = u002.friendName;
                p105.friendPhone = u002.friendPhone;
                p105.spouseName = u002.spouseName;
                p105.spousePhone = u002.spousePhone;
                p105.spouseIdCard = u002.spouseIdCard;
                p105.spouseEnterprise = u002.spouseEnterprise;
                p105.kinName = u002.kinName;
                p105.kinRelationshipTypeSN = u002.kinRelationshipTypeSN;
                p105.kinPhone = u002.kinPhone;
                p105.kinIdCard = u002.kinIdCard;
                p105.kinEnterprise = u002.kinEnterprise;
                p105.workEnterprise = u002.workEnterprise;
                p105.enterpriseTypeSN = u002.enterpriseTypeSN;
                p105.hiredate = u002.hiredate;
                p105.workTel = u002.workTel;
                p105.post = u002.post;
                p105.enterpriseSwitchboard = u002.enterpriseSwitchboard;
                p105.enterpriseWebsite = u002.enterpriseWebsite;
                p105.colleageName = u002.colleageName;
                p105.colleagePhone = u002.colleagePhone;
                p105.monthlyTotalIncome = u002.monthlyTotalIncome;
                p105.monthlyTotalExpenditure = u002.monthlyTotalExpenditure;
                p105.monthlyNetIncome = u002.monthlyNetIncome;
                p105.totalAssets = u002.totalAssets;
                p105.totalDebt = u002.totalDebt;
                p105.ifCourtImplementation = u002.ifCourtImplementation;
                p105.creditStatus = u002.creditStatus;
                p105.caseAmount = caseAmount;
                p105.caseMoneyAmount = caseMoneyAmount;
                p105.currentDebtAmount = debtAmount;
                p105.debtMoneyAmount = debtMoneyAmount;
                p105.currentOverdue = currentOverdueAmount;
                p105.overdueMoneyAmount = currentOverdueMoneyAmount;
                p105.historyOverdueAmount = historyOverdueAmount;
                dbma1.P105s.InsertOnSubmit(p105);

                dbma1.SubmitChanges();

                U003 u003 = dbma1.U003s.Where(c => c.userSN == financierUserSN).First();

                //如有邮箱提醒
                if (u003.billGenerate_email == true)
                {
                    SendBillEmail(dbma1, financierUserSN, crSN, f001.billSN, Convert.ToDecimal(f001.MoneyAmount));
                }

                //如有短信提醒
                if (u003.billGenerate_shortMessage == true)
                {
                    SendBillSM(dbma1, financierUserSN, crSN, f001.billSN, Convert.ToDecimal(f001.MoneyAmount));
                }

                dbma1.SubmitChanges();
            }
        }

        //发送产生账单邮件
        private int SendBillEmail(DBMA1DataContext dbma1,string financierSN,string crSN,string billSN,decimal billMoneyAmount)
        {
            //获得账单逾期时间
            int needPayDays = Convert.ToInt32(dbma1.A028s.First().needPayDays);

            string subject = "【凡奇金融】账单提醒";
            string content = string.Format("亲爱的凡奇用户，您的债权({0})已经被投资并且产生账单，账单号为{1}，金额为{2}。请您在{3}天内付款，不然将产生罚息。",crSN,billSN,billMoneyAmount,needPayDays);
            return C101.FC10105(dbma1, financierSN, subject, content);
        }

        //发送产生账单短信
        private void SendBillSM(DBMA1DataContext dbma1, string financierSN, string crSN, string billSN, decimal billMoneyAmount)
        {
            //获得账单逾期时间
            int needPayDays = Convert.ToInt32(dbma1.A028s.First().needPayDays);
            string phoneNum = dbma1.U000s.Where(c => c.userSN == financierSN).First().phone;

            string content = string.Format("亲爱的凡奇用户，您的债权({0})已经被投资并且产生账单，账单号为{1}，金额为{2}。请您在{3}天内付款，不然将产生罚息。", crSN, billSN, billMoneyAmount, needPayDays);
            Comm.SM.F001(dbma1, financierSN, phoneNum, content, 1, true);
        }
        #endregion

        #region FP10210 获取选择顾问列表
        /// <summary>
        /// 获取选择顾问列表
        /// </summary>
        public string FP10210(string crSN, string sortStr)
        {
            string investorUserSN = session["userSN"].ToString();

            string orderStr = string.Empty;
            string[] sortStrSplit = sortStr.Split(',');
            for (int i = 1; i < (sortStrSplit.Length - 1); i++)
            {
                string[] sortStrSplitSplit = sortStrSplit[i].Split('#');
                if ((i != 1))
                {
                    orderStr += ",";
                }
                orderStr += sortStrSplitSplit[0] == "1" ? " successCaseAmount" : " caseAmount";
                orderStr += sortStrSplitSplit[1] == "A" ? "" : " desc";
            }

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                List<SelectConsultantList> consultantList = null;

                string sqlStr = string.Format("select VP509011.consultantUserSN,VP509011.serviceProvince,VP509011.serviceCity,VP509011.caseAmount,VP509011.successCaseAmount,A.qualificationSN from VP509011 left join (select consultantUserSN,qualificationSN from P104 where  investorUserSN = '{0}' and creditRightSN = '{1}') as A on VP509011.consultantUserSN = A.consultantUserSN", investorUserSN, crSN);

                if (orderStr == string.Empty)
                {
                    consultantList = dbma1.ExecuteQuery<SelectConsultantList>(sqlStr).ToList();
                }
                else
                {
                    sqlStr = string.Format("{0} order by {1}", sqlStr, orderStr);
                    consultantList = dbma1.ExecuteQuery<SelectConsultantList>(sqlStr).ToList();
                }

                //发布费用
                A023 a023 = dbma1.A023s.First();
                decimal? fee = a023.consultantReserveCost;

                //账户余额
                F000 f000 = dbma1.F000s.Where(c => c.userSN == investorUserSN).First();
                decimal balance = f000.balance;

                return string.Format("{{\"balance\":\"{0}\",\"fee\":\"{1}\",\"consultantDataList\":{2}}}", balance, fee, C101.FC10107(consultantList));
            }
        }
        #endregion

        #region FP10211 获得顾问隐藏后信息
        /// <summary>
        /// 获得顾问隐藏后信息
        /// </summary>
        public string FP10211(string consultantUserSN,int ifVisiable)
        {
            
                using (DBMA1DataContext dbma1 = new DBMA1DataContext())
                {
                    string consultantBasicDataStr = null;

                    //传递隐藏信息
                    if (ifVisiable == 0)
                    {
                        //基本信息
                        var consultantBasicData = (from c in dbma1.VP801001s
                                                   where c.userSN == consultantUserSN
                                                   select new
                                                   {
                                                       c.userSN,
                                                       c.birthday,
                                                       c.gender,
                                                       c.registeredResidence,
                                                       idCard = c.idCard.Substring(0, 6),
                                                       c.maritalStatusType,
                                                       c.procreateStatus,
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

                        consultantBasicDataStr = C101.FC10107(consultantBasicData);
                    }
                    else
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

                        consultantBasicDataStr = C101.FC10107(consultantBasicData);
                    }
                
                

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

        #region 获得顾问隐藏的信息
        /// <summary>
        /// 获得顾问隐藏的信息
        /// 0:操作失败 不返回失败原因;1:操作成功;2;操作失败 失败原因余额不足
        /// </summary>
        public string FP10213(string pwd,string consultantUserSN,string crSN)
        {
            string investorUserSN = session["userSN"].ToString();
            //string investorUserSN = "U00002";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、没有被投资。2、债权已经被取消或者拒绝）
                var data1 = dbma1.P102s.Where(c => c.creditRightSN == crSN).FirstOrDefault();
                var data2 = (from c in dbma1.P203s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                             select c).FirstOrDefault();
                var data3 = (from c in dbma1.P100s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                             select c).FirstOrDefault();
                if (data1 != null || (data2 == null && data3 == null))
                {
                    return "0";
                }

                ///验证交易密码是否正确
                string transPwd = dbma1.U003s.Where(c => c.userSN == investorUserSN).First().transactPwd;

                if (C101.FC10104(pwd, transPwd) == false)
                {
                    return "0";
                }

                //从余额中扣除服务费 F000
                A023 a023 = dbma1.A023s.First();
                decimal fee = Convert.ToDecimal(a023.consultantReserveCost);

                F000 f000 = dbma1.F000s.Where(c => c.userSN == investorUserSN).First();
                if (f000.balance < fee)
                {
                    return "2";
                }
                f000.balance -= fee;

                //加入收支明细表中 F003
                string F003max33SN = C101.FC10102("F003", 8, "UA");
                F003 f003 = new F003();
                f003.revenueExpenditureSN = F003max33SN;
                f003.generetorUserSN = investorUserSN;
                f003.generateDate = DateTime.Now;
                f003.type = "顾问预约";
                f003.expenditure = fee;
                f003.balance = f000.balance;
                dbma1.F003s.InsertOnSubmit(f003);

                //获取隐藏信息
                var consultantBasicData = (from c in dbma1.VP801001s
                                           where c.userSN == consultantUserSN
                                           select new
                                           {
                                               c.name,
                                               idCard = c.idCard.Substring(0, 6),
                                               c.phone,
                                               c.email,
                                               c.currentAddressProvince,
                                               c.currentAddressCity,
                                               c.currentAddressDetails
                                           }).First();
                string consultantBasicDataStr = C101.FC10107(consultantBasicData);

                //添加一条投资方查看顾问信息资格表记录，P104
                string max33SN = AddRecord(dbma1, investorUserSN, consultantUserSN, crSN);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = investorUserSN;
                f006.businessSN = max33SN;
                f006.businessType = "顾问预约";
                f006.transactionMoneyAmount = fee;
                f006.groupUpValue = fee;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                return string.Format("{{\"status\":\"{0}\",\"consultantHideData\":{1}}}", 1, consultantBasicDataStr);
            }
        }

        //添加一条投资方查看顾问信息资格表记录，P104
        private string AddRecord(DBMA1DataContext dbma1, string investorUserSN,string consultantUserSN,string crSN)
        {
            string max33SN = C101.FC10102("P104", 7, "CA");
            P104 p104 = new P104();
            p104.qualificationSN = max33SN;
            p104.investorUserSN = investorUserSN;
            p104.consultantUserSN = consultantUserSN;
            p104.creditRightSN = crSN;
            p104.operateDate = DateTime.Now;

            dbma1.P104s.InsertOnSubmit(p104);

            return max33SN;
        }

        #endregion

        #region P10301 确认预约财务顾问
        /// <summary>
        /// 确认预约财务顾问
        /// </summary>
        public void FP10301(string consultantUserSN,string crSN,decimal quotePricePercent)
        {
            string investorUserSN = session["userSN"].ToString();
            //string investorUserSN = "U00002";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检查有效性（1、没有被投资。）
                var data1 = dbma1.P102s.Where(c => c.creditRightSN == crSN).FirstOrDefault();
                if (data1 != null)
                {
                    return;
                }

                //获取reserveSN
                string crReserveSN = string.Empty;

                var linqData1 = (from c in dbma1.P203s
                                where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                                select c).FirstOrDefault();
                if(linqData1 == null)
                {
                   var linqData2 = (from c in dbma1.P100s
                                where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                                 && c.receiverRefuseReserveDate == null
                                select c).FirstOrDefault();

                   if (linqData2 == null)
                   {
                       return;
                   }

                   crReserveSN = linqData2.reserveSN;
                }
                else
                {
                    crReserveSN = linqData1.reserveSN;
                }

                string max33SN = C101.FC10102("P101", 7, "B");

                P101 p101 = new P101();
                p101.reserveSN = max33SN;
                p101.creditRightReserveSN = crReserveSN;
                p101.senderUserSN = investorUserSN;
                p101.receiverUserSN = consultantUserSN;
                p101.creditRightSN = crSN;
                p101.costPercent = quotePricePercent;
                p101.sendTime = DateTime.Now;
                dbma1.P101s.InsertOnSubmit(p101);
                dbma1.SubmitChanges();

                U003 u003 = dbma1.U003s.Where(c => c.userSN == consultantUserSN).First();

                //如有邮箱提醒
                if (u003.billGenerate_email == true)
                {
                    SendByEmail(dbma1, consultantUserSN, investorUserSN);
                }
                //如有短信提醒
                if (u003.billGenerate_shortMessage == true)
                {
                    SendBySM(dbma1, consultantUserSN, investorUserSN);
                }

                dbma1.SubmitChanges();
            }
        }

        //发送预约邮箱提醒
        private int SendByEmail(DBMA1DataContext dbma1, string consultantUserSN, string investorUserSN)
        {
            string subject = "【凡奇金融】顾问预约";
            string content = string.Format("亲爱的凡奇用户，您已经被用户{0}预约，请您查看。", investorUserSN);
            return C101.FC10105(dbma1, consultantUserSN, subject, content);
        }

        //发送预约短信提醒
        private void SendBySM(DBMA1DataContext dbma1, string consultantUserSN, string investorUserSN)
        {
            string phoneNum = dbma1.U000s.Where(c => c.userSN == consultantUserSN).First().phone;

            string content = string.Format("亲爱的凡奇用户，您已经被用户{0}预约，请您查看。", investorUserSN);
            Comm.SM.F001(dbma1, consultantUserSN, phoneNum, content, 1, true);
        }
        #endregion

        #region P10208 查看顾问信息
        public string FP10208(string crSN,string consultantUserSN)
        {
            string investorUserSN = session["userSN"].ToString();
            //string investorUserSN = "U00002";

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

                //报价
                decimal quotePricePercent = (from c in dbma1.P101s
                                             where c.senderUserSN == investorUserSN 
                                                && c.receiverUserSN == consultantUserSN 
                                                && c.creditRightSN == crSN
                                                && c.senderCancelReserveDate == null
                                             select c).OrderByDescending(c => c.sendTime).First().costPercent;

                decimal financingMoneyAmount = (from c in dbma1.P200s
                                                where c.creditRightSN == crSN
                                                select c).First().financingAmount;

                decimal quotePrice = financingMoneyAmount * quotePricePercent;

                return string.Format("{{\"consultantBasicData\":{0},\"regDatetime\":{1},\"caseAmount\":\"{2}\",\"successCaseAmount\":\"{3}\",\"badLoanCaseAmount\":\"{4}\",\"currentBadLoanCaseAmount\":\"{5}\",\"quotePrice\":\"{6}\"}}", consultantBasicDataStr, regDatetimeStr, caseAmount, successCaseAmount, badLoanCaseAmount, currentBadLoanCaseAmount, quotePrice);
            }
        }
        #endregion

        #region P10209  取消财务顾问
        public void FP10209(string crSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、未审核）
                var data1 = dbma1.P500s.Where(c => c.creditRightSN == crSN && c.auditStatus != null).FirstOrDefault();
                if(data1 != null)
                {
                    return;
                }

                P101 p101 = (from c in dbma1.P101s
                             where c.creditRightSN == crSN
                                 && c.senderCancelReserveDate == null
                             select c).FirstOrDefault();
                if(p101 == null)
                {
                    return;
                }

                p101.senderCancelReserveDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion 
    }

    //选择顾问列表
    public class SelectConsultantList
    {
        public string consultantUserSN;
        public string serviceProvince;
        public string serviceCity;
        public int? caseAmount;
        public int? successCaseAmount;
        public string qualificationSN;
    }
}
