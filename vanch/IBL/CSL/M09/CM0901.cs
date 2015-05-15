using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //债权融资
    public class CM0901
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region INIT
        public string FM0901INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A024s.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M090101 保存参数设置
        public void FM090101(int minDeadline, int maxDeadline, decimal minFinancingMoneyAmount)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var a024 = dbma1.A024s.First();

                a024.minDeadline = minDeadline;
                a024.maxDeadline = maxDeadline;
                a024.minFinancingMoneyAmount = minFinancingMoneyAmount;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A024";
                b000.pkSN = a024.financingParamsSN.ToString();
                b000.actionTypeSN = "B0K";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090102 保存收费设置
        public void FM090102(decimal openServerCost, decimal financePublishCost, decimal investorRecommendCost)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var a024 = dbma1.A024s.First();

                a024.openServerCost = openServerCost;
                a024.financePublishCost = financePublishCost;
                a024.investorRecommendCost = investorRecommendCost;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A024";
                b000.pkSN = a024.financingParamsSN.ToString();
                b000.actionTypeSN = "B0L";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090103 保存账单设置
        public void FM090103(decimal serviceRateDaily, decimal minServiceRateTotel, decimal maxServiceRateTotel)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var a024 = dbma1.A024s.First();

                a024.serviceRateDaily = serviceRateDaily;
                a024.minServiceRateTotel = minServiceRateTotel;
                a024.maxServiceRateTotel = maxServiceRateTotel;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A024";
                b000.pkSN = a024.financingParamsSN.ToString();
                b000.actionTypeSN = "B0M";
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
