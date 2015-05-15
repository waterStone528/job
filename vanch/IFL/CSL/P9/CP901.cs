using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P9
{
    //账户信息
    public class CP901
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P90101 初始化
        public string FP90101()
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                A029 a029 = dbma1.A029s.First();

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

                //账户预约
                decimal balanceV = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                string name = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                return string.Format("{{\"vipLevel\":\"{0}\",\"vipRate\":\"{1}\",\"balance\":\"{2}\",\"name\":\"{3}\",\"userSN\":\"{4}\"}}", vipLevel, rewardRate, balanceV, name, userSN);
            }
        }
        #endregion

        #region P90102 充值付款
        public string FP90102(decimal rechargeAmount,string cardType,string bankId)
        {
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                F005 f005 = new F005();
                f005.rechargeSN = C101.FC10102("F005", 7, "UC");
                f005.userSN = userSN;
                f005.rechargeAmount = rechargeAmount;
                f005.rechargeDate = DateTime.Now;
                f005.cardType = cardType;
                f005.bankId = bankId;

                dbma1.F005s.InsertOnSubmit(f005);
                dbma1.SubmitChanges();

                return string.Format("{0}%{1}", f005.rechargeSN, userSN);
            }
        }
        #endregion

        #region FP90110 第三方支付回调处理函数
        public void FP90110(string billNo, decimal amount, string ifSuc, string ipsBillNo, string bankBillNo, string userSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //获得vip奖励比率
                //decimal rewardRate = 1;
                decimal rewardRate = C201.FC20152(dbma1, userSN);

                //充值记录表 F005
                F005 f005 = dbma1.F005s.Where(c => c.rechargeSN == billNo).First();
                f005.ifSuccess = ifSuc == "Y" ? true : false;
                f005.ipsBillNo = ipsBillNo;
                f005.bankBillNo = bankBillNo;

                if (ifSuc == "Y")
                {
                    //充值写入余额表 F000
                    F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                    f000.balance += amount;

                    //充值写入流水明细表 F003
                    F003 f003_1 = new F003();
                    f003_1.revenueExpenditureSN = C101.FC10102("F003", 8, "UA");
                    f003_1.generetorUserSN = userSN;
                    f003_1.generateDate = DateTime.Now;
                    f003_1.type = "账户充值";
                    f003_1.revenue = amount;
                    f003_1.balance = f000.balance;
                    f003_1.referSN = f005.rechargeSN;
                    dbma1.F003s.InsertOnSubmit(f003_1);

                    if (rewardRate > 0)
                    {
                        //奖励表 F004
                        F004 f004 = new F004();
                        f004.rewardSN = C101.FC10102("F004", 7, "UB");
                        f004.userSN = userSN;
                        f004.rewardAmount = amount * rewardRate;
                        f004.rewardDate = DateTime.Now;
                        f004.referSN = billNo;
                        f004.rewardType = "充值奖励";
                        dbma1.F004s.InsertOnSubmit(f004);

                        //奖励写入余额表 F000
                        f000.balance += Convert.ToDecimal(f004.rewardAmount);

                        //奖励写入流水明细表 F003
                        F003 f003_2 = new F003();
                        f003_2.revenueExpenditureSN = C101.FC10102("F003", 8, "UA");
                        f003_2.generetorUserSN = userSN;
                        f003_2.generateDate = DateTime.Now;
                        f003_2.type = "充值奖励";
                        f003_2.revenue = f004.rewardAmount;
                        f003_2.balance = f000.balance;
                        f003_2.referSN = f004.rewardSN;

                        dbma1.F003s.InsertOnSubmit(f003_2);
                    }
                }

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region 用户点击充值成功或者失败 P901020101
        public string FP901020101(string billNo)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                return dbma1.F005s.Where(c => c.rechargeSN == billNo).First().ifSuccess == true ? "1" : "0";
            }
        }
        #endregion

        public void FP90111(string s)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                dbma1.U002s.First().consultantDetails = s;

                dbma1.SubmitChanges();
            }
        }
    }
}

