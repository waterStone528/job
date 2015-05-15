using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //债权投资
    public class CM0906
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region 初始化
        public string FM0906INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A023s.First();

                return C101.FC10107(data);

            }
        }
        #endregion

        #region M090601 保存参数设置
        public void FM090601(decimal minInvestMoneyAmount,decimal minDailyRate,decimal maxDailyRate)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var a023 = dbma1.A023s.First();

                a023.minInvestMoneyAmount = minInvestMoneyAmount;
                a023.minDailyRate = minDailyRate;
                a023.maxDailyRate = maxDailyRate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A023";
                b000.pkSN = a023.investParamsSN.ToString(); ;
                b000.actionTypeSN = "B0H";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090602 保存收费设置
        public void FM090602(decimal openServerCost,decimal consultantReserveCost,decimal financingReserveCost)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var a023 = dbma1.A023s.First();

                a023.openServerCost = openServerCost;
                a023.consultantReserveCost = consultantReserveCost;
                a023.financingReserveCost = financingReserveCost;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A023";
                b000.pkSN = a023.investParamsSN.ToString();
                b000.actionTypeSN = "B0J";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion
    }
}
