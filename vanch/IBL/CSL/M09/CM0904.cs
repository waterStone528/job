using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //短信接口
    public class CM0904
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region 初始化
        public string FM0904INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A034s.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M090401 
        public void FM090401(bool switchStatus)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A034s.First();

                data.switchStatus = switchStatus;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A034";
                b000.pkSN = data.SN;
                b000.actionTypeSN = "B18";
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
