using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //客服参数
    public class CM0911
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region INIT
        public string FM0911INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.configs.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M091101 保存总设置
        public void FM091101(int mode)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.configs.First();

                data.cusSvrMode = mode;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "configs";
                b000.pkSN = data.config_id.ToString();
                b000.actionTypeSN = "B0W";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M091102 保存服务器端
        public void FM091102(int webDelay, int maxCusSvrConnLevel, int maxUserConnNum, int cusSvrUserMaxAmount)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.configs.First();

                data.webDelay = webDelay;
                data.maxCusSvrConnLevel = maxCusSvrConnLevel;
                data.maxUserConnNum = maxUserConnNum;
                data.cusSvrUserMaxAmount = cusSvrUserMaxAmount;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "configs";
                b000.pkSN = data.config_id.ToString();
                b000.actionTypeSN = "B0X";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M091103 保存客户端倒计时
        public void FM091103(int countSizeLevel, int showCountDownSizeLevel)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.configs.First();

                data.countSizeLevel = countSizeLevel;
                data.showCountDownSizeLevel = showCountDownSizeLevel;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "configs";
                b000.pkSN = data.config_id.ToString();
                b000.actionTypeSN = "B0Y";
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
