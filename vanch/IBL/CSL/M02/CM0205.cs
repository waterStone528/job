using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M02
{
    //客户分配
    public class CM0205
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region M020501 初始化
        public string FM020501(int pageSize)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VP801031s.Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020506 根据客户经理筛选
        public string FM020506(string filterStr, int pageSize)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP801031s
                                where c.work_num.Contains(filterStr)
                                select c).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020503 滚动加载
        public string FM020503(string filterStr, int pageFrom,int pageSize)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = (from c in dbbs1.VP801031s
                                where c.work_num.Contains(filterStr)
                                select c).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion 

        #region M020502 获取客户经理列表
        public string FM020502()
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                var dataList = dbbc1.B500s.ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020505 客户经理列表排序
        public string FM020505(string sortStr)
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                List<string> sortList = new List<string>();
                sortList.Add("reg_date");
                sortList.Add("clientTotalAmount");
                sortList.Add("financierAmount");
                sortList.Add("investorAmount");
                sortList.Add("consultantAmount");
                sortList.Add("sellerAmount");
                sortList.Add("purchaserAmount");

                string sqlOrderByStr = "";
                string[] sortStrSplit = sortStr.Split(',');
                for (int i = 1; i < (sortStrSplit.Length - 1); i++)
                {
                    string[] sortStrSplitSplit = sortStrSplit[i].Split('#');

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

                string sqlStr = string.Format("select * from B500 {0}", sqlOrderByStr);

                var dataList = dbbc1.ExecuteQuery<B500>(sqlStr).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M020504 新增或者修改客户经理 
        //成功："0"； 客户经理无效："1"
        public string FM020504(string userSN,string workNum)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                int internalUserSN = dbma1.internal_users.Where(c => c.work_num == workNum).First().internal_user_id;

                //查看客户经理是否有效
                var data = (from c in dbma1.B003s
                            where c.deleteDate == null
                             && c.internalUserSN == internalUserSN
                             && c.roleTypeSN.Trim() == "B01"
                            select c).FirstOrDefault(); 
                if(data == null)
                {
                    return "1";
                }

                //修改客户经理
                B005 b005 = (from c in dbma1.B005s
                             where c.userSN == userSN
                             select c).First();
                b005.internalUserSN = internalUserSN;
                b005.assignDate = DateTime.Now;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "B005";
                b000.pkSN = b005.userSN;
                b000.actionTypeSN = "B0D";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();

                return "0";
            }
        }
        #endregion
    }
}
