using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //资产出售
    public class CM0910
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region Init
        public string FM0910INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A027s.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M091001 保存参数设置
        public void FM091001(decimal minSellAmount)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A027s.First();

                data.minSellAmount = minSellAmount;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A027";
                b000.pkSN = data.sellingParamsSN.ToString();
                b000.actionTypeSN = "B0Q";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M091002 保存收费设置
        public void FM091002(decimal publishAssetsCost,decimal openServerCost)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A027s.First();

                data.publishAssetsCost = publishAssetsCost;
                data.openServerCost = openServerCost;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A027";
                b000.pkSN = data.sellingParamsSN.ToString();
                b000.actionTypeSN = "B0R";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M091003 保存账单设置
        public void FM091003(decimal serviceRate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A027s.First();

                data.serviceRate = serviceRate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A027";
                b000.pkSN = data.sellingParamsSN.ToString();
                b000.actionTypeSN = "B0S";
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
