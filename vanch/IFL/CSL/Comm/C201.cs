using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace IFL.Comm
{
    //业务辅助
    public class C201
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        /// <summary>
        /// 获取婚姻情况 A000
        /// </summary>
        public static string FC20100(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A000s
                            select new
                            {
                                Type = 2,
                                ID = c.maritalStatusTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取健康状况 A001
        /// </summary>
        public static string FC20101(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A001s
                            select new
                            {
                                Type = 9,
                                ID = c.healthyStatusTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取学历 A002
        /// </summary>
        public static string FC20102(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A002s
                            select new
                            {
                                Type = 8,
                                ID = c.degreeTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取家属关系 A003
        /// </summary>
        public static string FC20103(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A003s
                            select new
                            {
                                Type = 1,
                                ID = c.kinRelationshipTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取单位性质 A004
        /// </summary>
        public static string FC20104(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A004s
                            select new
                            {
                                Type = 3,
                                ID = c.enterpriseTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取信用状况 A005
        /// </summary>
        public static string FC20105(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A005s
                            select new
                            {
                                Type = 10,
                                ID = c.creditStatusTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取投资主体 A006
        /// </summary>
        public static string FC20106(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A006s
                            select new
                            {
                                Type = 4,
                                ID = c.investMainTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取融资主体 A007

        /// <summary>
        /// 获取资产类型(无混合资产) A008
        /// </summary>
        public static string FC20108(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A008s
                            where c.assetsTypeSN.Trim() != "AB"
                            select new
                            {
                                Type = 7,
                                ID = c.assetsTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取资产来源 A009
        /// </summary>
        public static string FC20109(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A009s
                            select new
                            {
                                Type = 15,
                                ID = c.assetsSourceTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取资产使用情况 A010
        /// </summary>
        public static string FC20110(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A010s
                            select new
                            {
                                Type = 16,
                                ID = c.useStatusTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取资产查封情况 A011


        /// <summary>
        /// 获取资产抵押类型 A012
        /// </summary>
        public static string FC20112(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A012s
                            select new
                            {
                                Type = 17,
                                ID = c.mortgageTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取所属行业 A013
        /// </summary>
        public static string FC20113(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A013s
                            select new
                            {
                                Type = 11,
                                ID = c.industryTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取还款类型 A014
        /// </summary>
        public static string FC20114(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A014s
                            select new
                            {
                                Type = 12,
                                ID = c.repaymentTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取资金用途 A015
        /// </summary>
        public static string FC20115(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A015s
                            select new
                            {
                                Type = 13,
                                ID = c.capitalPurposeTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取还款来源 A016
        /// </summary>
        public static string FC20116(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A016s
                            select new
                            {
                                Type = 14,
                                ID = c.repaymentSourceTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取保证方式 A017
        /// </summary>
        public static string FC20117(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A017s
                            select new
                            {
                                Type = 6,
                                ID = c.guaranteeTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取账单类型 A018 

        //获取业务类型 A019

        //获取身份证归属地

        /// <summary>
        /// 获取省 A021
        /// </summary>
        public static string FC20121(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A021s
                            select new
                            {
                                Type = 0,
                                ID = c.provinceSN,
                                Name = c.name.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 获取市 A022
        /// </summary>
        public static string FC20122(DBMA1DataContext dbma1,string provinceSN)
        {
            var linqData = from c in dbma1.A022s
                           where c.provinceSN == provinceSN
                           select new
                           {
                               ID = c.citySN,
                               Name = c.name.Trim()
                           };

            return C101.FC10107(linqData);
        }

        //获得投资参数 A023

        /// <summary>
        /// 获得融资参数 A024
        /// </summary>
        public static string FC20124(DBMA1DataContext dbma1)
        {
            var linqData = dbma1.A024s.First();

            var financingMoneyAmount = new { Min = linqData.minFinancingMoneyAmount, Max = linqData.maxFinancingMoneyAmount };
            var deadline = new { Min = linqData.minDeadline, Max = linqData.maxDeadline };
            var dailyRate = new { Min = linqData.minDailyRate, Max = linqData.maxDailyRate };
            var obtainCost = new { Min = linqData.minObtainCost, Max = linqData.maxObtainCost };
            var marketValue = new { Min = linqData.minMarketValue, Max = linqData.maxMarketValue };
            var registerCapital = new { Min = linqData.minRegisterCapital, Max = linqData.maxRegisterCapital };
            var totalAssets = new { Min = linqData.minTotalAssets, Max = linqData.maxTotalAssets };
            var totalLiability = new { Min = linqData.minTotalLiability, Max = linqData.maxTotalLiability };
            var currentLoan = new { Min = linqData.minCurrentLoan, Max = linqData.maxCurrentLoan };  
            var netAssets = new { Min = linqData.minNetAssets, Max = linqData.maxNetAssets };
            var turnover = new { Min = linqData.minTurnOver, Max = linqData.maxTurnOver };
            var taxTurnover = new { Min = linqData.minTaxTurnOver, Max = linqData.maxTaxTurnOver };
            var netProfit = new { Min = linqData.minNetProfit, Max = linqData.maxNetProfit };
            var stockHolderNum = new { Min = linqData.minStockHolderNum, Max = linqData.maxStockHolderNum };
            var staffNum = new { Min = linqData.minStaffNum, Max = linqData.maxStaffNum };
            var financingFee = new { SvrFee = linqData.financePublishCost, SvrRate = linqData.serviceRateDaily };
            var investorRecommendFee = new { SvrFee = linqData.investorRecommendCost, SvrRate = linqData.serviceRateDaily };

            string financingMoneyAmountStr = C101.FC10107(financingMoneyAmount);
            string deadlineStr = C101.FC10107(deadline);
            string dailyRateStr = C101.FC10107(dailyRate);
            string obtainCostStr = C101.FC10107(obtainCost);
            string marketValueStr = C101.FC10107(marketValue);
            string registerCapitalStr = C101.FC10107(registerCapital);
            string totalAssetsStr = C101.FC10107(totalAssets);
            string totalLiabilityStr = C101.FC10107(totalLiability);
            string currentLoanStr = C101.FC10107(currentLoan);
            string netAssetsStr = C101.FC10107(netAssets);
            string turnoverStr = C101.FC10107(turnover);
            string taxTurnoverStr = C101.FC10107(taxTurnover);
            string netProfitStr = C101.FC10107(netProfit);
            string stockHolderNumStr = C101.FC10107(stockHolderNum);
            string staffNumStr = C101.FC10107(staffNum);
            string financingFeeStr = C101.FC10107(financingFee);
            string investorRecommendFeeStr = C101.FC10107(investorRecommendFee);

            string res = string.Format("\"BorrowAmt\":{0},\"BorrowLimit\":{1},\"DayRate\":{2},\"TCO\":{3},\"MarketAmt\":{4},\"RegAmt\":{5},\"TotalAsset\":{6},\"TotalBorrow\":{7},\"NowBorrow\":{8},\"ClearAsset\":{9},\"SalesAmt\":{10},\"TaxSalesAmt\":{11},\"ClearProfit\":{12},\"ShareHolderNum\":{13},\"StaffNum\":{14},\"BorrowSvr\":{15},\"SltInvestSvr\":{16}", financingMoneyAmountStr, deadlineStr, dailyRateStr, obtainCostStr, marketValueStr, registerCapitalStr, totalAssetsStr, totalLiabilityStr, currentLoanStr, netAssetsStr, turnoverStr, taxTurnoverStr, netProfitStr, stockHolderNumStr, staffNumStr, financingFeeStr, investorRecommendFeeStr);

            return res;
        }

        //获取融资方取消预约原因 A200
        public static string FC20130(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A200s
                            select new
                            {
                                Type = 21,
                                ID = c.financierCancelReserveReasonTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取融资方拒绝预约原因 A201
        public static string FC20131(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A201s
                            select new
                            {
                                Type = 20,
                                ID = c.financierRefuseReserveReasonTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取投资方取消债权预约原因 A100
        public static string FC20132(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A100s
                            select new
                            {
                                Type = 19,
                                ID = c.investorCancelReserveReasonTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取投资方拒绝债权预约原因 A101
        public static string FC20133(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A101s
                            select new
                            {
                                Type = 18,
                                ID = c.investorRefuseReserveReasonTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取投资方取消顾问预约原因 A102

        //获取顾问拒绝预约原因 A500
        public static string FC20135(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A500s
                            select new
                            {
                                Type = 22,
                                ID = c.consultantRefuseReserveReasonTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取顾问审查未过原因 A501
        public static string FC20136(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A501s
                            select new
                            {
                                Type = 23,
                                ID = c.consultantAuditNotPassReasonTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取购买方取消预约原因 A400
        public static string FC20137(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A400s
                            select new
                            {
                                ID = c.purchaserCancelReserveReasonSN,
                                Value = c.value
                            }).ToList();

            return C101.FC10107(linqData);
        }

        //获取出售方拒绝预约原因 A300
        public static string FC20138(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A300s
                            select new
                            {
                                ID = c.sellerRefuseReserveReasonSN,
                                Value = c.value
                            }).ToList();

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 由省份获取市
        /// </summary>
        public static string FC20140(string provinceSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return Comm.C201.FC20122(dbma1, provinceSN);
            }
        }

        /// <summary>
        /// 获取抵押物信息
        /// </summary>
        public static string FC20141(string creditRightSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var pawnData = dbma1.VP201011s.Where(c => c.creditRightSN == creditRightSN).First();

                return C101.FC10107(pawnData);
            }
        }

        /// <summary>
        /// 获取企业信息
        /// </summary>
        public static string FC20142(string creditRightSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var enterpriseData = dbma1.VP201021s.Where(c => c.creditRightSN == creditRightSN).First();

                return C101.FC10107(enterpriseData);
            }
        }

        /// <summary>
        /// 获取融资方全部信息
        /// </summary>
        public static string FC20143(string financierUserSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //案例情况
                var caseStatus = dbma1.P102s.Where(c => c.financierUserSN == financierUserSN);
                int caseAmount = caseStatus.Count();
                decimal? caseMoneyAmount = caseAmount == 0 ? 0 : caseStatus.Sum(c => c.investMoneyAmount);

                //当前债务情况
                var debtStatus = dbma1.P102s.Where(c => c.financierUserSN == financierUserSN && c.closeCaseDate == null);
                int debtAmount = debtStatus.Count();
                decimal? debtMoneyAmount = debtAmount == 0 ? 0 : debtStatus.Sum(c => c.investMoneyAmount);

                //当前逾期情况
                var currentOverdueStatus = from c in dbma1.P102s
                                           where c.financierUserSN == financierUserSN
                                            && c.closeCaseDate == null
                                            && DateTime.Now > c.deadlineDate
                                           select c;
                int currentOverdueAmount = currentOverdueStatus.Count();
                decimal? currentOverdueMoneyAmount = currentOverdueAmount == 0 ? 0 : currentOverdueStatus.Sum(c => c.investMoneyAmount);

                //历史逾期数量
                var historyOverdueStatus = from c in dbma1.P102s
                                           from p in c.P103s
                                           where c.financierUserSN == financierUserSN
                                            && c.deadlineDate < p.repayDate
                                            && c.investSN == p.investSN
                                           select c;
                int historyOverdueAmount = historyOverdueStatus.Count();

                //注册时间
                DateTime regDatetime = dbma1.U000s.Where(c => c.userSN == financierUserSN).First().registerDate;
                string regDatetimeStr = C101.FC10107(regDatetime);

                //基本信息
                var financierBasicData = (from c in dbma1.VP801001s
                                          where c.userSN == financierUserSN
                                          select c).First();
                string financierBasicDataStr = C101.FC10107(financierBasicData);

                string res = string.Format("{{\"financierBasicData\":{0},\"caseAmount\":{1},\"caseMoneyAmount\":{2},\"debtAmount\":{3},\"debtMoneyAmount\":{4},\"currentOverdueAmount\":{5},\"currentOverdueMoneyAmount\":{6},\"historyOverdueAmount\":{7},\"regDatetime\":{8}}}", financierBasicDataStr, caseAmount, caseMoneyAmount, debtAmount, debtMoneyAmount, currentOverdueAmount, currentOverdueMoneyAmount, historyOverdueAmount, regDatetimeStr);

                return res;
            }
        }

        /// <summary>
        /// 投资方信息
        /// </summary>
        public static string FC20144(string investorUserSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var investorData = (from c in dbma1.VP801001s
                                    where c.userSN == investorUserSN
                                    select c).First();


                var investStatus = dbma1.VP104011s.Where(c => c.investorUserSN == investorUserSN).First();

                string res = string.Format("{{\"investorData\":{0},\"investCaseAmount\":{1},\"investMoneyAmount\":{2}}}", C101.FC10107(investorData), investStatus.investCaseAmount, investStatus.InvestMoneyAmount);

                return res;
            }
        }

        //顾问信息
        public static string FC20145(string consultantUserSN, string crSN)
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

                //报价
                decimal quotePricePercent = (from c in dbma1.P101s
                                             where c.receiverUserSN == consultantUserSN && c.creditRightSN == crSN
                                             orderby c.sendTime descending
                                             select c).First().costPercent;

                decimal financingMoneyAmount = (from c in dbma1.P200s
                                                where c.creditRightSN == crSN
                                                select c).First().financingAmount;

                decimal quotePrice = financingMoneyAmount * quotePricePercent;

                return string.Format("{{\"consultantBasicData\":{0},\"regDatetime\":{1},\"caseAmount\":\"{2}\",\"successCaseAmount\":\"{3}\",\"badLoanCaseAmount\":\"{4}\",\"currentBadLoanCaseAmount\":\"{5}\",\"quotePrice\":\"{6}\"}}", consultantBasicDataStr, regDatetimeStr, caseAmount, successCaseAmount, badLoanCaseAmount, currentBadLoanCaseAmount, quotePrice);
            }
        }

        /// <summary>
        /// 验证交易密码是否正确
        /// </summary>
        public static bool FC20146(DBMA1DataContext dbma1, string userSN, string pwd)
        {
            string transPwd = dbma1.U003s.Where(c => c.userSN == userSN).First().transactPwd;

            return C101.FC10104(pwd, transPwd);
        }

        /// <summary>
        /// 扣款
        /// </summary>
        public static bool FC20147(DBMA1DataContext dbma1,string userSN,decimal amount,string type,string referSN)
        {
            //从余额中扣除服务费 F000
            F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
            if (f000.balance < amount)
            {
                return false;
            }
            f000.balance -= amount;

            //加入收支明细表中 F003
            string F003max33SN = C101.FC10102("F003", 8, "UA");
            F003 f003 = new F003();
            f003.revenueExpenditureSN = F003max33SN;
            f003.generetorUserSN = userSN;
            f003.generateDate = DateTime.Now;
            f003.type = type;
            f003.expenditure = amount;
            f003.balance = f000.balance;
            f003.referSN = referSN;
            dbma1.F003s.InsertOnSubmit(f003);

            return true;
        }

        /// <summary>
        /// 获取市 A022
        /// </summary>
        public static string FC20148(DBMA1DataContext dbma1)
        {
            var linqData = from c in dbma1.A022s
                           select new
                           {
                               ID = c.citySN,
                               Name = c.name.Trim(),
                               ParentID = c.provinceSN
                           };

            return C101.FC10107(linqData);
        }

        /// <summary>
        /// 检查余额是否充足
        /// </summary>
        public static bool FC20149(DBMA1DataContext dbma1,string userSN, decimal amount)
        {
            decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

            return balance >= amount ? true : false;
        }

        #region 账户退出
        public void FC20150()
        {
            session["userSN"] = null;
            session.RemoveAll();
            //session.Clear();
        }
        #endregion

        #region 获取资产类型(有混合资产)
        public static string FC20151(DBMA1DataContext dbma1)
        {
            var linqData = (from c in dbma1.A008s
                            select new
                            {
                                Type = 7,
                                ID = c.assetsTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }
        #endregion

        #region 获得vip奖励比率 FC20152
        public static decimal FC20152(DBMA1DataContext dbma1,string userSN)
        {
            //vip等级设置
            A029 a029 = dbma1.A029s.First();
            string a029Str = C101.FC10107(a029);

            //当前成长值
            decimal currentGroupValue = Convert.ToInt32(dbma1.F006s.Where(c => c.userSN == userSN).Sum(c => c.groupUpValue));

            //当前vip等级及奖励率
            decimal rewardRate;
            if (currentGroupValue < a029.vip1_originateValue)
            {
                rewardRate = 0;
            }
            else if (currentGroupValue < a029.vip2_originateValue)
            {
                rewardRate = a029.vip1_rewardRate;
            }
            else if (currentGroupValue < a029.vip3_originateValue)
            {
                rewardRate = a029.vip2_rewardRate;
            }
            else if (currentGroupValue < a029.vip4_originateValue)
            {
                rewardRate = a029.vip3_rewardRate;
            }
            else if (currentGroupValue < a029.vip5_originateValue)
            {
                rewardRate = a029.vip4_rewardRate;
            }
            else if (currentGroupValue < a029.vip6_originateValue)
            {
                rewardRate = a029.vip5_rewardRate;
            }
            else if (currentGroupValue < a029.vip7_originateValue)
            {
                rewardRate = a029.vip6_rewardRate;
            }
            else
            {
                rewardRate = a029.vip7_rewardRate;
            }

            return rewardRate;
        }
        #endregion

        #region FC20153 是否有逾期账单
        public static bool FC20153(DBMA1DataContext dbma1,string userSN)
        {
            int needPayDays = Convert.ToInt32(dbma1.A028s.First().needPayDays);

            var data = (from c in dbma1.F001s
                        where c.payerUserSN == userSN
                         & !dbma1.F002s.Where(o => o.billSN == c.billSN).Any()
                         & (DateTime.Now - c.generateDate).Days > needPayDays
                        select c).FirstOrDefault();

            return data != null ? true : false;
        }
        #endregion

        #region FC20154 扣款（1、余额。2、收支明细表。3、成长值表）
        public static bool FC20154(DBMA1DataContext dbma1, string userSN, decimal amount, string type, string referSN)
        {
            //从余额中扣除服务费 F000
            F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
            if (f000.balance < amount)
            {
                return false;
            }
            f000.balance -= amount;

            //加入收支明细表中 F003
            string F003max33SN = C101.FC10102("F003", 8, "UA");
            F003 f003 = new F003();
            f003.revenueExpenditureSN = F003max33SN;
            f003.generetorUserSN = userSN;
            f003.generateDate = DateTime.Now;
            f003.type = type;
            f003.expenditure = amount;
            f003.balance = f000.balance;
            f003.referSN = referSN;
            dbma1.F003s.InsertOnSubmit(f003);

            //加入成长值表 F006
            F006 f006 = new F006();
            f006.groupUpSN = C101.FC10102("F006", 7, "UD");
            f006.userSN = userSN;
            f006.businessSN = referSN;
            f006.businessType = type;
            f006.transactionMoneyAmount = amount;
            f006.groupUpValue = amount;
            f006.acquireDate = DateTime.Now;
            dbma1.F006s.InsertOnSubmit(f006);

            return true;
        }
        #endregion
    }
}
