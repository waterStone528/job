using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M02
{
    //债权融资
    public class CM0201
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region INIT 
        public string FINIT(int pageSize)
        {
            using(DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                //数据
                var dataList = dbbc1.B100s.Take(pageSize).ToList();

                //服务状态
                string serverStatusListStr = GetServerStatusList(dataList);

                return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1}}}", C101.FC10107(dataList), serverStatusListStr);
            }
        }
        #endregion

        #region M020112 排序
        public string FM020112(string sortStr,int pageSize)
        {
            using(DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                var dataList = Sort(dbbc1, sortStr).Take(pageSize).ToList();

                //服务状态
                string serverStatusListStr = GetServerStatusList(dataList);

                return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1}}}", C101.FC10107(dataList), serverStatusListStr);
            }
        }
        #endregion

        #region M020113 加载更多
        public string FM020113(int pageFrom,int pageSize,string sortStr)
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                var dataList = Sort(dbbc1, sortStr).Skip(pageFrom).Take(pageSize).ToList();

                //服务状态
                string serverStatusListStr = GetServerStatusList(dataList);

                return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1}}}", C101.FC10107(dataList), serverStatusListStr);
            }
        }
        #endregion

        #region M020101 用户信息
        public string FM020101(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                //案例情况
                var caseStatus = dbbs1.P102s.Where(c => c.financierUserSN == userSN);
                int caseAmount = caseStatus.Count();
                decimal? caseMoneyAmount = caseAmount == 0 ? 0 : caseStatus.Sum(c => c.investMoneyAmount);

                //债务情况
                var debtStatus = dbbs1.P102s.Where(c => c.financierUserSN == userSN && c.closeCaseDate == null);
                int debtAmount = debtStatus.Count();
                decimal? debtMoneyAmount = debtAmount == 0 ? 0 : debtStatus.Sum(c => c.investMoneyAmount);

                //当前逾期情况
                var currentOverdueStatus = from c in dbbs1.P102s
                                           where c.financierUserSN == userSN
                                            && c.closeCaseDate == null
                                            && c.investDate.Date < DateTime.Now.Date
                                           select c;
                int currentOverdueAmount = currentOverdueStatus.Count();
                decimal? currentOverdueMoneyAmount = currentOverdueAmount == 0 ? 0 : currentOverdueStatus.Sum(c => c.investMoneyAmount);

                //历史逾期数量
                var historyOverdueStatus = from c in dbbs1.P102s
                                           from p in dbbs1.P103s
                                           where c.investSN == p.investSN
                                            && c.financierUserSN == userSN
                                            && c.investDate.Date < p.repayDate.Date
                                           select c;
                int historyOverdueAmount = historyOverdueStatus.Count();

                //注册时间
                DateTime regDatetime = dbbs1.VU000s.Where(c => c.userSN == userSN).First().registerDate;
                string regDatetimeStr = C101.FC10107(regDatetime);

                //基本信息
                var financierBasicData = (from c in dbbs1.VP801001s
                                          where c.userSN == userSN
                                          select c).First();
                string financierBasicDataStr = C101.FC10107(financierBasicData);

                string res = string.Format("{{\"financierBasicData\":{0},\"caseAmount\":{1},\"caseMoneyAmount\":{2},\"debtAmount\":{3},\"debtMoneyAmount\":{4},\"currentOverdueAmount\":{5},\"currentOverdueMoneyAmount\":{6},\"historyOverdueAmount\":{7},\"regDatetime\":{8}}}", financierBasicDataStr, caseAmount, caseMoneyAmount, debtAmount, debtMoneyAmount, currentOverdueAmount, currentOverdueMoneyAmount, historyOverdueAmount, regDatetimeStr);

                return res;
            }
        }

        #endregion

        #region M020103 获取取消发布债权
        public string FM020103(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP201041s.Where(c => c.publisherUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion 

        #region M020104 已发布债权
        public string FM020104(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP101001s.Where(c => c.publisherUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020102 取消发布的债权
        //0:成功； 1：失效； 2：失败
        public string FM020102(string crSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataVerify = (from c in dbma1.P200s
                                  where c.creditRightSN == crSN
                                     && c.cancelDate == null
                                     && !dbma1.P203s.Where(o => o.creditRightSN == crSN && o.senderCancelReserveDate == null && o.receiverRefuseReserveDate == null).Any()
                                     && !dbma1.P100s.Where(p => p.creditRightSN == crSN && p.senderCancelReserveDate == null && p.receiverRefuseReserveDate == null).Any()
                                  select c).FirstOrDefault();

                if (dataVerify == null)
                {
                    return "1";
                }

                var data = dbma1.P200s.Where(c => c.creditRightSN == crSN).First();
                data.cancelDate = DateTime.Now;

                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "P200";
                b000.pkSN = data.creditRightSN;
                b000.actionTypeSN = "B01";
                int internalUserSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int internalUserSN = 1032;
                b000.operatorSN = internalUserSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();

                return "0";
            }
        }
        #endregion

        #region F020105 取消预约
        public string FM020105(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP202041s.Where(c => c.financierUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020106 拒绝预约
        public string FM020106(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP202061s.Where(c => c.financierUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020107 预约中债权
        public string FM020107(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP202081s.Where(c => c.financierUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion 

        #region M020108 还款中债权
        public string FM020108(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP203011s.Where(c => c.financierUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020109 已还款债权
        public string FM020109(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP203091s.Where(c => c.financierUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020110 查看备注
        public string FM020110(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var data = dbbs1.VP801021s.Where(c => c.userSN == userSN).First();

                return data.creditRightFinancingNote == null ? "" : data.creditRightFinancingNote.Trim();
            }
        }
        #endregion 

        #region M020111 保存备注
        public void FM020111(string userSN,string note)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();

                data.creditRightFinancingNote = note;
                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M0201014 修改服务状态
        public void FM020114(string userSN, string status)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                data.creditRightFinancingStatus = status == "1" ? true : false;

                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "U001";
                b000.pkSN = data.userSN;
                b000.actionTypeSN = status == "1" ? "B02" : "B03";
                int internalUserSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int internalUserSN = 1032;
                b000.operatorSN = internalUserSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        //排序
        private IEnumerable<B100> Sort(DBBC1DataContext dbbc1, string sortStr)
        {
            List<string> sortList = new List<string>();
            sortList.Add("cancelPublishCrAmount");
            sortList.Add("publishedCrAmount");
            sortList.Add("cancelReserveAmount");
            sortList.Add("refuseReserveAmount");
            sortList.Add("reservingCrAmount");
            sortList.Add("repayingCrAmount");
            sortList.Add("repayedCrAmount");
    
            string sqlWhereStr = "";
            string sqlOrderByStr = "";
            string[] sortStrSplit = sortStr.Split(',');
            for (int i = 1; i < (sortStrSplit.Length - 1); i++)
            {
                string[] sortStrSplitSplit = sortStrSplit[i].Split('#');
                if (sortStrSplitSplit[0] == "1")
                {
                    sqlWhereStr = string.Format("where userSN like '%{0}%'", sortStrSplitSplit[1]);

                    continue;
                }

                if (sqlOrderByStr == "")
                {
                    sqlOrderByStr += "order by";
                }
                else
                {
                    sqlOrderByStr += ",";
                }

                int orderByFieldNum = Convert.ToInt32(sortStrSplitSplit[0]) - 2;
                string orderByFieldName = sortList[orderByFieldNum];
                string order = sortStrSplitSplit[1] == "A" ? "" : " desc";
                sqlOrderByStr += string.Format(" {0} {1}", orderByFieldName, order);
            }

            string sqlStr = string.Format("select * from B100 {0} {1}", sqlWhereStr, sqlOrderByStr);

            IEnumerable<B100> crDataList = dbbc1.ExecuteQuery<B100>(sqlStr);

            return crDataList;
        }

        //获取实时服务状态
        private string GetServerStatusList(IEnumerable<B100> dataList)
        {
            //服务状态
            List<bool?> serverStatusList = new List<bool?>();
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                foreach (var b100 in dataList)
                {
                    U001 u001 = dbma1.U001s.Where(c => c.userSN == b100.userSN).First();
                    serverStatusList.Add(u001.creditRightFinancingStatus);
                }
            }

            return C101.FC10107(serverStatusList);
        }
    }
}
