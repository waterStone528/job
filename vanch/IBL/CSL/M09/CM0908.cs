using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //财务设置
    public class CM0908
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region Init
        public string FM0908INIT()
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A028s.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M090801 保存应收账款
        public void FM090801(int needPayDays, decimal overdueRateDaily)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A028s.First();

                data.needPayDays = needPayDays;
                data.overdueRateDaily = overdueRateDaily;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A028";
                b000.pkSN = data.moneyParamsSN.ToString();
                b000.actionTypeSN = "B0T";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090802 保存附加收费
        public void FM090802(decimal shortMessageCost)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A028s.First();

                data.shortMessageCost = shortMessageCost;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A028";
                b000.pkSN = data.moneyParamsSN.ToString();
                b000.actionTypeSN = "B0U";
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
