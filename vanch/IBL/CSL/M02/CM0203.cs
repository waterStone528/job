using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M02
{
    //资产购买
    public class CM0203
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region Init 
        public string FM0203INIT(int pageSize)
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                //数据
                var dataList = dbbc1.B300s.Take(pageSize).ToList();

                using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
                {
                    //uip
                    string pawnStr = C102.FC20108(dbbs1);

                    //服务状态
                    string serverStatusListStr = GetServerStatusList(dataList);

                    return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1},\"pawn\":{2}}}", C101.FC10107(dataList), serverStatusListStr, pawnStr);
                }
            }
        }
        #endregion

        #region M020303 排序
        public string FM020303(string sortStr, int pageSize)
        {
             using(DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                List<B300> dataList = Sort(dbbc1, sortStr).Take(pageSize).ToList();

                    //服务状态
                string serverStatusListStr = GetServerStatusList(dataList);

                return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1}}}", C101.FC10107(dataList), serverStatusListStr);
            }
        }
        #endregion 

        #region M020310 加载更多
        public string FM020310(int pageFrom, int pageSize, string sortStr)
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

        #region M020304 用户信息
        public string FM020304(string purchaserUserSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var purchaserData = (from c in dbbs1.VP801001s
                                     where c.userSN == purchaserUserSN
                                     select new
                                     {
                                         c.userSN,
                                         c.name,
                                         c.birthday,
                                         c.gender,
                                         c.registeredResidence,
                                         idCard = c.idCard.Substring(0, 6) + "*",
                                         phone = c.phone.Substring(0, 3) + "****" + c.phone.Substring(7, 4),
                                         c.email,
                                         c.maritalStatusType,
                                         c.procreateStatus,
                                         c.currentAddressProvince,
                                         c.currentAddressCity,
                                         c.currentAddressDetails,
                                         c.assetsProvince,
                                         c.minPurchasePrice,
                                         c.maxPurchasePrice,
                                         c.assetsType
                                     }).First();

                return C101.FC10107(purchaserData);
            }
        }
        #endregion

        #region M020305 取消预约
        public string FM020305(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP402021s.Where(c => c.senderUserSN == userSN).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020306 获取预约中资产信息
        public string FM020306(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP402011s
                                where c.senderUserSN == userSN
                                select c).ToList();
                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020307 获取已购买资产信息
        public string FM020307(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP404001s
                                where c.purchaserUserSN == userSN
                                select c).ToList();
                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020308 获取备注信息
        public string FM020308(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                return dbbs1.VP801021s.Where(c => c.userSN == userSN).First().assetsPurchaseNote;
            }
        }
        #endregion

        #region M020309 保存备注信息
        public void FM020309(string userSN, string note)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                data.assetsPurchaseNote = note;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M020311 修改服务状态 
        public void FM020311(string userSN, string status)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                data.assetsPruchaseStatus = status == "1" ? true : false;

                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "U001";
                b000.pkSN = data.userSN;
                b000.actionTypeSN = status == "1" ? "B09" : "B0A";
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
        private IEnumerable<B300> Sort(DBBC1DataContext dbbc1, string sortStr)
        {
            List<string> sortList = new List<string>();
            sortList.Add("cancelReserveAmount");
            sortList.Add("reservingAmount");
            sortList.Add("purchasedAssetsAmount");

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

            string sqlStr = string.Format("select * from B300 {0} {1}", sqlWhereStr, sqlOrderByStr);

            IEnumerable<B300> crDataList = dbbc1.ExecuteQuery<B300>(sqlStr);

            return crDataList;
        }

        //获取服务状态列表
        private string GetServerStatusList(IEnumerable<B300> dataList)
        {
            //服务状态
            List<bool?> serverStatusList = new List<bool?>();
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                foreach (var b300 in dataList)
                {
                    U001 u001 = dbma1.U001s.Where(c => c.userSN == b300.userSN).First();
                    serverStatusList.Add(u001.assetsPruchaseStatus);
                }
            }

            return C101.FC10107(serverStatusList);
        }
    }
}
