using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P7
{
    //成长值
    public class CP701
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P70101 VIP等级
        public string FP70101()
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //用户名
                string userName = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                //vip等级设置
                A029 a029 = dbma1.A029s.First();
                string a029Str = C101.FC10107(a029);

                //当前成长值
                decimal currentGroupValue = Convert.ToInt32(dbma1.F006s.Where(c => c.userSN == userSN).Sum(c => c.groupUpValue));

                //当前vip等级及奖励率
                string vipLevel = string.Empty;
                decimal rewardRate;
                if (currentGroupValue < a029.vip1_originateValue)
                {
                    vipLevel = "0";
                    rewardRate = 0;
                }
                else if (currentGroupValue < a029.vip2_originateValue)
                {
                    vipLevel = "1";
                    rewardRate = a029.vip1_rewardRate;
                }
                else if (currentGroupValue < a029.vip3_originateValue)
                {
                    vipLevel = "2";
                    rewardRate = a029.vip2_rewardRate;
                }
                else if (currentGroupValue < a029.vip4_originateValue)
                {
                    vipLevel = "3";
                    rewardRate = a029.vip3_rewardRate;
                }
                else if (currentGroupValue < a029.vip5_originateValue)
                {
                    vipLevel = "4";
                    rewardRate = a029.vip4_rewardRate;
                }
                else if (currentGroupValue < a029.vip6_originateValue)
                {
                    vipLevel = "5";
                    rewardRate = a029.vip5_rewardRate;
                }
                else if (currentGroupValue < a029.vip7_originateValue)
                {
                    vipLevel = "6";
                    rewardRate = a029.vip6_rewardRate;
                }
                else
                {
                    vipLevel = "7";
                    rewardRate = a029.vip7_rewardRate;
                }

                //已奖励的V币
                int rewardV = Convert.ToInt32
                                  (
                                    (from c in dbma1.F004s
                                     where c.userSN == userSN
                                      && c.rewardType == "充值奖励"
                                     select c.rewardAmount).Sum()
                                   );

                return string.Format("{{\"userName\":\"{0}\",\"vipConfig\":{1},\"currentGroupValue\":\"{2}\",\"vipLevel\":\"{3}\",\"rewardRate\":\"{4}\",\"rewardV\":\"{5}\",\"userSN\":\"{6}\"}}", userName, a029Str, currentGroupValue, vipLevel, rewardRate, rewardV, userSN);
            }
        }
        #endregion

        #region P70102 成长明细初始化
        public string FP70102(int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var groupDataList = (from c in dbma1.F006s
                                     where c.userSN == userSN
                                     select new
                                     {
                                         c.acquireDate,
                                         c.businessSN,
                                         c.businessType,
                                         c.transactionMoneyAmount,
                                         c.groupUpValue
                                     }).Take(pageSize).ToList();

                return string.Format("{{\"dataList\":{0},\"maxDatetime\":\"{1}\"}}", C101.FC10107(groupDataList), DateTime.Now);
            }
        }
        #endregion 

        #region P70103 成长明细-滚动加载
        public string FP70103(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var groupDataList = (from c in dbma1.F006s
                                     where c.userSN == userSN
                                        && maxDatetime > c.acquireDate
                                     select new
                                     {
                                         c.acquireDate,
                                         c.businessSN,
                                         c.businessType,
                                         c.transactionMoneyAmount,
                                         c.groupUpValue
                                     }).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(groupDataList);
            }
        }
        #endregion 
    }
}
