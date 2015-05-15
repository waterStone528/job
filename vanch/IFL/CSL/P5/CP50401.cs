using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P5
{
    //已审核债权
    public class CP50401
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P5040101 初始化
        public string FP5040101(int pageSize)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var crDataList = (from c in dbma1.VP504011s
                                  where c.consultantUserSN == consultantUserSN
                                    && c.closeCaseDate == null
                                  orderby c.serverDate descending
                                  select c).Take(pageSize).ToList();
                var crDataListStr = C101.FC10107(crDataList);

                return string.Format("{{\"crDataList\":{0},\"maxDatetime\":\"{1}\"}}", crDataListStr, DateTime.Now);
            }
        }
        #endregion

        #region P5040102 滚动加载
        public string FP5040102(DateTime maxDatetime,int pageFrom,int pageSize)
        {
            string consultantUserSN = session["userSN"].ToString();
            //string consultantUserSN = "U00003";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var crDataList = (from c in dbma1.VP504011s
                                  where c.consultantUserSN == consultantUserSN
                                    && c.closeCaseDate == null
                                    && maxDatetime >= c.serverDate
                                  orderby c.serverDate descending
                                  select c).Skip(pageFrom).Take(pageSize).ToList();
                return C101.FC10107(crDataList);
            }
        }
        #endregion

        #region P5040104 融资方历史信息
        public string FP5040104(string crSN, string financierUserSN)
        {
            string financierHistoryDataStr = string.Empty;
            string regDatetimeStr = string.Empty;

            //基本信息
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var financierHistoryData = (from c in dbma1.VP105011s
                                            where c.creditRightSN == crSN
                                            select c).First();
                financierHistoryDataStr = C101.FC10107(financierHistoryData);

                //注册时间
                DateTime regDatetime = dbma1.U000s.Where(c => c.userSN == financierUserSN).First().registerDate;
                regDatetimeStr = C101.FC10107(regDatetime);
            }

            return string.Format("{{\"historyData\":{0},\"regDatetime\":{1}}}", financierHistoryDataStr, regDatetimeStr);
        }
        #endregion 
    }
}
