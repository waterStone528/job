using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //顾问参数
    public class CM0903
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region INIT
        public string FM0903INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string data = dbma1.A025s.First().openServerCost == null ? "" : dbma1.A025s.First().openServerCost.ToString(); ;

                return data;
            }
        }
        #endregion

        #region M090301 保存收费设置
        public void FM090301(decimal openServerCost)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var a025 = dbma1.A025s.First();

                a025.openServerCost = openServerCost;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A025";
                b000.pkSN = a025.consultantParamsSN.ToString();
                b000.actionTypeSN = "B0N";
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
