using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P3
{
    //发布资产
    public class CP301
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P30101 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public string FP30101(int pageSize)
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //判断是否有权限
                var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                bool ifBillOverdue = C201.FC20153(dbma1, userSN);
                if (data.assetsSellingStatus != true || ifBillOverdue == true)
                {
                    return "{\"status\":\"false\"}";
                }

                string userName = dbma1.U000s.Where(c => c.userSN == userSN).First().name;

                string provinceData = C201.FC20121(dbma1).Replace("[", "").Replace("]", "");
                string assetsType = C201.FC20108(dbma1).Replace("[", "").Replace("]", "");
                string assetsSourceType = C201.FC20109(dbma1).Replace("[", "").Replace("]", "");
                string useStatusType = C201.FC20110(dbma1).Replace("[", "").Replace("]", "");
                string RejectReason = C201.FC20138(dbma1);

                var vp401001List = (from c in dbma1.VP401001s
                                    where c.publisherUserSN == userSN
                                    orderby c.publishDate descending
                                    select c).Take(pageSize).ToList();
                string vp401001ListStr = C101.FC10107(vp401001List);

                return string.Format("{{\"status\":\"true\",\"userName\":\"{0}\",\"SltConfigData\":[{1},{2},{3},{4}],\"assetsDataList\":{5},\"maxDatetime\":\"{6}\",\"RejectReason\":{7},\"userSN\":\"{8}\"}}", userName, provinceData, assetsType, assetsSourceType, useStatusType, vp401001ListStr, DateTime.Now, RejectReason, userSN);
            }
        }
        #endregion

        #region P30102 滚动加载
        public string FP30102(DateTime maxDatetime,int pageFrom,int pageSize)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00003";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var vp401001List = (from c in dbma1.VP401001s
                                    where c.publisherUserSN == userSN
                                     && maxDatetime > c.publishDate
                                    orderby c.publishDate descending
                                    select c).Skip(pageFrom).Take(pageSize).ToList();

                return C101.FC10107(vp401001List);
            }
        }
        #endregion

        #region P30103 发布资产
        //0：成功。1：密码错误。2：余额不足
        public string FP30103(string assetsData,string pwd)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00003";

            P300 assetsDataObj = C101.FC10108(assetsData,typeof(P300)) as P300;

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //密码是否正确
                if(C201.FC20146(dbma1,userSN,pwd) == false)
                {
                    return "{\"resStatus\":\"1\"}";
                }

                //余额是否充足
                decimal publishAssetsFee = Convert.ToDecimal(dbma1.A027s.First().publishAssetsCost);
                //扣款
                if(C201.FC20147(dbma1,userSN, publishAssetsFee,"资产发布",null) == false)
                {
                    return "{\"resStatus\":\"2\"}";
                }

                //加入资产表 P300
                string max33SN = C101.FC10102("P300", 6, "G");
                assetsDataObj.assetsSN = max33SN;
                assetsDataObj.publisherUserSN = userSN;
                assetsDataObj.publishDate = DateTime.Now;
                dbma1.P300s.InsertOnSubmit(assetsDataObj);

                //加入成长值表 F006
                F006 f006 = new F006();
                f006.groupUpSN = C101.FC10102("F006", 7, "UD");
                f006.userSN = userSN;
                f006.businessSN = max33SN;
                f006.businessType = "资产发布";
                f006.transactionMoneyAmount = publishAssetsFee;
                f006.groupUpValue = publishAssetsFee;
                f006.acquireDate = DateTime.Now;
                dbma1.F006s.InsertOnSubmit(f006);

                dbma1.SubmitChanges();

                string assetsDataStr = C101.FC10107(dbma1.VP301001s.Where(c => c.assetsSN == max33SN).First() as VP301001);

                return string.Format("{{\"resStatus\":\"{0}\",\"assetsData\":{1}}}", "0", assetsDataStr);
            }
        }
        #endregion 

        #region P30104 取消发布
        public void FP30104(string assetsSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //有效性检查（1、取消。2、预约）
                var data1 = dbma1.P300s.Where(c => c.assetsSN == assetsSN && c.cancelDate != null).FirstOrDefault();
                var data2 = dbma1.P400s.Where(c => c.assetsSN == assetsSN).FirstOrDefault();
                if(data1 != null || data2 != null)
                {
                    return;
                }

                P300 p300 = dbma1.P300s.Where(c => c.assetsSN == assetsSN).First();
                p300.cancelDate = DateTime.Now;

                dbma1.SubmitChanges();
            }
        }
        #endregion 

        #region P30105 获取发布资产费用、余额、服务费率
        public string FP30105()
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00003";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                A027 a027 = dbma1.A027s.First();
                decimal publishAssetsFee = Convert.ToDecimal(a027.publishAssetsCost);
                decimal serverFeeRate = Convert.ToDecimal(a027.serviceRate);
                decimal balance = dbma1.F000s.Where(c => c.userSN == userSN).First().balance;

                return string.Format("{{\"publishAssetsFee\":\"{0}\",\"serverFeeRate\":\"{1}\",\"balance\":\"{2}\"}}", publishAssetsFee, serverFeeRate, balance);
            }
        }
        #endregion 
    }
}
