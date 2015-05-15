using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M06
{
    //手工调账
    public class CM0603
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region init 初始化
        public string FM0603INIT(int pageSize)
        {
            using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VF007s.Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M060301 充值
        //“0”：成功； “1”：交易密码错误; "2":姓名错误
        public string FM060301(string phone, string name, decimal changeAmount, string changeReasonType, string pwd, string note)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //查看密码是否正确
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                var data = dbma1.internal_users.Where(c => c.internal_user_id == operatorSN && c.pwd == pwd).FirstOrDefault();
                if (data == null)
                {
                    return "{\"sucStatus\":\"1\"}";
                }

                var u000 = dbma1.U000s.Where(c => c.phone ==phone).First();
                string userSN = u000.userSN;

                //姓名是否正确
                if(u000.name.Trim() != name)
                {
                    return "{\"sucStatus\":\"2\"}";
                }

                //更新余额表 F000
                F000 f000 = dbma1.F000s.Where(c => c.userSN == userSN).First();
                f000.balance += changeAmount;

                //添加调账表 F007
                F007 f007 = new F007();
                f007.changeAccountSN = C101.FC10102("F007", 7, "UF");
                f007.userSN = userSN;
                f007.phone = phone;
                f007.name = name;
                f007.changeAmount = changeAmount;
                f007.changeReasonType = changeReasonType;
                f007.note = note;
                f007.changeDate = DateTime.Now;
                dbma1.F007s.InsertOnSubmit(f007);

                //添加收支明细表 F003
                F003 f003 = new F003();
                f003.revenueExpenditureSN = C101.FC10102("F003", 8, "UA");
                f003.generetorUserSN = userSN;
                f003.generateDate = DateTime.Now;
                f003.type = changeReasonType;
                f003.revenue = changeAmount;
                f003.expenditure = 0;
                f003.balance = f000.balance;
                f003.referSN = f007.changeAccountSN;
                dbma1.F003s.InsertOnSubmit(f003);

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "B005";
                b000.pkSN = f007.changeAccountSN;
                b000.actionTypeSN = "B0G";
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();


                dbma1.SubmitChanges();

                return string.Format("{{\"sucStatus\":\"0\",\"SN\":\"{0}\",\"date\":{1}}}", f007.changeAccountSN, C101.FC10107(DateTime.Now));
            }
        }
        #endregion 

        #region M060302 加载更多
        public string FM060302(int pageFrom, int pageSize)
        {
            using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
            {
                var dataList = dbbs1.VF007s.Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(dataList);
            }
        }
        #endregion
    }
}
