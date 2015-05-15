using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //资产购买
    public class CM0907
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region Init 
        public string FM0907INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A026s.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M090701 保存收费设置
        public void FM090701(decimal openServerCost, decimal assetsReserveCost)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A026s.First();

                data.openServerCost = openServerCost;
                data.assetsReserveCost = assetsReserveCost;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A026";
                b000.pkSN = data.purchaseParamsSN.ToString();
                b000.actionTypeSN = "B0P";
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
