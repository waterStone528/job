using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M02
{
    //债权投资
    public class CM0202
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region INIT 
        public string FINIT(int pageSize)
        {
            string pawnStr = string.Empty;
            string dataListStr = string.Empty;

            using(DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                //数据
                var dataList = dbbc1.B200s.Take(pageSize).ToList();
                dataListStr = C101.FC10107(dataList);

                using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
                {
                    //uip
                    pawnStr = C102.FC20108(dbbs1);

                    //服务状态
                    string serverStatusListStr = GetServerStatusList(dataList);

                    return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1},\"pawn\":{2}}}", C101.FC10107(dataList), serverStatusListStr, pawnStr);
                }
            }
        }
        #endregion

        #region M020209 排序
        public string FM020209(string sortStr,int pageSize)
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

        #region M020210 加载更多
        public string FM020210(int pageFrom, int pageSize, string sortStr)
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

        #region M020201 用户信息
        public string FM020201(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var data = (from c in dbbs1.VP801001s
                            where c.userSN == userSN
                            select c).First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M020202 取消预约
        public string FM020202(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP102031s.Where(c => c.investorUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020203 拒绝预约
        public string FM020203(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP102051s.Where(c => c.investorUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020204 预约中债权
        public string FM020204(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP202081s.Where(c => c.investorUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020205 已投资债权
        public string FM020205(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP105051s.Where(c => c.investorUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020206 已结案债权
        public string FM020206(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP105061s.Where(c => c.investorUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020207 获取备注信息
        public string FM020207(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                return dbbs1.VP801021s.Where(c => c.userSN == userSN).First().creditRightInvestNote;
            }
        }
        #endregion

        #region M020208 保存备注信息
        public void FM020208(string userSN, string note)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                data.creditRightInvestNote = note;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M0202011 修改服务状态
        public void FM020211(string userSN, string status)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                data.creditRightInvestStatus = status == "1" ? true : false;

                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "U001";
                b000.pkSN = data.userSN;
                b000.actionTypeSN = status == "1" ? "B04" : "B05";
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
        private IEnumerable<B200> Sort(DBBC1DataContext dbbc1, string sortStr)
        {
            List<string> sortList = new List<string>();
            sortList.Add("cancelReserveAmount");
            sortList.Add("refuseReserveAmount");
            sortList.Add("reservingCrAmount");
            sortList.Add("investedCrAmount");
            sortList.Add("closedCaseCrAmount");

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

            string sqlStr = string.Format("select * from B200 {0} {1}", sqlWhereStr, sqlOrderByStr);

            IEnumerable<B200> crDataList = dbbc1.ExecuteQuery<B200>(sqlStr);

            return crDataList;
        }

        //获取服务状态列表
        private string GetServerStatusList(IEnumerable<B200> dataList)
        {
            //服务状态
            List<bool?> serverStatusList = new List<bool?>();
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                foreach (var b200 in dataList)
                {
                    U001 u001 = dbma1.U001s.Where(c => c.userSN == b200.userSN).First();
                    serverStatusList.Add(u001.creditRightInvestStatus);
                }
            }

            return C101.FC10107(serverStatusList);
        }
    }
}
