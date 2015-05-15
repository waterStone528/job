using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M09
{
    //VIP参数
    public class CM0909
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region INIT 
        public string FM0909INIT()
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                return C101.FC10107(data);
            }
        }
        #endregion

        #region M090902 VIP1
        public void FM090902(int value,decimal rate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                data.vip1_originateValue = value;
                data.vip1_rewardRate = rate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A029";
                b000.pkSN = data.vipParamsSN.ToString();
                b000.actionTypeSN = "B11";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090903 VIP2
        public void FM090903(int value, decimal rate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                data.vip2_originateValue = value;
                data.vip2_rewardRate = rate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A029";
                b000.pkSN = data.vipParamsSN.ToString();
                b000.actionTypeSN = "B12";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090904 VIP3
        public void FM090904(int value, decimal rate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                data.vip3_originateValue = value;
                data.vip3_rewardRate = rate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A029";
                b000.pkSN = data.vipParamsSN.ToString();
                b000.actionTypeSN = "B13";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090905 VIP4
        public void FM090905(int value, decimal rate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                data.vip4_originateValue = value;
                data.vip4_rewardRate = rate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A029";
                b000.pkSN = data.vipParamsSN.ToString();
                b000.actionTypeSN = "B14";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090906 VIP5
        public void FM090906(int value, decimal rate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                data.vip5_originateValue = value;
                data.vip5_rewardRate = rate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A029";
                b000.pkSN = data.vipParamsSN.ToString();
                b000.actionTypeSN = "B15";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090907 VIP6
        public void FM090907(int value, decimal rate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                data.vip6_originateValue = value;
                data.vip6_rewardRate = rate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A029";
                b000.pkSN = data.vipParamsSN.ToString();
                b000.actionTypeSN = "B16";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();
            }
        }
        #endregion

        #region M090908 VIP7
        public void FM090908(int value, decimal rate)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var data = dbma1.A029s.First();

                data.vip7_originateValue = value;
                data.vip7_rewardRate = rate;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "A029";
                b000.pkSN = data.vipParamsSN.ToString();
                b000.actionTypeSN = "B17";
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
