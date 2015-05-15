using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P7
{
    //扩广活动
    public class CP702
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P70201 注册赠送
        public void FP70201()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00004";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                decimal rewardAmount = Convert.ToInt32(dbma1.A031s.First().regPresentV);

                //更新账户表 F000
                F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                f000.balance += rewardAmount;
                f000.lastOperate = DateTime.Now;

                //添加奖励表 F004
                string F004Max33SN = C101.FC10102("F004", 7, "UB");
                F004 f004 = new F004();
                f004.rewardSN = F004Max33SN;
                f004.userSN = userSN;
                f004.rewardAmount = rewardAmount;
                f004.rewardDate = DateTime.Now;
                f004.rewardType = "注册赠送";
                dbma1.F004s.InsertOnSubmit(f004);

                //添加收支明细表 F003
                string F003Max33SN = C101.FC10102("F003", 8, "UA");
                F003 f003 = new F003();
                f003.revenueExpenditureSN = F003Max33SN;
                f003.generetorUserSN = userSN;
                f003.generateDate = DateTime.Now;
                f003.type = "注册赠送";
                f003.revenue = rewardAmount;
                f003.balance = f000.balance;
                f003.referSN = F004Max33SN;
                dbma1.F003s.InsertOnSubmit(f003);

                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
