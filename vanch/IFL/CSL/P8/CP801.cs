using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P8
{    //基本信息
    public class CP801
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P8010100 初始化
        public string FP8010100()
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //获取uip
                string provinceData = C201.FC20121(dbma1).Replace("[", "").Replace("]", "");
                string kinRelationType = C201.FC20103(dbma1).Replace("[", "").Replace("]", "");
                string maritalStatusType = C201.FC20100(dbma1).Replace("[", "").Replace("]", "");
                string enterpriseType = C201.FC20104(dbma1).Replace("[", "").Replace("]", "");
                string investMainType = C201.FC20106(dbma1).Replace("[", "").Replace("]", "");
                string guaranteeType = C201.FC20117(dbma1).Replace("[", "").Replace("]", "");
                string assetsType = C201.FC20108(dbma1).Replace("[", "").Replace("]", "");
                string degreeType = C201.FC20102(dbma1).Replace("[", "").Replace("]", "");
                string healthyStatusType = C201.FC20101(dbma1).Replace("[", "").Replace("]", "");
                //string creditStatusType = C201.FC20105(dbma1).Replace("[", "").Replace("]", "");
                string industryType = C201.FC20113(dbma1).Replace("[", "").Replace("]", "");

                A023 a023 = dbma1.A023s.First();
                A024 a024 = dbma1.A024s.First();
                A026 a026 = dbma1.A026s.First();

                var PawnRate = new { Min = a023.minMorgageRate, Max = a023.maxMorgageRate };
                var InvestAmt = new { Min = a023.minInvestMoneyAmount, Max = a023.maxInvestMoneyAmount };
                var DayRate = new { Min = a023.minDailyRate, Max = a023.maxDailyRate };
                var InvestLimit = new { Min = a023.minInvestDays, Max = a023.maxInvestDays };
                var BayAmt = new { Min = a026.minPurchasePrice, Max = a026.maxPurchasePrice };
                var TAsset = new { Min = a024.minTotalAssets, Max = a024.maxTotalAssets };
                var TLiability = new { Min = a024.maxTotalLiability, Max = a024.maxTotalLiability };
                var CAsset = new { Min = a024.minNetAssets, Max = a024.maxNetAssets };

                string PawnRateStr = C101.FC10107(PawnRate);
                string InvestAmtStr = C101.FC10107(InvestAmt);
                string DayRateStr = C101.FC10107(DayRate);
                string InvestLimitStr = C101.FC10107(InvestLimit);
                string BayAmtStr = C101.FC10107(BayAmt);
                string TAssetStr = C101.FC10107(TAsset);
                string TLiabilityStr = C101.FC10107(TLiability);
                string CAssetStr = C101.FC10107(CAsset);

                //获取用户信息-债权投资
                string userSN = session["userSN"].ToString();
                //string userSN = "U00001";

                string userData = GetUserData(dbma1, userSN);


                return string.Format("{{\"uip\":{{\"SltConfigData\":[{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}],\"PawnRate\":{10},\"InvestAmt\":{11},\"DayRate\":{12},\"InvestLimit\":{13},\"BayAmt\":{14},\"TAsset\":{15},\"TLiability\":{16},\"CAsset\":{17}}},\"userData\":{18},\"userSN\":\"{19}\"}}", provinceData, kinRelationType, maritalStatusType, enterpriseType, investMainType, guaranteeType, assetsType, degreeType, healthyStatusType, industryType, PawnRateStr, InvestAmtStr, DayRateStr, InvestLimitStr, BayAmtStr, TAssetStr, TLiabilityStr, CAssetStr, userData, userSN);
            }  
        }
        #endregion

        #region 债权投资
        //P8010101 债权投资载入
        public string FP8010101()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetUserData(dbma1, userSN);
            }
        }

        //P8010102 个人信息
        public void FP8010102(string maritalStatusTypeSN, bool procreateStatus, string currentAddressProvinceSN, string currentAddressCitySN, string currentAddressDetails)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.maritalStatusTypeSN = maritalStatusTypeSN;
                u002.procreateStatus = procreateStatus;
                u002.currentAddressProvinceSN = currentAddressProvinceSN;
                u002.currentAddressCitySN = currentAddressCitySN;
                u002.currentAddressDetails = currentAddressDetails;

                dbma1.SubmitChanges();
            }
        }

        //P8010103 投资意向
        public void FP8010103(string investMainTypeSN, bool financingMain, decimal maxMortgageRate, string guaranteeTypeSN, string investProvinceSN, string investCitySN, decimal minInvestMoneyAmount, decimal minInterestRate, short minInvestDays, short maxInvestDays, string collateralDemand)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();
                u002.investMainTypeSN = investMainTypeSN;
                u002.financingMain = financingMain;
                u002.maxMortgageRate = maxMortgageRate;
                u002.guaranteeTypeSN = guaranteeTypeSN;
                u002.investProvinceSN = investProvinceSN;
                u002.investCitySN = investCitySN;
                u002.minInvestMoneyAmount = minInvestMoneyAmount;
                u002.minInterestRate = minInterestRate;
                u002.minInvestDays = minInvestDays;
                u002.maxInvestDays = maxInvestDays;
                u002.collateralDemand = collateralDemand;

                dbma1.SubmitChanges();
            }
        }

        //P8010104 工作信息
        public void FP8010104(string workEnterprise, string enterpriseTypeSN, DateTime? hiredate, string workTel, string post, string enterpriseSwitchboard, string enterpriseWebsite)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();
                u002.workEnterprise = workEnterprise;
                u002.enterpriseTypeSN = enterpriseTypeSN;
                u002.hiredate = hiredate;
                u002.workTel = workTel;
                u002.post = post;
                u002.enterpriseSwitchboard = enterpriseSwitchboard;
                u002.enterpriseWebsite = enterpriseWebsite;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region 债权融资
        //P8010201 债权融资载入
        public string FP8010201()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetUserData(dbma1, userSN);
            }
        }

        //P8010202 个人信息
        public void FP8010202(string maritalStatusTypeSN, bool procreateStatus, string healthyStatusTypeSN, bool ifBasicLivingAllowance, string currentAddressProvinceSN, string currentAddressCitySN, string currentAddressDetails, string graduateSchool, string degreeTypeSN, string degreeCard, string friendName, string friendPhone)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.maritalStatusTypeSN = maritalStatusTypeSN;
                u002.procreateStatus = procreateStatus;
                u002.healthyStatusTypeSN = healthyStatusTypeSN;
                u002.ifBasicLivingAllowance = ifBasicLivingAllowance;
                u002.currentAddressProvinceSN = currentAddressProvinceSN;
                u002.currentAddressCitySN = currentAddressCitySN;
                u002.currentAddressDetails = currentAddressDetails;
                u002.graduateSchool = graduateSchool;
                u002.degreeTypeSN = degreeTypeSN;
                u002.degreeCard = degreeCard;
                u002.friendName = friendName;
                u002.friendPhone = friendPhone;

                dbma1.SubmitChanges();
            }
        }

        //P8010203 家庭信息
        public void FP8010203(string spouseName, string spousePhone, string spouseIdCard, string spouseEnterprise, string kinName, string kinRelationshipTypeSN, string kinPhone, string kinIdCard, string kinEnterprise)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.spouseName = spouseName;
                u002.spousePhone = spousePhone;
                u002.spouseIdCard = spouseIdCard;
                u002.spouseEnterprise = spouseEnterprise;
                u002.kinName = kinName;
                u002.kinRelationshipTypeSN = kinRelationshipTypeSN;
                u002.kinPhone = kinPhone;
                u002.kinIdCard = kinIdCard;
                u002.kinEnterprise = kinEnterprise;

                dbma1.SubmitChanges();
            }
        }

        //P8010204 工作信息
        public void FP8010204(string workEnterprise, string enterpriseTypeSN, DateTime? hiredate, string workTel, string post, string enterpriseSwitchboard, string enterpriseWebsite, string colleageName, string colleagePhone)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.workEnterprise = workEnterprise;
                u002.enterpriseTypeSN = enterpriseTypeSN;
                u002.hiredate = hiredate;
                u002.workTel = workTel;
                u002.post = post;
                u002.enterpriseSwitchboard = enterpriseSwitchboard;
                u002.enterpriseWebsite = enterpriseWebsite;
                u002.colleageName = colleageName;
                u002.colleagePhone = colleagePhone;

                dbma1.SubmitChanges();
            }
        }

        //P8010205 财务信息
        public void FP8010205(decimal monthlyTotalIncome, decimal monthlyTotalExpenditure, decimal monthlyNetIncome, decimal totalAssets, decimal totalDebt, bool ifCourtImplementation, bool creditStatus)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.monthlyTotalIncome = monthlyTotalIncome;
                u002.monthlyTotalExpenditure = monthlyTotalExpenditure;
                u002.monthlyNetIncome = monthlyNetIncome;
                u002.totalAssets = totalAssets;
                u002.totalDebt = totalDebt;
                u002.ifCourtImplementation = ifCourtImplementation;
                u002.creditStatus = creditStatus;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region 资产出售
        //P8010301 资产出售载入
        public string FP8010301()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetUserData(dbma1, userSN);
            }
        }

        //P8010302 个人信息
        public void FP8010302(string maritalStatusTypeSN, bool procreateStatus, string currentAddressProvinceSN, string currentAddressCitySN, string currentAddressDetails)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.maritalStatusTypeSN = maritalStatusTypeSN;
                u002.procreateStatus = procreateStatus;
                u002.currentAddressProvinceSN = currentAddressProvinceSN;
                u002.currentAddressCitySN = currentAddressCitySN;
                u002.currentAddressDetails = currentAddressDetails;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region 资产购买
        //P8010401 资产购买载入
        public string FP8010401()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetUserData(dbma1, userSN);
            }
        }

        //P8010402 个人信息
        public void FP8010402(string maritalStatusTypeSN, bool procreateStatus, string currentAddressProvinceSN, string currentAddressCitySN, string currentAddressDetails)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();
                u002.maritalStatusTypeSN = maritalStatusTypeSN;
                u002.procreateStatus = procreateStatus;
                u002.currentAddressProvinceSN = currentAddressProvinceSN;
                u002.currentAddressCitySN = currentAddressCitySN;
                u002.currentAddressDetails = currentAddressDetails;

                dbma1.SubmitChanges();
            }
        }

        //P8010403 购买意向
        public void FP8010403(string assetsProvince, decimal minPurchasePrice, decimal maxPurchasePrice, string assetsType)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.assetsProvince = assetsProvince;
                u002.minPurchasePrice = minPurchasePrice;
                u002.maxPurchasePrice = maxPurchasePrice;
                u002.assetsType = assetsType;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region 顾问信息
        //P8010501 顾问信息载入
        public string FP8010501()
        {
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return GetUserData(dbma1, userSN);
            }
        }

        //P8010502 个人信息
        public void FP8010502(string maritalStatusTypeSN, bool procreateStatus, string currentAddressProvinceSN, string currentAddressCitySN, string currentAddressDetails, string graduateSchool, string degreeTypeSN, string degreeCard)
        {
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.maritalStatusTypeSN = maritalStatusTypeSN;
                u002.procreateStatus = procreateStatus;
                u002.currentAddressProvinceSN = currentAddressProvinceSN;
                u002.currentAddressCitySN = currentAddressCitySN;
                u002.currentAddressDetails = currentAddressDetails;
                u002.graduateSchool = graduateSchool;
                u002.degreeTypeSN = degreeTypeSN;
                u002.degreeCard = degreeCard;

                dbma1.SubmitChanges();
            }
        }

        //P8010503 工作信息
        public void FP8010503(string workEnterprise, string enterpriseTypeSN, string industryTypeSN, DateTime? hiredate, string workTel, string post, string enterpriseSwitchboard, string enterpriseWebsite, string colleageName, string colleagePhone, string colleageIdCard)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.workEnterprise = workEnterprise;
                u002.enterpriseTypeSN = enterpriseTypeSN;
                u002.industryTypeSN = industryTypeSN;
                u002.hiredate = hiredate;
                u002.workTel = workTel;
                u002.post = post;
                u002.enterpriseSwitchboard = enterpriseSwitchboard;
                u002.enterpriseWebsite = enterpriseWebsite;
                u002.colleageName = colleageName;
                u002.colleagePhone = colleagePhone;
                u002.colleageIdCard = colleageIdCard;

                dbma1.SubmitChanges();
            }
        }

        //P8010504 顾问介绍
        public void FP8010504(string serviceProvinceSN, string serviceCitySN, bool investigate, bool assetsEvaluate, bool creditRightGuarantee, bool badLoanCollect, string consultantDetails)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                u002.serviceProvinceSN = serviceProvinceSN;
                u002.serviceCitySN = serviceCitySN;
                u002.investigate = investigate;
                u002.assetsEvaluate = assetsEvaluate;
                u002.creditRightGuarantee = creditRightGuarantee;
                u002.badLoanCollect = badLoanCollect;
                u002.consultantDetails = consultantDetails;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        //获取用户信息
        private string GetUserData(DBMA1DataContext dbma1, string userSN)
        {
            var userData = (from c in dbma1.VP801011s
                           where c.userSN == userSN
                           select c
                           ).First() ;
            return C101.FC10107(userData);
        }
    }
}