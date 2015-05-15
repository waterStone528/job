using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //邮件接口
    public class CM0905
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region 初始化
        public string FM0905INIT()
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A035s.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M090501
        public void FM090501(string smtp,string userName,string pwd,int port,bool switchStatus)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A035s.First();

                data.SMTP = smtp;
                data.userName = userName;
                data.pwd = pwd;
                data.port = port;
                data.switchStatus = switchStatus;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A035";
                b000.pkSN = data.SN;
                b000.actionTypeSN = "B19";
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
