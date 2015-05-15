using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M02
{
    //资产出售
    public class CM0206
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region M020601
        public string FM020601(int pageSize)
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                //数据
                var dataList = dbbc1.B600s.Take(pageSize).ToList();

                //服务状态
                string serverStatusListStr = GetServerStatusList(dataList);

                return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1}}}", C101.FC10107(dataList), serverStatusListStr);
            }
        }
        #endregion

        #region M020602 排序
        public string FM020602(string sortStr, int pageSize)
        {
            using(DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                List<B600> dataList = Sort(dbbc1, sortStr).Take(pageSize).ToList();

                //服务状态
                string serverStatusListStr = GetServerStatusList(dataList);

                return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1}}}", C101.FC10107(dataList), serverStatusListStr);
            }
        }
        #endregion

        #region M0206012 加载更多
        public string FM020612(string sortStr, int pageFrom, int pageSize)
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                List<B600> dataList = Sort(dbbc1, sortStr).Skip(pageFrom).Take(pageSize).ToList();

                //服务状态
                string serverStatusListStr = GetServerStatusList(dataList);

                return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1}}}", C101.FC10107(dataList), serverStatusListStr);
            }
        }
        #endregion

        #region M020603 出售方信息
        public string FM020603(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var sellerData = (from c in dbbs1.VP801001s
                                  where c.userSN == userSN
                                  select new
                                  {
                                      c.userSN,
                                      c.name,
                                      c.birthday,
                                      c.gender,
                                      c.registeredResidence,
                                      idCard = c.idCard.Substring(0, 6) + "*",
                                      phone = c.phone,
                                      c.email,
                                      c.maritalStatusType,
                                      c.procreateStatus,
                                      c.currentAddressProvince,
                                      c.currentAddressCity,
                                      c.currentAddressDetails
                                  }).First();

                return C101.FC10107(sellerData);
            }
        }
        #endregion

        #region M020604 获取已发布资产信息
        public string FM020604(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var vp401001List = (from c in dbbs1.VP401001s
                                    where c.publisherUserSN == userSN
                                    select c).ToList();
                return C101.FC10107(vp401001List);
            }
        }
        #endregion

        #region M020605 获取预约中资产信息
        public string FM020605(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP402011s
                                    where c.receiverUserSN == userSN
                                    select c).ToList();
                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020606 获取已出售资产信息
        public string FM020606(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP404001s
                                    where c.sellerUserSN == userSN
                                    select c).ToList();
                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020607 获取备注信息
        public string FM020607(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                return dbbs1.VP801021s.Where(c => c.userSN == userSN).First().assetsSellingNote;
            }
        }
        #endregion

        #region M020608 保存备注信息
        public void FM020608(string userSN, string note)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                data.assetsSellingNote = note;

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M020609 获取取消发布的资产信息
        public string FM020609(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP301011s
                                where c.publisherUserSN == userSN
                                select c).ToList();
                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020610 取消发布的资产
        //0:成功； 1：失效； 2：失败
        public string FM020610(string assetsSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataVerify = (from c in dbma1.P300s
                            where c.assetsSN == assetsSN
                             && c.cancelDate == null
                             && !c.P400s.Where(p => p.senderCancelReserveDate == null && p.receiverRefuseReserveDate == null).Any()
                            select c).FirstOrDefault();

                if (dataVerify == null)
                {
                    return "1";
                }

                var data = dbma1.P300s.Where(c => c.assetsSN == assetsSN).First();
                data.cancelDate = DateTime.Now;

                //后台操作记录
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "P300";
                b000.pkSN = data.assetsSN;
                b000.actionTypeSN = "B06";
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

        #region M020611 获取拒绝预约的资产信息
        public string FM020611(string userSN)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP302001s
                                where c.receiverUserSN == userSN
                                select c).ToList();
                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020613 修改服务状态
        public void FM020613(string userSN, string status)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                data.assetsSellingStatus = status == "1" ? true : false;

                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "U001";
                b000.pkSN = data.userSN;
                b000.actionTypeSN = status == "1" ? "B07" : "B08";
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
        private IEnumerable<B600> Sort(DBBC1DataContext dbbc1, string sortStr)
        {
            List<string> sortList = new List<string>();
            sortList.Add("cancelPublishAssetsAmount");
            sortList.Add("publishedAssetsAmount");
            sortList.Add("refuseReserveAmount");
            sortList.Add("reservingAssetsAmount");
            sortList.Add("selledAssetsAmount");

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

            string sqlStr = string.Format("select * from B600 {0} {1}", sqlWhereStr, sqlOrderByStr);

            IEnumerable<B600> crDataList = dbbc1.ExecuteQuery<B600>(sqlStr);

            return crDataList;
        }

        //获取服务状态列表
        private string GetServerStatusList(IEnumerable<B600> dataList)
        {
            //服务状态
            List<bool?> serverStatusList = new List<bool?>();
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                foreach (var temp in dataList)
                {
                    U001 u001 = dbma1.U001s.Where(c => c.userSN == temp.userSN).First();
                    serverStatusList.Add(u001.assetsSellingStatus);
                }
            }

            return C101.FC10107(serverStatusList);
        }
    }
}
