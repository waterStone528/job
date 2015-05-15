using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;

namespace CSL.M06
{
    //用户账户
    public class CM0601
    {
        #region INIT 初始化
        public string FM0601INIT(int pageSize)
        {
            using(DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                var dataList = dbbc1.M0601s.ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M060101 加载更多
        public string FM060101(string sortStr, int pageFrom ,int pageSize)
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                var dataList = Sort(dbbc1, sortStr).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M060102 筛选排序
        public string FM060102(string sortStr,int pageSize)
        {
            using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
            {
                var dataList = Sort(dbbc1, sortStr).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M060103 应收账单信息
        public string FM060103(string userSN)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                VA028 a028 = dbbs1.VA028s.First();
                int needPayDays = Convert.ToInt32(a028.needPayDays);
                decimal overdueRateDaily = Convert.ToDecimal(a028.overdueRateDaily);

                var dataList = (from c in dbbs1.VF001s
                                where c.payerUserSN == userSN
                                    && !dbbs1.VF002s.Where(o => o.billSN == c.billSN).Any()
                                select new
                                {
                                    c.billSN,
                                    c.businessSN,
                                    c.billType,
                                    c.MoneyAmount,
                                    c.generateDate,
                                    needPayDate = c.generateDate.AddDays(needPayDays),
                                    lateFee = c.generateDate.AddDays(needPayDays) > DateTime.Now ? 0 : c.MoneyAmount * overdueRateDaily * ((DateTime.Now - c.generateDate.AddDays(needPayDays)).Days - 1) 
                                }).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        //排序
        private IEnumerable<M0601> Sort(DBBC1DataContext dbbc1, string sortStr)
        {
            List<string> sortList = new List<string>();
            sortList.Add("consumptionAmount");
            sortList.Add("balanceAmount");
            sortList.Add("needPayBillAmount");
            sortList.Add("needPayAmount");
            sortList.Add("overDueAmount");

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

            string sqlStr = string.Format("select * from M0601 {0} {1}", sqlWhereStr, sqlOrderByStr);

            IEnumerable<M0601> crDataList = dbbc1.ExecuteQuery<M0601>(sqlStr);

            return crDataList;
        }
    }
}
