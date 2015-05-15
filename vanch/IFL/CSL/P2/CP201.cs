using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P2
{
    //发布债权
    public class CP201
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP20101 债权融资页面初始化
        /// <summary>
        /// 债权融资页面初始化
        /// </summary>
        public string FP20101(int pageSize)
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //判断是否有权限并且是否有逾期账单
                var data1 = dbma1.U001s.Where(c => c.userSN == userSN).First();
                bool ifBillOverdue = C201.FC20153(dbma1, userSN);
                if (data1.creditRightFinancingStatus != true || ifBillOverdue == true)
                {
                    return "{\"status\":\"false\"}";
                }

                //获取uip数据
                string provinceData = C201.FC20121(dbma1).Replace("[","").Replace("]","");
                string guaranteeType = C201.FC20117(dbma1).Replace("[", "").Replace("]", "");
                string assetsType = C201.FC20151(dbma1).Replace("[", "").Replace("]", "");
                string creditStatusType = C201.FC20105(dbma1).Replace("[", "").Replace("]", "");
                string industryType = C201.FC20113(dbma1).Replace("[", "").Replace("]", "");
                string repaymentType = C201.FC20114(dbma1).Replace("[", "").Replace("]", "");
                string capitalPurposeType = C201.FC20115(dbma1).Replace("[", "").Replace("]", "");
                string repaymentSourceType = C201.FC20116(dbma1).Replace("[", "").Replace("]", "");
                string assetsSourceType = C201.FC20109(dbma1).Replace("[", "").Replace("]", "");
                string useStatusType = C201.FC20110(dbma1).Replace("[", "").Replace("]", "");
                string morgageType = C201.FC20112(dbma1).Replace("[", "").Replace("]", "");
                string financierCancelReserveReasonType = C201.FC20130(dbma1).Replace("[", "").Replace("]", "");
                string financierRefuseReserveReasonType = C201.FC20131(dbma1).Replace("[", "").Replace("]", "");

                string financingParams = C201.FC20124(dbma1);
                //string res = string.Format("{{\"SltConfigData\":[{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}],{11}}}", provinceData, guaranteeType, assetsType, creditStatusType, industryType, repaymentType, capitalPurposeType, repaymentSourceType, assetsSourceType, useStatusType, morgageType, financingParams);


                //获取已发布的债权
                string crListStr = GetCrFirst(dbma1, userSN, pageSize);

                string userName = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                string res = string.Format("{{\"status\":\"true\",\"uipData\":{{\"SltConfigData\":[{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}],{13}}},\"crData\":{14},\"maxDatetime\":\"{15}\",\"userName\":\"{16}\",\"userSN\":\"{17}\"}}", provinceData, guaranteeType, assetsType, creditStatusType, industryType, repaymentType, capitalPurposeType, repaymentSourceType, assetsSourceType, useStatusType, morgageType, financierCancelReserveReasonType, financierRefuseReserveReasonType, financingParams, crListStr, DateTime.Now, userName, userSN);

                return res;
            }
        }

        //首次获取发布的债权
        private string GetCrFirst(DBMA1DataContext dbma1, string userSN, int pageSize)
        {
            var crList = (from c in dbma1.VP101001s
                          where c.publisherUserSN == userSN
                          orderby c.publishDate descending
                          select new
                          {
                              No = c.creditRightSN,
                              BorrowAmt = c.financingAmount,
                              AssureWay = c.guaranteeType,
                              BorrowLimit = c.loanDays,
                              PaymentWay = c.repaymentType,
                              DayRate = c.dailyRate,
                              PawnName = c.collateralType,
                              PawnRate = c.mortgageRate,
                              BorrowBody = c.mainFinancing,
                              Area1 = c.province,
                              Area2 = c.city == null ? "" : c.city,
                              FundPurpose = c.capitalPurposeType,
                              PaymentSource = c.repaymentSourceType,
                              publishDate = c.publishDate
                          }).Take(pageSize).ToList();

            return C101.FC10107(crList);
        }
        #endregion

        #region FP20102 滚动获取发布债权
        /// <summary>
        /// 滚动获取发布债权
        /// </summary>
        public string FP20102(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string userSN = session["userSN"].ToString();
                //string userSN = "U00001";

                return GetCrNotFirst(dbma1, userSN, maxDatetime, pageFrom, pageSize);
            }
        }

        //非首次获取发布的债权
        private string GetCrNotFirst(DBMA1DataContext dbma1, string userSN, DateTime maxDatetime, int pageFrom, int pageSize)
        {
            var crList = (from c in dbma1.VP101001s
                          where c.publisherUserSN == userSN
                             && maxDatetime >= c.publishDate
                          orderby c.publishDate descending
                          select new
                          {
                              No = c.creditRightSN,
                              BorrowAmt = c.financingAmount,
                              AssureWay = c.guaranteeType,
                              BorrowLimit = c.loanDays,
                              PaymentWay = c.repaymentType,
                              DayRate = c.dailyRate,
                              PawnName = c.collateralType,
                              PawnRate = c.mortgageRate,
                              BorrowBody = c.mainFinancing,
                              Area1 = c.province,
                              Area2 = c.city == null ? "" : c.city,
                              FundPurpose = c.capitalPurposeType,
                              PaymentSource = c.repaymentSourceType
                          }).Skip(pageFrom).Take(pageSize).ToList();

            return C101.FC10107(crList);
        }
        #endregion

        #region FP20103 发布债权
        /// <summary>
        /// 发布债权
        /// </summary>
        public string FP20103(string financingData,string pawnData,string enterpriseData,string pwd)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string userSN = session["userSN"].ToString();

                //验证交易密码是否正确
                string transPwd = dbma1.U003s.Where(c => c.userSN == userSN).First().transactPwd;

                if (C101.FC10104(pwd, transPwd) == false)
                {
                    return "false";
                }

                //从余额中扣除服务费 F000
                A024 a024 = dbma1.A024s.First();
                decimal publishFee =  Convert.ToDecimal(a024.financePublishCost);

                F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                if (f000.balance < publishFee)
                {
                    return "false";
                }
                f000.balance -= publishFee;

                //加入收支明细表中 F003
                string max33SN = C101.FC10102("F003", 8, "UA");
                F003 f003 = new F003();
                f003.revenueExpenditureSN = max33SN;
                f003.generetorUserSN = userSN;
                f003.generateDate = DateTime.Now;
                f003.type = "债权发布";
                f003.expenditure = publishFee;
                f003.balance = f000.balance;
                dbma1.F003s.InsertOnSubmit(f003);

                //债权
                P200 p200 = Comm.C101.FC10108(financingData, typeof(P200)) as P200;
                p200.creditRightSN = C101.FC10102("P200",6,"E");
                p200.publisherUserSN = userSN;
                //p200.publisherUserSN = "U00001";
                p200.publishDate = DateTime.Now;
                dbma1.P200s.InsertOnSubmit(p200);

                //抵押物
                if(pawnData != "null")
                {
                    P201 p201 = Comm.C101.FC10108(pawnData, typeof(P201)) as P201;
                    p201.assetsSN = C101.FC10102("P201", 6, "FA");
                    p201.creditRightSN = p200.creditRightSN;
                    dbma1.P201s.InsertOnSubmit(p201);
                }

                //企业
                if (enterpriseData != "null")
                {
                    P202 p202 = Comm.C101.FC10108(enterpriseData, typeof(P202)) as P202;
                    p202.enterpriseSN = C101.FC10102("P202", 6, "FB");
                    p202.creditRightSN = p200.creditRightSN;
                    dbma1.P202s.InsertOnSubmit(p202);
                }

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = userSN;
                f006.businessSN = p200.creditRightSN;
                f006.businessType = "债权发布";
                f006.transactionMoneyAmount = publishFee;
                f006.groupUpValue = publishFee;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                var creditRight = dbma1.VP201001s.Where(c => c.creditRightSN == p200.creditRightSN).First();

                return C101.FC10107(creditRight);
            }
        }
        #endregion

        #region FP20106 取消发布的债权
        /// <summary>
        /// 取消发布的债权
        /// </summary>
        public void FP20106(string crSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //验证是否有效（没有被预约）
                var data1 = dbma1.P203s.Where(c => c.creditRightSN == crSN && c.senderCancelReserveDate == null && c.receiverRefuseReserveDate == null).FirstOrDefault();
                var data2 = dbma1.P100s.Where(c => c.creditRightSN == crSN && c.senderCancelReserveDate == null && c.receiverRefuseReserveDate == null).FirstOrDefault(); 
                if(data1 != null || data2 != null)
                {
                    return;
                }

                var cr = dbma1.P200s.Where(c => c.creditRightSN == crSN).First();
                cr.cancelDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region FP20107 获取推荐投资方列表
        /// <summary>
        /// 获取推荐投资方列表
        /// </summary>
        public string FP20107(string sortStr)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            string orderStr = string.Empty;
            string[] sortStrSplit = sortStr.Split(',');
            for(int i=1;i < (sortStrSplit.Length - 1);i++)
            {
                string[] sortStrSplitSplit = sortStrSplit[i].Split('#');
                if((i != 1))
                {
                    orderStr += ",";
                }
                orderStr += sortStrSplitSplit[0] == "1" ? " minInvestMoneyAmount" : " investCaseAmount";
                orderStr += sortStrSplitSplit[1] == "A" ? "" : " desc";
            }

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                List<RecommandInvestModel> recommandInvestDataList = null;

                if (orderStr == string.Empty)
                {
                    recommandInvestDataList = dbma1.ExecuteQuery<RecommandInvestModel>("select * from VP106011").ToList();
                }
                else
                {
                    string sqlStr = string.Format(@"select * from VP106011 order by {0}", orderStr);
                    recommandInvestDataList = dbma1.ExecuteQuery<RecommandInvestModel>(sqlStr).ToList();
                }

                string recommandInvestDataListStr = C101.FC10107(recommandInvestDataList);

                //账户余额
                F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                decimal balance = f000.balance;


                return string.Format("{{\"recommandInvestDataList\":{0},\"balance\":\"{1}\"}}", recommandInvestDataListStr, balance);
            }
        }
        #endregion

        #region FP20108 获取投资方详细信息
        /// <summary>
        /// 获取投资方详细信息
        /// </summary>
        public string FP20108(string investorUserSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var investorData = (from c in dbma1.VP801001s
                                    where c.userSN == investorUserSN
                                    select new
                                    {
                                        c.userSN,
                                        c.birthday,
                                        c.gender,
                                        c.registeredResidence,
                                        c.idCard,
                                        c.email,
                                        c.maritalStatusType,
                                        c.enterpriseType,
                                        c.hiredate,
                                        c.workTel,
                                        c.post,
                                        c.enterpriseSwitchboard,
                                        c.enterpriseWebsite,
                                        c.investMainType,
                                        c.financingMain,
                                        c.maxMortgageRate,
                                        c.guaranteeType,
                                        c.investProvince,
                                        c.investCity,
                                        c.minInvestMoneyAmount,
                                        c.minInterestRate,
                                        c.minInvestDays,
                                        c.maxInvestDays,
                                        c.collateralDemand
                                    }).First();
                                       

                var investStatus = dbma1.VP104011s.Where(c => c.investorUserSN == investorUserSN).First();

                string res = string.Format("{{\"investorData\":{0},\"investCaseAmount\":{1},\"investMoneyAmount\":{2}}}", C101.FC10107(investorData), investStatus.investCaseAmount, investStatus.InvestMoneyAmount);

                return res;
            }
        }
        #endregion

        #region FP20109 融资方发出预约
        /// <summary>
        /// 融资方发出预约
        /// </summary>
        public string FP20109(string pwd,string investorUserSN,string crSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string financierUserSN = session["userSN"].ToString();
                //string financierUserSN = "U00001";

                //检查有效性（1、债权没有被取消。2、债权没有被预约）
                var data1 = dbma1.P200s.Where(c => c.creditRightSN == crSN && c.cancelDate != null).FirstOrDefault();
                var data2 = dbma1.P203s.Where(c => c.creditRightSN == crSN && c.senderCancelReserveDate == null && c.receiverRefuseReserveDate == null).FirstOrDefault();
                var data3 = dbma1.P100s.Where(c => c.creditRightSN == crSN && c.senderCancelReserveDate == null && c.receiverRefuseReserveDate == null).FirstOrDefault();
                if (data1 != null || data2 != null || data3 != null)
                {
                    return "false";
                }

                //验证交易密码是否正确
                string transPwd = dbma1.U003s.Where(c => c.userSN == financierUserSN).First().transactPwd;

                if (C101.FC10104(pwd, transPwd) == false)
                {
                    return "false";
                }

                //从余额中扣除服务费 F000
                A024 a024 = dbma1.A024s.First();
                decimal investorRecommendCost = Convert.ToDecimal(a024.investorRecommendCost);

                F000 f000 = dbma1.F000s.Where(c => c.userSN == financierUserSN).First();
                if (f000.balance < investorRecommendCost)
                {
                    return "false";
                }
                f000.balance -= investorRecommendCost;

                //加入收支明细表中 F003
                string F003max33SN = C101.FC10102("F003", 8, "UA");
                F003 f003 = new F003();
                f003.revenueExpenditureSN = F003max33SN;
                f003.generetorUserSN = financierUserSN;
                f003.generateDate = DateTime.Now;
                f003.type = "投资方推荐";
                f003.expenditure = investorRecommendCost;
                f003.balance = f000.balance;
                dbma1.F003s.InsertOnSubmit(f003);

                //债权预约表添加记录 P203
                string max33SN = C101.FC10102("P203",7,"F");
                P203 p203 = new P203();
                p203.reserveSN = max33SN;
                p203.senderUserSN = financierUserSN;
                p203.receiverUserSN = investorUserSN;
                p203.creditRightSN = crSN;
                p203.sendDate = DateTime.Now;
                dbma1.P203s.InsertOnSubmit(p203);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = financierUserSN;
                f006.businessSN = max33SN;
                f006.businessType = "投资方推荐";
                f006.transactionMoneyAmount = investorRecommendCost;
                f006.groupUpValue = investorRecommendCost;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                U003 u003 = dbma1.U003s.Where(c => c.userSN == investorUserSN).First();

                //如有邮箱提醒
                if (u003.billGenerate_email == true)
                {
                    SendByEmail(dbma1, investorUserSN, crSN);
                }
                //如有短信提醒
                if (u003.billGenerate_shortMessage == true)
                {
                    SendBySM(dbma1, investorUserSN, crSN);
                }

                dbma1.SubmitChanges();

                return "true";
            }
        }

        //发送预约邮件提醒
        private int SendByEmail(DBMA1DataContext dbma1, string investorUserSN, string crSN)
        {
            string subject = "【凡奇金融】债权预约";
            string content = string.Format("亲爱的凡奇用户，您已经被融资方预约，债权编号为（{0}），请您查看。", crSN);
            return C101.FC10105(dbma1, investorUserSN, subject, content);
        }
        //发送预约短信提醒
        private void SendBySM(DBMA1DataContext dbma1, string investorUserSN, string crSN)
        {
            string phoneNum = dbma1.U000s.Where(c => c.userSN == investorUserSN).First().phone;

            string content = string.Format("亲爱的凡奇用户，您已经被融资方预约，债权编号为（{0}），请您查看。", crSN);
            Comm.SM.F001(dbma1, investorUserSN, phoneNum, content, 1, true);
        }
#endregion

        #region P20111 获取发布费用和账户余额
        public string FP20111()
        {
            string financierUserSN = session["userSN"].ToString();
            //string financierUserSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //发布费用
                A024 a024 = dbma1.A024s.First();
                decimal? publishFee = a024.financePublishCost;

                //账户余额
                F000 f000 = dbma1.F000s.Where(c => c.userSN == financierUserSN).First();
                decimal balance = f000.balance;

                return string.Format("{{\"publishFee\":\"{0}\",\"balance\":\"{1}\"}}", publishFee, balance);
            }
        }
        #endregion
    }

    public class RecommandInvestModel
    {
        private string _investorUserSN;
        private string _investMain;
        private decimal? _minInvestMoneyAmount;
        private string _province;
        private string _city;
        private bool _financingMain;
        private int _investCaseAmount;

        public string InvestorUserSN
        {
            get { return _investorUserSN; }
            set { _investorUserSN = value; }
        }

        public string InvestMain
        {
            get { return _investMain; }
            set { _investMain = value; }
        }

        public decimal? MinInvestMoneyAmount
        {
            get { return _minInvestMoneyAmount; }
            set { _minInvestMoneyAmount = value; }
        }

        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public bool FinancingMain
        {
            get { return _financingMain; }
            set { _financingMain = value; }
        }

        public int InvestCaseAmount
        {
            get { return _investCaseAmount; }
            set { _investCaseAmount = value; }
        }
    }
}



