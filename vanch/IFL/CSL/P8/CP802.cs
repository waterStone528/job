using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P8
{
    public class CP802
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region 获取uip
        /// <summary>
        /// 获取uip
        /// </summary>
        public string FP80200()
        {
            if (session["userSN"] == null)
            {
                return "notLogin";
            }
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string provinceData = C201.FC20121(dbma1).Replace("[", "").Replace("]", "");
                string marital = C201.FC20100(dbma1).Replace("[", "").Replace("]", "");
                string enterpriseType = C201.FC20104(dbma1).Replace("[", "").Replace("]", "");
                string investMain = C201.FC20106(dbma1).Replace("[", "").Replace("]", "");
                string guaranteeType = C201.FC20117(dbma1).Replace("[", "").Replace("]", "");
                string assetsType = C201.FC20108(dbma1).Replace("[", "").Replace("]", "");
                string degreeType = C201.FC20102(dbma1).Replace("[", "").Replace("]", "");
                string healthStatus = C201.FC20101(dbma1).Replace("[", "").Replace("]", "");
                //string creditStatusType = C201.FC20105(dbma1).Replace("[", "").Replace("]", "");
                string industryType = C201.FC20113(dbma1).Replace("[", "").Replace("]", "");
                string cityData = C201.FC20148(dbma1);

                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();
                //当前成长值
                decimal currentGroupValue = Convert.ToInt32(dbma1.F006s.Where(c => c.userSN == userSN).Sum(c => c.groupUpValue));
                var userInfo = new { No = u002.userSN, Name = u002.name, Sex = u002.gender, BirthDay = u002.birthday, CardID = u002.idCard.Substring(0,6) + "*", Mobile = u002.phone, CardAddress = u002.registeredResidence, MyGrow = currentGroupValue };

                string userInfoStr = C101.FC10107(userInfo);

                var investParams = dbma1.A023s.First();
                var financingParams = dbma1.A024s.First();
                var consultantParams = dbma1.A025s.First();
                var purchaseParams = dbma1.A026s.First();
                var sellParams = dbma1.A027s.First();

                var PawnRate = new { Min = investParams.minMorgageRate, Max = investParams.maxMorgageRate };
                var InvestAmt = new { Min = investParams.minInvestMoneyAmount, Max = investParams.maxInvestMoneyAmount };
                var DayRate = new { Min = investParams.minDailyRate, Max = investParams.maxDailyRate };
                var InvestLimit = new { Min = investParams.minInvestDays, Max = investParams.maxInvestDays };
                var BayAmt = new { Min = purchaseParams.minPurchasePrice, Max = purchaseParams.maxPurchasePrice };
                //var MTIn = new {Min = null,Max = null};
                //var MTOut = new {Min = null,Max = null};
                //var MCIn = new { Min = null, MAx = null };
                var TAsset = new { Min = financingParams.minTotalAssets, Max = financingParams.maxTotalAssets };
                var TBorrow = new { Min = financingParams.minTotalLiability, Max = financingParams.maxTotalLiability };
                var CAsset = new { Min = financingParams.minTotalAssets, Max = financingParams.maxTotalAssets };

                string PawnRateStr = C101.FC10107(PawnRate);
                string InvestAmtStr = C101.FC10107(InvestAmt);
                string DayRateStr = C101.FC10107(DayRate);
                string InvestLimitStr = C101.FC10107(InvestLimit);
                string BayAmtStr = C101.FC10107(BayAmt);
                string TAssetStr = C101.FC10107(TAsset);
                string TBorrowStr = C101.FC10107(TBorrow);
                string CAssetStr = C101.FC10107(CAsset);

                U001 u001 = dbma1.U001s.Where(c => c.userSN == userSN).First();
                string investApplyStatus = u001.creditRightInvestApplyStauts == 2 ? "1" : "0";
                string financingApplyStatus = u001.creditRightFinancingApplyStatus == 2 ? "1" : "0";
                string sellApplyStatus = u001.assetsSellingApplyStatus == 2 ? "1" : "0";
                string purchaseApplyStatus = u001.assetsPurchaseApplyStatus == 2 ? "1" : "0";
                string consultantApplyStatus = u001.consultantApplyStatus == 2 ? "1" : "0";
                if (u001.consultantApplyStatus == 0)
                {
                    consultantApplyStatus = "0";
                }
                else if(u001.consultantApplyStatus == 1)
                {
                    consultantApplyStatus = "2";
                }
                else if (u001.consultantApplyStatus == 2)
                {
                    consultantApplyStatus = "1";
                }
                else if(u001.consultantApplyStatus == 3)
                {
                    consultantApplyStatus = "0";
                }

                string CreditInvestStr = "\"\"";
                string CreditBorrowStr = "\"\"";
                string AssetsSalesStr = "\"\"";
                string AssetsBuyStr = "\"\"";
                string AdviserServerStr = "\"\"";

                //投资
                if(investApplyStatus == "1")
                {
                    var userLinq = dbma1.P102s.Where(c => c.investorUserSN == userSN);

                    //债权总额
                    decimal crMoneyAmount = (from c in dbma1.P102s
                                             where c.investorUserSN == userSN
                                                //&& c.closeCaseDate == null
                                             select new
                                             {
                                                 c.investMoneyAmount
                                             }).ToList().Sum(r => r.investMoneyAmount);         

                    //债权数量
                    int crAmount = (from c in dbma1.P102s
                                    where c.investorUserSN == userSN
                                        //&& c.closeCaseDate == null
                                    select c).Count();

                    //投资收益
                    decimal investEarnings = (from c in userLinq
                                              from o in c.P103s
                                              where c.closeCaseDate != null
                                              select new
                                              {
                                                  earnings = (o.repayDate - c.investDate).Days * c.dailyRate * c.investMoneyAmount
                                              }).ToList().Sum(c => c.earnings);

                    var CreditInvest = new { TotalPrice = crMoneyAmount, TotalNum = crAmount, TotalIncome = investEarnings };
                    CreditInvestStr = C101.FC10107(CreditInvest);
                }
                //融资
                if(financingApplyStatus == "1")
                {
                    //正在融资
                    int financingAmount = (from c in dbma1.P200s
                                            where c.publisherUserSN == userSN
                                             && c.cancelDate == null
                                             && !dbma1.P102s.Any(o => o.creditRightSN == c.creditRightSN)
                                            select c).Count();

                    //正在还款
                    int repayingAmount = (from c in dbma1.P102s
                                          where c.financierUserSN == userSN
                                             && c.closeCaseDate == null
                                          select c).Count();

                    //即将逾期
                    //即将逾期天数
                    int aboutToOverdueDays = 30;
                    int aboutToOverdueAmount = (from c in dbma1.P102s
                                                where c.financierUserSN == userSN
                                                 && c.closeCaseDate == null
                                                 && DateTime.Now.AddDays(aboutToOverdueDays) > c.deadlineDate
                                                 && DateTime.Now <= c.deadlineDate
                                                select c).Count();

                    var CreditBorrow = new { Borrowing = financingAmount, Paying = repayingAmount, Overdue = aboutToOverdueAmount };
                    CreditBorrowStr = C101.FC10107(CreditBorrow);
                }
                //资产出售
                if (sellApplyStatus == "1")
                {
                    //已发布
                    int publishAssetsAmount = (from c in dbma1.VP401001s
                                              where c.publisherUserSN == userSN
                                              select c).Count();

                    //预约中
                    //int reservingAssetsAmount = (from c in dbma1.P400s
                    //                             where c.receiverUserSN == userSN
                    //                                 && c.senderCancelReserveDate == null
                    //                                 && c.receiverRefuseReserveDate == null
                    //                                 && !c.P401s.Any()
                    //                             select c).Count();
                    int reservingAssetsAmount = (from c in dbma1.VP402011s
                                                 where c.receiverUserSN == userSN
                                                 select c).Count();

                    //已成交
                    //int purchasedAssetsAmount = dbma1.P401s.Where(c => c.sellerUserSN == userSN).Count();
                    int purchasedAssetsAmount = (from c in dbma1.VP404001s
                                                 where c.sellerUserSN == userSN
                                                 && c.sellerDeleteDate == null
                                                 select c).Count();

                    var AssetsSales = new { Publish = publishAssetsAmount, Booking = reservingAssetsAmount, Close = purchasedAssetsAmount };
                    AssetsSalesStr = C101.FC10107(AssetsSales);
                }
                //资产购买
                if (purchaseApplyStatus == "1")
                {
                    //预约中
                    //int reservingAssetsAmount = (from c in dbma1.P400s
                    //                             where c.senderUserSN == userSN
                    //                                 && c.senderCancelReserveDate == null
                    //                                 && c.receiverRefuseReserveDate == null
                    //                                 && !c.P401s.Any()
                    //                             select c).Count();
                    int reservingAssetsAmount = (from c in dbma1.VP402011s
                                                 where c.senderUserSN == userSN
                                                 select c).Count();

                    //已成交
                    //int purchasedAssetsAmount = dbma1.P401s.Where(c => c.purchaserUserSN == userSN).Count();
                    int purchasedAssetsAmount = (from c in dbma1.VP404001s
                                                 where c.purchaserUserSN == userSN
                                                  && c.purchaserDeleteDate == null
                                                 select c).Count();


                    var AssetsBuy = new { Booking = reservingAssetsAmount, Close = purchasedAssetsAmount };
                    AssetsBuyStr = C101.FC10107(AssetsBuy);
                }
                //财务管理
                if (consultantApplyStatus == "1")
                {
                    //预约中
                    int reserveAmount = (from c in dbma1.VP502001s
                                         where c.consultantUserSN == userSN
                                         select c).Count();

                    //服务中
                    int servicingAmount = (from c in dbma1.VP503001s
                                           where c.consultantUserSN == userSN
                                           select c).Count();

                    //已结案
                    int servicedAmount = (from c in dbma1.VP504021s
                                          where c.consultantUserSN == userSN
                                          select c).Count();

                    ////已成交
                    //int investAmount = (from c in dbma1.P500s.Where(c => c.consultantUserSN == userSN && c.auditStatus != null)
                    //                    join o in dbma1.P101s on c.reserveSN equals o.reserveSN
                    //                    join p in dbma1.P102s on o.creditRightReserveSN equals p.reserveSN
                    //                    select c).Count();

                    var AdviserServer = new { Booking = reserveAmount, Serving = servicingAmount, Close = servicedAmount };
                    AdviserServerStr = C101.FC10107(AdviserServer);      
                }

                //财务管理
                decimal balanceV = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;
                decimal usedV = Convert.ToDecimal
                               (
                                    (from c in dbma1.F003s
                                     where c.generetorUserSN == userSN
                                         && c.expenditure != null
                                     select c.expenditure).Sum()
                               );

                int needPayBillAmount = (from c in dbma1.F001s
                                         where c.payerUserSN == userSN
                                             && !dbma1.F002s.Any(o => o.billSN == c.billSN)
                                         select c).Count();

                var cwData = new { RemainVB = balanceV, UsedVB = usedV, Payable = needPayBillAmount };
                string FinaceStr = C101.FC10107(cwData);

                //成长值
                string GrowUpInfoStr = GrowUp(dbma1, userSN, currentGroupValue);

                //安全管理
                int temp = 0;
                if (dbma1.U000s.Where(c => c.userSN == userSN).First().ifChangePwd == true) { temp++; }
                if (dbma1.U003s.Where(c => c.userSN == userSN).First().ifChangeTransactPwd == true) { temp++; }
                if (u002.ifChangeEmail == true) { temp++; }
                string level = string.Empty;
                if(temp == 0 || temp == 1)
                {
                    level = "低";
                }
                else if(temp == 2)
                {
                    level = "中";
                }
                else
                {
                    level = "高";
                }
                var SafeManage = new { Leval = level, Mobile = u002.phone, Email = u002.email == null ? "" : u002.email };
                string SafeManageStr = C101.FC10107(SafeManage);

                //服务费
                var SvrFee = new { Invest = investParams.openServerCost, Borrow = financingParams.openServerCost, AssetSale = sellParams.openServerCost, AssetBuy = purchaseParams.openServerCost, Adviser = consultantParams.openServerCost };
                string SvrFeeStr = C101.FC10107(SvrFee);

                //账单逾期及停止服务
                A028 a028 = dbma1.A028s.First();
                var SvrStopTip = new { Day = a028.serverStopDays, Rate = a028.serverStopRate };
                var OverdueTip = new { Day = Convert.ToInt32(a028.needPayDays), Rate = a028.overdueRateDaily };
                string SvrStopTipStr = C101.FC10107(SvrStopTip);
                string OverdueTipStr = C101.FC10107(OverdueTip);

                //注册奖励V币数
                decimal SentScore = Convert.ToInt32(dbma1.A031s.First().regPresentV);

                //账单情况
                string billStatus;
                //无未付账单
                if (needPayBillAmount == 0)
                {
                    billStatus = "0";
                }
                else
                {
                    var linq = (from c in dbma1.F001s
                                where c.payerUserSN == userSN
                                 && !dbma1.F002s.Any(o => o.billSN == c.billSN)
                                 && (DateTime.Now - c.generateDate).Days >=  OverdueTip.Day
                                select c).FirstOrDefault();

                    //付款未超过规定时间
                    if (linq == null)
                    {
                        billStatus = "1";
                    }
                    //付款超过规定时间
                    else
                    {
                        billStatus = "2";
                    }
                }

                //是否已经领取V币
                string ifGetV = (from c in dbma1.F004s
                                 where c.userSN == userSN
                                     && c.rewardType.Trim() == "注册赠送"
                                 select c).FirstOrDefault() == null ? "1" : "0";

                string res;
                res = string.Format("{{\"SltConfigData\":[{0},{1},{2},{3},{4},{5},{6},{7},{8}],\"PawnRate\":{9},\"InvestAmt\":{10},\"DayRate\":{11},\"InvestLimit\":{12},\"BayAmt\":{13},\"TAsset\":{14},\"TBorrow\":{15},\"CAsset\":{16},\"CreditInvest\":{17},\"CreditBorrow\":{18},\"AssetsSales\":{19},\"AssetsBuy\":{20},\"AdviserServer\":{21},\"Finace\":{22},\"GrowUpInfo\":{23},\"SafeManage\":{24},\"SvrFee\":{25},\"SvrStopTip\":{26},\"OverdueTip\":{27},\"SentScore\":\"{28}\",\"investApplyStatus\":\"{29}\",\"financingApplyStatus\":\"{30}\",\"sellApplyStatus\":\"{31}\",\"purchaseApplyStatus\":\"{32}\",\"consultantApplyStatus\":\"{33}\",\"investStatus\":\"{34}\",\"financingStatus\":\"{35}\",\"sellStatus\":\"{36}\",\"purchaseStatus\":\"{37}\",\"consultantStatus\":\"{38}\",\"ifGetV\":\"{39}\",\"UserInfo\":{40},\"billStatus\":\"{41}\",\"cityData\":{42}}}", provinceData, marital, enterpriseType, investMain, guaranteeType, assetsType, degreeType, healthStatus, industryType, PawnRateStr, InvestAmtStr, DayRateStr, InvestLimitStr, BayAmtStr, TAssetStr, TBorrowStr, CAssetStr, CreditInvestStr, CreditBorrowStr, AssetsSalesStr, AssetsBuyStr, AdviserServerStr, FinaceStr, GrowUpInfoStr, SafeManageStr, SvrFeeStr, SvrStopTipStr, OverdueTipStr, SentScore, investApplyStatus, financingApplyStatus, sellApplyStatus, purchaseApplyStatus, consultantApplyStatus,u001.creditRightInvestStatus,u001.creditRightFinancingStatus,u001.assetsSellingStatus,u001.assetsPruchaseStatus,u001.consultantStatus,ifGetV, userInfoStr, billStatus, cityData);

                return res;
            }
        }

        //成长值
        private string GrowUp(DBMA1DataContext dbma1, string userSN, decimal currentGroupValue)
        {
            //vip等级设置
            A029 a029 = dbma1.A029s.First();

            //当前vip等级及奖励率
            string vipLevel = string.Empty;
            decimal rewardRate;
            if (currentGroupValue < a029.vip1_originateValue)
            {
                vipLevel = "0";
                rewardRate = 0;
            }
            else if (currentGroupValue < a029.vip2_originateValue)
            {
                vipLevel = "1";
                rewardRate = a029.vip1_rewardRate;
            }
            else if (currentGroupValue < a029.vip3_originateValue)
            {
                vipLevel = "2";
                rewardRate = a029.vip2_rewardRate;
            }
            else if (currentGroupValue < a029.vip4_originateValue)
            {
                vipLevel = "3";
                rewardRate = a029.vip3_rewardRate;
            }
            else if (currentGroupValue < a029.vip5_originateValue)
            {
                vipLevel = "4";
                rewardRate = a029.vip4_rewardRate;
            }
            else if (currentGroupValue < a029.vip6_originateValue)
            {
                vipLevel = "5";
                rewardRate = a029.vip5_rewardRate;
            }
            else if (currentGroupValue < a029.vip7_originateValue)
            {
                vipLevel = "6";
                rewardRate = a029.vip6_rewardRate;
            }
            else
            {
                vipLevel = "7";
                rewardRate = a029.vip7_rewardRate;
            }

            var GrowUpInfo = new { Leval = vipLevel, Award = rewardRate };
            return C101.FC10107(GrowUpInfo);
        }

        //投资额
        public decimal? GetInvestMoneyAmount(decimal moneyAmount)
        {
            return moneyAmount;
        }

        //投资收益
        private decimal CalculateInvestEarnings(DateTime investDate,decimal dailyRate)
        {
            int investDays = (DateTime.Now - investDate).Days;
            return investDays * dailyRate;
        }
        #endregion

        #region 投资服务申请
        //获取投资方信息 P802010105
        public string FP802010105()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                return string.Format("{{\"balance\":\"{0}\",\"userData\":{1}}}", balance, GetUserData(dbma1, userSN));
            }
        }

        /// <summary>
        /// 投资服务申请
        /// </summary>
        public string FP802010106(string investSvrData, string pwd)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            U002 investSvrObj = C101.FC10108(investSvrData, typeof(U002)) as U002;

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //密码是否正确
                if (C201.FC20146(dbma1,userSN,pwd) == false)
                {
                    return "0";
                }

                //扣款
                A023 a023 = dbma1.A023s.First();
                if(C201.FC20147(dbma1,userSN,Convert.ToDecimal(a023.openServerCost),"投资申请",null) == false)
                {
                    return "2";
                }

                //更新用户信息基本表 U002
                InvestSvrUpdateU002(dbma1, investSvrObj, userSN);

                //更新服务表 U001
                InvestSvrUpdateU001(dbma1, userSN);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = userSN;
                f006.businessSN = userSN;
                f006.businessType = "投资申请";
                f006.transactionMoneyAmount = Convert.ToDecimal(a023.openServerCost);
                f006.groupUpValue = a023.openServerCost;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                return "1";
            }
        }

        //更新用户信息基本表 U002
        private void InvestSvrUpdateU002(DBMA1DataContext dbma1,U002 investSvrObj,string userSN)
        {
            U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

            u002.maritalStatusTypeSN = investSvrObj.maritalStatusTypeSN;
            u002.procreateStatus = investSvrObj.procreateStatus;
            u002.currentAddressProvinceSN = investSvrObj.currentAddressProvinceSN;
            u002.currentAddressCitySN = investSvrObj.currentAddressCitySN;
            u002.currentAddressDetails = investSvrObj.currentAddressDetails;
            u002.workEnterprise = investSvrObj.workEnterprise;
            u002.enterpriseTypeSN = investSvrObj.enterpriseTypeSN;
            u002.workTel = investSvrObj.workTel;
            u002.hiredate = investSvrObj.hiredate;
            u002.post = investSvrObj.post;
            u002.investMainTypeSN = investSvrObj.investMainTypeSN;
            u002.financingMain = investSvrObj.financingMain;
            u002.maxMortgageRate = investSvrObj.maxMortgageRate;
            u002.guaranteeTypeSN = investSvrObj.guaranteeTypeSN;
            u002.investProvinceSN = investSvrObj.investProvinceSN;
            u002.investCitySN = investSvrObj.investCitySN;
            u002.minInvestMoneyAmount = investSvrObj.minInvestMoneyAmount;
            u002.minInterestRate = investSvrObj.minInterestRate;
            u002.minInvestDays = investSvrObj.minInvestDays;
            u002.maxInvestDays = investSvrObj.maxInvestDays;
            u002.collateralDemand = investSvrObj.collateralDemand;
        }

        //更新服务表 U001
        private void InvestSvrUpdateU001(DBMA1DataContext dbma1,string userSN)
        {
            U001 u001 = dbma1.U001s.Where(c => c.userSN == userSN).First();
            u001.creditRightInvestApplyStauts = 2;
            u001.applyCreditRightInvestDate = DateTime.Now;
            u001.openCreditRightInvestmentDate = DateTime.Now;
            u001.creditRightInvestStatus = true;
        }

        #endregion

        #region 融资服务申请
        //获取融资方信息 P802010105
        public string FP802010206()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                return string.Format("{{\"balance\":\"{0}\",\"userData\":{1}}}", balance, GetUserData(dbma1, userSN));
            }
        }

        public string FP802010207(string financingSvrData, string pwd)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            U002 financingSvrObj = C101.FC10108(financingSvrData, typeof(U002)) as U002;

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //密码是否正确
                if (C201.FC20146(dbma1, userSN, pwd) == false)
                {
                    return "0";
                }

                //扣款
                A024 a024 = dbma1.A024s.First();
                if (C201.FC20147(dbma1, userSN, Convert.ToDecimal(a024.openServerCost), "融资申请", null) == false)
                {
                    return "0";
                }

                //更新用户信息基本表 U002
                FinancingSvrUpdateU002(dbma1, financingSvrObj, userSN);

                //更新服务表 U001
                FinancingSvrUpdateU001(dbma1, userSN);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = userSN;
                f006.businessSN = userSN;
                f006.businessType = "融资申请";
                f006.transactionMoneyAmount = Convert.ToDecimal(a024.openServerCost);
                f006.groupUpValue = a024.openServerCost;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                return "1";
            }
        }

        //更新用户信息基本表 U002
        private void FinancingSvrUpdateU002(DBMA1DataContext dbma1,U002 financingSvrObj,string userSN)
        {
            U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

            u002.maritalStatusTypeSN = financingSvrObj.maritalStatusTypeSN;
            u002.procreateStatus = financingSvrObj.procreateStatus;
            u002.currentAddressProvinceSN = financingSvrObj.currentAddressProvinceSN;
            u002.currentAddressCitySN = financingSvrObj.currentAddressCitySN;
            u002.currentAddressDetails = financingSvrObj.currentAddressDetails;
            u002.workEnterprise = financingSvrObj.workEnterprise;
            u002.enterpriseTypeSN = financingSvrObj.enterpriseTypeSN;
            u002.workTel = financingSvrObj.workTel;
            u002.hiredate = financingSvrObj.hiredate;
            u002.post = financingSvrObj.post;
            u002.healthyStatusTypeSN = financingSvrObj.healthyStatusTypeSN;
            u002.ifBasicLivingAllowance = financingSvrObj.ifBasicLivingAllowance;
            u002.graduateSchool = financingSvrObj.graduateSchool;
            u002.degreeTypeSN = financingSvrObj.degreeTypeSN;
            u002.monthlyTotalIncome = financingSvrObj.monthlyTotalIncome;
            u002.monthlyTotalExpenditure = financingSvrObj.monthlyTotalExpenditure;
            u002.monthlyNetIncome = financingSvrObj.monthlyNetIncome;
            u002.totalAssets = financingSvrObj.totalAssets;
            u002.totalDebt = financingSvrObj.totalDebt;
            u002.ifCourtImplementation = financingSvrObj.ifCourtImplementation;
            u002.creditStatus = financingSvrObj.creditStatus;
        }

        //更新服务表 U001
        private void FinancingSvrUpdateU001(DBMA1DataContext dbma1,string userSN)
        {
            U001 u001 = dbma1.U001s.Where(c => c.userSN == userSN).First();

            u001.creditRightFinancingApplyStatus = 2;
            u001.applyCreditRightFinancingDate = DateTime.Now;
            u001.openCreditRightFinancingDate = DateTime.Now;
            u001.creditRightFinancingStatus = true;
        }

        #endregion

        #region 资产出售申请
        //获取出售方信息 P802010303
        public string FP802010303()
        {
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                return string.Format("{{\"balance\":\"{0}\",\"userData\":{1}}}", balance, GetUserData(dbma1, userSN));
            }
        }

        //申请 P802010304
        public string FP802010304(string SellingSvrData, string pwd)
        {
            string userSN = session["userSN"].ToString();

            U002 svrObj = C101.FC10108(SellingSvrData, typeof(U002)) as U002;

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //密码是否正确
                if (C201.FC20146(dbma1, userSN, pwd) == false)
                {
                    return "0";
                }

                //扣款
                A027 a027 = dbma1.A027s.First();
                if (C201.FC20147(dbma1, userSN, Convert.ToDecimal(a027.openServerCost), "出售申请", null) == false)
                {
                    return "0";
                }

                //更新用户信息基本表 U002
                SellSvrUpdateU002(dbma1, svrObj, userSN);

                //更新服务表 U001
                SellSvrUpdateU001(dbma1, userSN);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = userSN;
                f006.businessSN = userSN;
                f006.businessType = "出售申请";
                f006.transactionMoneyAmount = Convert.ToDecimal(a027.openServerCost);
                f006.groupUpValue = a027.openServerCost;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);


                dbma1.SubmitChanges();

                return "1";
            }
        }

        //更新用户信息基本表 U002
        private void SellSvrUpdateU002(DBMA1DataContext dbma1, U002 svrObj, string userSN)
        {
            U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

            u002.maritalStatusTypeSN = svrObj.maritalStatusTypeSN;
            u002.procreateStatus = svrObj.procreateStatus;
            u002.currentAddressProvinceSN = svrObj.currentAddressProvinceSN;
            u002.currentAddressCitySN = svrObj.currentAddressCitySN;
            u002.currentAddressDetails = svrObj.currentAddressDetails;
        }

        //更新服务表 U001
        private void SellSvrUpdateU001(DBMA1DataContext dbma1, string userSN)
        {
            U001 u001 = dbma1.U001s.Where(c => c.userSN == userSN).First();

            u001.assetsSellingApplyStatus = 2;
            u001.applyAssetsSellingDate = DateTime.Now;
            u001.openAssetsSellingDate = DateTime.Now;
            u001.assetsSellingStatus = true;
        }
        #endregion

        #region 资产购买申请
        //获取融资方信息 P802010404
        public string FP802010404()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                return string.Format("{{\"balance\":\"{0}\",\"userData\":{1}}}", balance, GetUserData(dbma1, userSN));
            }
        }

        //申请 P802010405
        public string FP802010405(string PurchaseSvrData, string pwd)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            U002 purchaseSvrObj = C101.FC10108(PurchaseSvrData, typeof(U002)) as U002;

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //密码是否正确
                if (C201.FC20146(dbma1, userSN, pwd) == false)
                {
                    return "0";
                }

                //扣款
                A026 a026 = dbma1.A026s.First();
                if (C201.FC20147(dbma1, userSN, Convert.ToDecimal(a026.openServerCost), "购买申请", null) == false)
                {
                    return "0";
                }

                //更新用户信息基本表 U002
                PurchaseSvrUpdateU002(dbma1, purchaseSvrObj, userSN);

                //更新服务表 U001
                PurchaseSvrUpdateU001(dbma1, userSN);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = userSN;
                f006.businessSN = userSN;
                f006.businessType = "购买申请";
                f006.transactionMoneyAmount = Convert.ToDecimal(a026.openServerCost);
                f006.groupUpValue = a026.openServerCost;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                return "1";
            }
        }

        //更新用户信息基本表 U002
        private void PurchaseSvrUpdateU002(DBMA1DataContext dbma1, U002 PurchaseSvrObj, string userSN)
        {
            U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

            u002.maritalStatusTypeSN = PurchaseSvrObj.maritalStatusTypeSN;
            u002.procreateStatus = PurchaseSvrObj.procreateStatus;
            u002.currentAddressProvinceSN = PurchaseSvrObj.currentAddressProvinceSN;
            u002.currentAddressCitySN = PurchaseSvrObj.currentAddressCitySN;
            u002.currentAddressDetails = PurchaseSvrObj.currentAddressDetails;
            u002.assetsProvince = PurchaseSvrObj.assetsProvince;
            u002.minPurchasePrice = PurchaseSvrObj.minPurchasePrice;
            u002.maxPurchasePrice = PurchaseSvrObj.maxPurchasePrice;
            u002.assetsType = PurchaseSvrObj.assetsType;
        }

        //更新服务表 U001
        private void PurchaseSvrUpdateU001(DBMA1DataContext dbma1, string userSN)
        {
            U001 u001 = dbma1.U001s.Where(c => c.userSN == userSN).First();

            u001.assetsPurchaseApplyStatus = 2;
            u001.applyAssetsPurchaseDate = DateTime.Now;
            u001.openAssetsPurchaseDate = DateTime.Now;
            u001.assetsPruchaseStatus = true;
        }

        #endregion

        #region 顾问服务申请
        //获取顾问信息 P802010505
        public string FP802010505()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                return string.Format("{{\"balance\":\"{0}\",\"userData\":{1}}}", balance, GetUserData(dbma1, userSN));
            }
        }

        //申请
        public string FP8010506(string u002Str,string pwd)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            U002 consultantSvrObj = C101.FC10108(u002Str, typeof(U002)) as U002;

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //密码是否正确
                if (C201.FC20146(dbma1, userSN, pwd) == false)
                {
                    return "0";
                }

                //扣款
                A025 a025 = dbma1.A025s.First();
                if (C201.FC20147(dbma1, userSN, Convert.ToDecimal(a025.openServerCost), "顾问申请", null) == false)
                {
                    return "0";
                }

                //更新用户信息基本表 U002
                ConcultantSvrUpdateU002(dbma1, consultantSvrObj, userSN);

                //更新服务表 U001
                ConcultantSvrUpdateU001(dbma1, userSN);

                //更新顾问申请备案表 U006
                ConsultantSvrApplyRecUpdateU006(dbma1, consultantSvrObj, userSN);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = userSN;
                f006.businessSN = userSN;
                f006.businessType = "顾问申请";
                f006.transactionMoneyAmount = Convert.ToDecimal(a025.openServerCost);
                f006.groupUpValue = a025.openServerCost;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                return "1";
            }
        }

        //更新用户信息基本表 U002
        private void ConcultantSvrUpdateU002(DBMA1DataContext dbma1,U002 consultantSvrObj,string userSN)
        {
            U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

            u002.maritalStatusTypeSN = consultantSvrObj.maritalStatusTypeSN;
            u002.procreateStatus = consultantSvrObj.procreateStatus;
            u002.currentAddressProvinceSN = consultantSvrObj.currentAddressProvinceSN;
            u002.currentAddressCitySN = consultantSvrObj.currentAddressCitySN;
            u002.currentAddressDetails = consultantSvrObj.currentAddressDetails;
            u002.workEnterprise = consultantSvrObj.workEnterprise;
            u002.enterpriseTypeSN = consultantSvrObj.enterpriseTypeSN;
            u002.industryTypeSN = consultantSvrObj.industryTypeSN;
            u002.workTel = consultantSvrObj.workTel;
            u002.hiredate = consultantSvrObj.hiredate;
            u002.post = consultantSvrObj.post;
            u002.graduateSchool = consultantSvrObj.graduateSchool;
            u002.degreeTypeSN = consultantSvrObj.degreeTypeSN;
            u002.serviceProvinceSN = consultantSvrObj.serviceProvinceSN;
            u002.serviceCitySN = consultantSvrObj.serviceCitySN == "" ? null : consultantSvrObj.serviceCitySN;
            u002.investigate = consultantSvrObj.investigate;
            u002.assetsEvaluate = consultantSvrObj.assetsEvaluate;
            u002.creditRightGuarantee = consultantSvrObj.creditRightGuarantee;
            u002.badLoanCollect = consultantSvrObj.badLoanCollect;
            u002.consultantDetails = consultantSvrObj.consultantDetails;
        }

        //更新服务表 U001
        private void ConcultantSvrUpdateU001(DBMA1DataContext dbma1,string userSN)
        {
            U001 u001 = dbma1.U001s.Where(c => c.userSN == userSN).First();

            u001.consultantApplyStatus = 1;
            u001.applyConsultantDate = DateTime.Now;
            u001.openConsultantDate = DateTime.Now;
            u001.consultantStatus = true;
        }

        //更新顾问申请备案表 U006
        private void ConsultantSvrApplyRecUpdateU006(DBMA1DataContext dbma1,U002 consultantSvrObj,string userSN)
        {
            U006 u006 = new U006();
            U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

            u006.recordSN = C101.FC10102("U006", 6, "UE");
            u006.userSN = userSN;
            u006.name = u002.name;
            u006.birthday = u002.birthday;
            u006.gender = u002.gender;
            u006.registeredResidence = u002.registeredResidence.Trim();
            u006.idCard = u002.idCard;
            u006.phone = u002.phone;
            u006.email = consultantSvrObj.email;
            u006.maritalStatusType = dbma1.A000s.Where(c => c.maritalStatusTypeSN == consultantSvrObj.maritalStatusTypeSN).First().value;
            u006.procreateStatus = consultantSvrObj.procreateStatus;
            u006.currentAddressProvince = dbma1.A021s.Where(c => c.provinceSN == consultantSvrObj.currentAddressProvinceSN).First().name;
            u006.currentAddressCity = dbma1.A022s.Where(c => c.citySN == consultantSvrObj.currentAddressCitySN).First().name;
            u006.currentAddressDetails = consultantSvrObj.currentAddressDetails;
            u006.workEnterprise = consultantSvrObj.workEnterprise;
            u006.enterpriseType = dbma1.A004s.Where(c => c.enterpriseTypeSN == consultantSvrObj.enterpriseTypeSN).First().value;
            u006.industryType = dbma1.A013s.Where(c => c.industryTypeSN == consultantSvrObj.industryTypeSN).First().value;
            u006.workTel = consultantSvrObj.workTel;
            u006.hiredate = consultantSvrObj.hiredate;
            u006.post = consultantSvrObj.post;
            u006.graduateSchool = consultantSvrObj.graduateSchool;
            u006.degreeType = dbma1.A002s.Where(c => c.degreeTypeSN == consultantSvrObj.degreeTypeSN).First().value;
            u006.serviceProvince = dbma1.A021s.Where(c => c.provinceSN == consultantSvrObj.serviceProvinceSN).First().name;
            u006.serviceCity = consultantSvrObj.serviceCitySN == "" ? null : dbma1.A022s.Where(c => c.citySN == consultantSvrObj.serviceCitySN).First().name;
            u006.investigate = consultantSvrObj.investigate;
            u006.assetsEvaluate = consultantSvrObj.assetsEvaluate;
            u006.creditRightGuarantee = consultantSvrObj.creditRightGuarantee;
            u006.badLoanCollect = consultantSvrObj.badLoanCollect;
            u006.consultantDetails = consultantSvrObj.consultantDetails;
            u006.applyDate = DateTime.Now;

            dbma1.U006s.InsertOnSubmit(u006);
        }

        #endregion

        //获取用户信息
        private string GetUserData(DBMA1DataContext dbma1, string userSN)
        {
            var userData = (from c in dbma1.VP801011s
                            where c.userSN == userSN
                            select c
                           ).First();
            return C101.FC10107(userData);
        }
    }
}





