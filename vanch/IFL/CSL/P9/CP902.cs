using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P9
{
    //账单信息
    public class CP902
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P90201 初始化
        public string FP90201(int pageSize)
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string userName = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                A028 a028 = dbma1.A028s.First();
                int needPayDays = Convert.ToInt32(a028.needPayDays);
                decimal overdueRateDaily = Convert.ToDecimal(a028.overdueRateDaily);

                var billList = (from c in dbma1.F001s
                                where c.payerUserSN == userSN
                                    && !c.F002s.Any()
                                orderby c.generateDate descending
                                select new
                                {
                                    c.billSN,
                                    c.businessSN,
                                    c.billType,
                                    c.MoneyAmount,
                                    c.generateDate,
                                    needPayDate = c.generateDate.AddDays(needPayDays),
                                    lateFee = (DateTime.Now - c.generateDate).Days > needPayDays ? c.MoneyAmount * overdueRateDaily * ((DateTime.Now - c.generateDate).Days - needPayDays) : 0
                                }).Take(pageSize).ToList();

                return string.Format("{{\"dataList\":{0},\"maxDatetime\":\"{1}\",\"userName\":\"{2}\",\"userSN\":\"{3}\"}}", C101.FC10107(billList), DateTime.Now, userName, userSN);
            }
        }
        #endregion

        #region P90203 滚动加载
        public string FP90203(DateTime maxDatetime, int pageFrom, int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                A028 a028 = dbma1.A028s.First();
                int needPayDays = Convert.ToInt32(a028.needPayDays);
                decimal overdueRateDaily = Convert.ToDecimal(a028.overdueRateDaily);

                var billList = (from c in dbma1.F001s
                                where c.payerUserSN == userSN
                                    && !c.F002s.Any()
                                    && maxDatetime > c.generateDate
                                orderby c.generateDate descending
                                select new
                                {
                                    c.billSN,
                                    c.businessSN,
                                    c.billType,
                                    c.MoneyAmount,
                                    c.generateDate,
                                    needPayDate = c.generateDate.AddDays(needPayDays),
                                    lateFee = c.generateDate.AddDays(needPayDays) > DateTime.Now ? 0 : c.MoneyAmount * needPayDays * overdueRateDaily
                                }).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(billList);
            }
        }
        #endregion

        #region P90204 获取账户余额
        public string FP90204()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //账户余额
                F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                return f000.balance.ToString();
            }
        }
        #endregion

        #region P90202 付款
        public string FP90202(string billSN,string pwd)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、已经付款）
                var data1 = dbma1.F002s.Where(c => c.billSN == billSN).FirstOrDefault();
                if(data1 != null)
                {
                    return "true";
                }

                //验证交易密码是否正确
                string transPwd = dbma1.U003s.Where(c => c.userSN == userSN).First().transactPwd;
                if (C101.FC10104(pwd, transPwd) == false)
                {
                    return "false";
                }

                //从余额中扣除服务费 F000
                A028 a028 = dbma1.A028s.First();
                int needPayDays = Convert.ToInt32(a028.needPayDays);
                decimal overdueRateDaily = Convert.ToDecimal(a028.overdueRateDaily);

                F001 f001 = dbma1.F001s.Where(c => c.billSN == billSN).First();
                decimal moneyAmount = Convert.ToDecimal(f001.MoneyAmount);
                decimal lateFee = Convert.ToDecimal((DateTime.Now - f001.generateDate).Days > needPayDays ? f001.MoneyAmount * overdueRateDaily * ((DateTime.Now - f001.generateDate).Days - needPayDays) : 0);
                decimal fee = moneyAmount + lateFee;

                F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                if (f000.balance < fee)
                {
                    return "false";
                }
                f000.balance -= fee;

                //加入收支明细表中 F003
                string F003max33SN = C101.FC10102("F003", 8, "UA");
                F003 f003 = new F003();
                f003.revenueExpenditureSN = F003max33SN;
                f003.generetorUserSN = userSN;
                f003.generateDate = DateTime.Now;
                f003.type = "资产出售账单";
                f003.expenditure = fee;
                f003.balance = f000.balance;
                dbma1.F003s.InsertOnSubmit(f003);

                //加入付款表 F001
                string F002max33SN = C101.FC10102("F002", 6, "QA");
                F002 f002 = new F002();
                f002.paySN = F002max33SN;
                f002.billSN = billSN;
                f002.payMoneyAmount = fee;
                f002.payDate = DateTime.Now;
                dbma1.F002s.InsertOnSubmit(f002);

                //加入成长值表 F006
                string F006Max33SN = C101.FC10102("F006", 7, "UD");
                F006 f006 = new F006();
                f006.groupUpSN = F006Max33SN;
                f006.userSN = userSN;
                f006.businessSN = F002max33SN;
                f006.businessType = "账单付款";
                f006.transactionMoneyAmount = fee;
                f006.groupUpValue = fee;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                return "true";
            }
        }
        #endregion 

    }
}
