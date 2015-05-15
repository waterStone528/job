using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P1
{
    public class CP101
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region FP10101 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        public string FP10101(int pageSize)
        {
            //判断是否登录
            if(session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //判断是否有权限并且是否有逾期账单
                var data1 = dbma1.U001s.Where(c => c.userSN == userSN).First();
                bool ifBillOverdue = C201.FC20153(dbma1, userSN);
                if (data1.creditRightInvestStatus != true || ifBillOverdue == true)
                {
                    return "{\"status\":\"false\"}";
                }

                //获取uip
                string provinceData = C201.FC20121(dbma1).Replace("[", "").Replace("]", "");
                string guaranteeType = C201.FC20117(dbma1).Replace("[", "").Replace("]", "");
                string repaymentType = C201.FC20114(dbma1).Replace("[", "").Replace("]", "");
                string capitalPurposeType = C201.FC20115(dbma1).Replace("[", "").Replace("]", "");
                string repaymentSourceType = C201.FC20116(dbma1).Replace("[", "").Replace("]", "");
                string investorCancelReserveReasonType = C201.FC20132(dbma1).Replace("[", "").Replace("]", "");
                string investorRefuseReserveReasonType = C201.FC20133(dbma1).Replace("[", "").Replace("]", "");

                //获取债权信息
                var crDataList = dbma1.VP101001s.OrderByDescending(c => c.publishDate).Take(pageSize).ToList();
                string crDataListStr = C101.FC10107(crDataList);

                string userName = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                string res = string.Format("{{\"status\":\"true\",\"uipData\":{{\"SltConfigData\":[{0},{1},{2},{3},{4},{5},{6}]}},\"crDate\":{7},\"maxDatetime\":\"{8}\",\"userName\":\"{9}\",\"userSN\":\"{10}\"}}", provinceData, guaranteeType, repaymentType, capitalPurposeType, repaymentSourceType, investorCancelReserveReasonType, investorRefuseReserveReasonType, crDataListStr, DateTime.Now, userName, userSN);

                return res;
            }
        }
        #endregion

        #region FP10102 滚动获取债权信息
        /// <summary>
        /// 滚动获取债权信息
        /// </summary>
        public string FP10102(DateTime maxDatetime, int pageFrom, int pageSize, string keyword, string provinceSN, string citySN, string capitalPurposeSN, string paymentTypeSN, string guaranteeTypeSN, string financingMain, string sortStr)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var searchLinq = Search(dbma1, keyword, provinceSN, citySN, capitalPurposeSN, paymentTypeSN, guaranteeTypeSN, financingMain, sortStr);

                List<VP101001> crList = new List<VP101001>();
                if (sortStr == ",")
                {
                    crList = (from c in searchLinq
                                  where maxDatetime >= c.publishDate
                                  orderby c.publishDate descending
                                  select c).Skip(pageFrom).Take(pageSize).ToList();
                }
                else
                {
                    crList = (from c in searchLinq
                                  where maxDatetime >= c.publishDate
                                  select c).Skip(pageFrom).Take(pageSize).ToList();
                }

                return C101.FC10107(crList);
            }
        }
        #endregion

        #region FP10105 获取隐藏融资方信息
        /// <summary>
        /// 获取隐藏融资方信息
        /// </summary>
        public string FP10105(string financierUserSN)
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
                                          select new
                                          {
                                              c.userSN,
                                              c.birthday,
                                              c.gender,
                                              c.registeredResidence,
                                              idCard = c.idCard.Substring(0,6),
                                              c.maritalStatusType,
                                              c.procreateStatus,
                                              c.healthyStatusType,
                                              c.ifBasicLivingAllowance,
                                              c.graduateSchool,
                                              c.degreeType,
                                              c.enterpriseType,
                                              c.hiredate,
                                              c.post,
                                              c.monthlyTotalIncome,
                                              c.monthlyTotalExpenditure,
                                              c.monthlyNetIncome,
                                              c.totalAssets,
                                              c.totalDebt,
                                              netAssests = c.totalAssets - c.totalDebt,
                                              c.ifCourtImplementation,
                                              c.creditStatus
                                          }).First();
                string financierBasicDataStr = C101.FC10107(financierBasicData);

                string res = string.Format("{{\"financierBasicData\":{0},\"caseAmount\":{1},\"caseMoneyAmount\":{2},\"debtAmount\":{3},\"debtMoneyAmount\":{4},\"currentOverdueAmount\":{5},\"currentOverdueMoneyAmount\":{6},\"historyOverdueAmount\":{7},\"regDatetime\":{8}}}", financierBasicDataStr, caseAmount, caseMoneyAmount, debtAmount, debtMoneyAmount, currentOverdueAmount, currentOverdueMoneyAmount, historyOverdueAmount, regDatetimeStr);

                return res;
            }
        }
        #endregion

        #region FP10106 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        public string FP10106(int pageSize,string keyword, string provinceSN, string citySN, string capitalPurposeSN, string paymentTypeSN, string guaranteeTypeSN, string financingMain, string sortStr)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                if (guaranteeTypeSN == "担保 抵押")
                {
                    guaranteeTypeSN = "担保+抵押";
                }

                var searchCrLinq = Search(dbma1, keyword, provinceSN, citySN, capitalPurposeSN, paymentTypeSN, guaranteeTypeSN, financingMain, sortStr);
                var searchCrList = searchCrLinq.Take(pageSize);
                string dataStr = C101.FC10107(searchCrList);

                return string.Format("{{\"data\":{0},\"maxDatetime\":\"{1}\"}}", dataStr, DateTime.Now);
            }
        }

        //搜索，返回全部数据
        private IEnumerable<VP101001> Search(DBMA1DataContext dbma1, string keyword, string provinceSN, string citySN, string capitalPurposeSN, string paymentTypeSN, string guaranteeTypeSN, string financingMain, string sortStr)
        {
            List<string> sortList = new List<string>();
            sortList.Add("loanDays");
            sortList.Add("financingAmount");
            sortList.Add("dailyRate");
            sortList.Add("mortgageRate");

            string sqlLinkStr = "where";
            string sqlWhereStr = "";

            if (keyword != "")
            {
                sqlWhereStr += string.Format(" {0} creditRightSN like N'%{1}%'", sqlLinkStr, keyword);
                sqlLinkStr = "and";
            }

            if (provinceSN != "区域-省" && provinceSN != "-1")
            {
                sqlWhereStr += string.Format(" {0} province=N'{1}'", sqlLinkStr, provinceSN);
                sqlLinkStr = "and";
            }
            if (citySN != "区域-市" && citySN != "-1")
            {
                sqlWhereStr += string.Format(" {0} city=N'{1}'", sqlLinkStr, citySN);
                sqlLinkStr = "and";
            }
            if (capitalPurposeSN != "资金用途" && capitalPurposeSN != "-1")
            {
                sqlWhereStr += string.Format(" {0} capitalPurposeType=N'{1}'", sqlLinkStr, capitalPurposeSN);
                sqlLinkStr = "and";
            }
            if (paymentTypeSN != "还款方式" && paymentTypeSN != "-1")
            {
                sqlWhereStr += string.Format(" {0} repaymentType=N'{1}'", sqlLinkStr, paymentTypeSN);
                sqlLinkStr = "and";
            }
            if (guaranteeTypeSN != "保证方式" && guaranteeTypeSN != "-1")
            {
                sqlWhereStr += string.Format(" {0} guaranteeType=N'{1}'", sqlLinkStr, guaranteeTypeSN);
                sqlLinkStr = "and";
            }
            if (financingMain != "融资方" && financingMain != "-1")
            {
                bool ifCompany = financingMain == "企业" ? true : false;
                sqlWhereStr += string.Format(" {0} mainFinancing=N'{1}'", sqlLinkStr, ifCompany);
                sqlLinkStr = "and";
            }

            string sqlOrderByStr = "";
            string[] sortStrSplit = sortStr.Split(',');
            for (int i = 1; i < (sortStrSplit.Length - 1); i++)
            {
                string[] sortStrSplitSplit = sortStrSplit[i].Split('#');
                if (i == 1)
                {
                    sqlOrderByStr += "order by";
                }
                else
                {
                    sqlOrderByStr += ",";
                }

                int orderByFieldNum = Convert.ToInt32(sortStrSplitSplit[0]) - 4;
                string orderByFieldName = sortList[orderByFieldNum];
                string order = sortStrSplitSplit[1] == "A" ? "" : " desc";
                sqlOrderByStr += string.Format(" {0} {1}", orderByFieldName, order);
            }

            string sqlStr = string.Format("select * from VP101001 {0} {1}", sqlWhereStr, sqlOrderByStr);

            IEnumerable<VP101001> crDataList = dbma1.ExecuteQuery<VP101001>(sqlStr);

            return crDataList;
        }
        #endregion 

        /// <summary>
        /// 投资方债权预约
        /// </summary>
        public string FP10107(string pwd,string crSN,string financierUserSN)
        {
            string investorSN = session["userSN"].ToString();
            //string investorSN = "U00002";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //预约失效
                if(dbma1.VP101001s.Where(c => c.creditRightSN == crSN).FirstOrDefault() == null)
                {
                    return "false";
                }

                //验证交易密码是否正确
                string transPwd = dbma1.U003s.Where(c => c.userSN == investorSN).First().transactPwd;

                if (C101.FC10104(pwd, transPwd) == false)
                {
                    return "false";
                }

                //从余额中扣除服务费 F000
                A023 a023 = dbma1.A023s.First();
                decimal reserveFee = Convert.ToDecimal(a023.financingReserveCost);

                F000 f000 = dbma1.F000s.Where(c => c.userSN == investorSN).First();
                if (f000.balance < reserveFee)
                {
                    return "false";
                }
                f000.balance -= reserveFee;

                //加入收支明细表中 F003
                string F003max33SN = C101.FC10102("F003", 8, "UA");
                F003 f003 = new F003();
                f003.revenueExpenditureSN = F003max33SN;
                f003.generetorUserSN = investorSN;
                f003.generateDate = DateTime.Now;
                f003.type = "债权预约";
                f003.expenditure = reserveFee;
                f003.balance = f000.balance;
                dbma1.F003s.InsertOnSubmit(f003);

                //预约
                string max33SN = C101.FC10102("P100", 7, "A");
                P100 p100 = new P100();
                p100.reserveSN = max33SN;
                p100.senderUserSN = investorSN;
                p100.receiverUserSN = financierUserSN;
                p100.creditRightSN = crSN;
                p100.sendDate = DateTime.Now;
                dbma1.P100s.InsertOnSubmit(p100);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = investorSN;
                f006.businessSN = max33SN;
                f006.businessType = "债权预约";
                f006.transactionMoneyAmount = reserveFee;
                f006.groupUpValue = reserveFee;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                U003 u003 = dbma1.U003s.Where(c => c.userSN == financierUserSN).First();

                //如有邮箱提醒
                if (u003.billGenerate_email == true)
                {
                    SendByEmail(dbma1, financierUserSN, crSN);
                }
                //如有短信提醒
                if (u003.billGenerate_shortMessage == true)
                {
                    SendBySM(dbma1, financierUserSN, crSN);
                }

                dbma1.SubmitChanges();

                return "true";
            }
        }

        //预约发送邮件提醒
        private int SendByEmail(DBMA1DataContext dbma1, string financierSN, string crSN)
        {
            string subject = "【凡奇金融】债权预约";
            string content = string.Format("亲爱的凡奇用户，您的债权({0})已经被预约，请您查看。", crSN);
            return C101.FC10105(dbma1, financierSN, subject, content);
        }

        //预约发送短信提醒
        private void SendBySM(DBMA1DataContext dbma1, string financierSN, string crSN)
        {
            string phoneNum = dbma1.U000s.Where(c => c.userSN == financierSN).First().phone;

            string content = string.Format("亲爱的凡奇用户，您的债权({0})已经被预约，请您查看。", crSN);
            Comm.SM.F001(dbma1, financierSN, phoneNum, content, 1, true);
        }

        #region P10109 获取账户余额及债权预约费用
        public string FP10109()
        {
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //发布费用
                A023 a023 = dbma1.A023s.First();
                decimal? reserveFee = a023.financingReserveCost;

                //账户余额
                F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                decimal balance = f000.balance;

                return string.Format("{{\"reserveFee\":\"{0}\",\"balance\":\"{1}\"}}", reserveFee, balance);
            }
        }
        #endregion 
    }
}
