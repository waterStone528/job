using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;

namespace CSL.M06
{
    //流水明细
    public  class CM0604
    {
        #region M0604INIT 初始化
        public string FM0604INIT(int pageSize)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var linqDataList = (from c in dbbs1.VF003s
                                    select new
                                    {
                                        userSN = c.generetorUserSN,
                                        c.revenueExpenditureSN,
                                        c.generateDate,
                                        c.type,
                                        c.revenue,
                                        c.expenditure,
                                    }
                                    ).Take(pageSize).ToList();
                var s = string.Format("{{\"data\":{0},{1}}}", C101.FC10107(linqDataList), SumRevenueAndExpenditure(dbbs1));
                return s;
            }
        }
        #endregion

        #region M060402 删选
        public string FM060402(string sortStr, int pageSize)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var linqDataList = (from c in Sort(dbbs1,sortStr)
                                    select new
                                    {
                                        userSN = c.generetorUserSN,
                                        c.revenueExpenditureSN,
                                        c.generateDate,
                                        c.type,
                                        c.revenue,
                                        c.expenditure,
                                    }
                                    ).Take(pageSize).ToList();
                return string.Format("{{\"data\":{0},{1}}}", C101.FC10107(linqDataList), SumRevenueAndExpenditure(dbbs1));
            }
        }
        #endregion

        #region M060401 加载更多
        public string FM060401(string sortStr, int pageFrom, int pageSize)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var linqDataList = (from c in Sort(dbbs1, sortStr)
                                    select new
                                    {
                                        userSN = c.generetorUserSN,
                                        c.revenueExpenditureSN,
                                        c.generateDate,
                                        c.type,
                                        c.revenue,
                                        c.expenditure,
                                    }
                                    ).Skip(pageFrom).Take(pageSize).ToList();
                return string.Format("{{\"data\":{0},{1}}}", C101.FC10107(linqDataList), SumRevenueAndExpenditure(dbbs1));
            }
        }
        #endregion


        //删选
        private IEnumerable<VF003> Sort(DBBS1DataContext dbbs1, string sortStr)
        {
            string sqlWhereStr = "";
            string[] sortStrSplit = sortStr.Split(',');
            for (int i = 1; i < (sortStrSplit.Length - 1); i++)
            {
                string[] sortStrSplitSplit = sortStrSplit[i].Split('#');
                if (sortStrSplitSplit[0] == "1")
                {
                    if (sqlWhereStr == "")
                    {
                        sqlWhereStr = string.Format("where CONVERT(varchar(100), generateDate, 111) like N'%{0}%'", sortStrSplitSplit[1]);
                    }
                    else
                    {
                        sqlWhereStr = string.Format(" and CONVERT(varchar(100), generateDate, 111) like N'%{0}%'", sortStrSplitSplit[1]);
                    }
                    
                    continue;
                }

                if (sortStrSplitSplit[0] == "2")
                {
                    if (sqlWhereStr == "")
                    {
                        sqlWhereStr = string.Format("where type like N'%{0}%'", sortStrSplitSplit[1]);
                    }
                    else
                    {
                        sqlWhereStr = string.Format(" and type like N'%{0}%'", sortStrSplitSplit[1]);
                    }

                    continue;
                }
            }

            string sqlStr = string.Format("select * from VF003 {0}", sqlWhereStr);

            IEnumerable<VF003> crDataList = dbbs1.ExecuteQuery<VF003>(sqlStr);

            return crDataList;
        }

        //合计充值和消费
        private string SumRevenueAndExpenditure(DBBS1DataContext dbbs1)
        {
            decimal revenueTotal = Convert.ToDecimal(dbbs1.VF003s.Sum(c => c.revenue));
            decimal expenditure = Convert.ToDecimal(dbbs1.VF003s.Sum(c => c.expenditure));

            return string.Format("\"revenueTotal\":\"{0}\",\"expenditure\":\"{1}\"", revenueTotal, expenditure);
        }
    }
}
