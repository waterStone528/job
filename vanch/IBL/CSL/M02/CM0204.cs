using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M02
{
    //财务顾问
     public class CM0204
    {
         private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

         #region Init
         public string FM0204INIT(int pageSize)
         {
             using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
             {
                 //数据
                 var dataList = dbbc1.B400s.Take(pageSize).ToList();

                 using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
                 {
                     //服务状态
                     string serverStatusListStr = GetServerStatusList(dataList);

                     //申请状态
                     string applyStatusListStr = GetApplyStatusList(dbbs1, dataList);

                     return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1},\"applyStatusList\":{2}}}", C101.FC10107(dataList), serverStatusListStr, applyStatusListStr);
                 }
             }
         }
         #endregion

         #region M020410 排序
         public string FM020410(string sortStr, int pageSize)
         {
             using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
             {
                 List<B400> dataList = Sort(dbbc1, sortStr).Take(pageSize).ToList();

                 using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
                 {
                     //服务状态
                     string serverStatusListStr = GetServerStatusList(dataList);

                     //申请状态
                     string applyStatusListStr = GetApplyStatusList(dbbs1, dataList);

                     return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1},\"applyStatusList\":{2}}}", C101.FC10107(dataList), serverStatusListStr, applyStatusListStr);
                 }
             }
         }
         #endregion 

         #region M020411 加载更多
         public string FM020411(int pageFrom, int pageSize, string sortStr)
         {
             using (DBBC1DataContext dbbc1 = new DBBC1DataContext())
             {
                 var dataList = Sort(dbbc1, sortStr).Skip(pageFrom).Take(pageSize).ToList();

                 using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
                 {
                     //服务状态
                     string serverStatusListStr = GetServerStatusList(dataList);

                     //申请状态
                     string applyStatusListStr = GetApplyStatusList(dbbs1, dataList);

                     return string.Format("{{\"dataList\":{0},\"serverStatusList\":{1},\"applyStatusList\":{2}}}", C101.FC10107(dataList), serverStatusListStr, applyStatusListStr);
                 }
             }
         }
         #endregion

         #region M020401 查看用户信息
         public string FM020401(string userSN)
         {
             using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
             {
                 //基本信息
                 var consultantBasicData = (from c in dbbs1.VP801001s
                                            where c.userSN == userSN
                                            select new
                                            {
                                                c.userSN,
                                                c.name,
                                                c.birthday,
                                                c.gender,
                                                c.registeredResidence,
                                                idCard = c.idCard.Substring(0, 6),
                                                c.phone,
                                                c.email,
                                                c.maritalStatusType,
                                                c.procreateStatus,
                                                c.currentAddressProvince,
                                                c.currentAddressCity,
                                                c.currentAddressDetails,
                                                c.graduateSchool,
                                                c.degreeType,
                                                c.degreeCard,
                                                c.workEnterprise,
                                                c.industryType,
                                                c.enterpriseType,
                                                c.hiredate,
                                                c.workTel,
                                                c.post,
                                                c.enterpriseSwitchboard,
                                                c.enterpriseWebsite,
                                                c.colleageName,
                                                c.colleagePhone,
                                                colleageIdCard = c.colleageIdCard.Substring(0,6),
                                                c.serviceProvince,
                                                c.serviceCity,
                                                c.investigate,
                                                c.assetsEvaluate,
                                                c.badLoanCollect,
                                                c.creditRightGuarantee,
                                                c.consultantDetails
                                            }).First();
                 return C101.FC10107(consultantBasicData);
             }
         }
         #endregion

         #region M020402 拒绝预约信息
         public string FM020402(string userSN)
         {
             using(DBBS1DataContext dbbs1 = new DBBS1DataContext())
             {
                 var dataList = dbbs1.VP502011s.Where(c => c.consultantUserSN == userSN).ToList();

                 return C101.FC10107(dataList);
             }
         }
         #endregion

         #region M020403 审核中债权
         public string FM020403(string userSN)
         {
             using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
             {
                 var dataList = dbbs1.VP503011s.Where(c => c.consultantUserSN == userSN).ToList();

                 return C101.FC10107(dataList);
             }
         }
         #endregion

         #region M020404 已审核债权
         public string FM020404(string userSN)
         {
             using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
             {
                 var dataList = dbbs1.VP504041s.Where(c => c.consultantUserSN == userSN && c.closeCaseDate == null).ToList();

                 return C101.FC10107(dataList);
             }
         }
         #endregion

         #region M020405 已结案债权
         public string FM020405(string userSN)
         {
             using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
             {
                 var dataList = dbbs1.VP504051s.Where(c => c.consultantUserSN == userSN).ToList();

                 return C101.FC10107(dataList);
             }
         }
         #endregion

         #region M020406 获取备注信息
         public string FM020406(string userSN)
         {
             using (DBBS1DataContext dbbs1 = new DBBS1DataContext())
             {
                 return dbbs1.VP801021s.Where(c => c.userSN == userSN).First().consultantNote;
             }
         }
         #endregion

         #region M020407 保存备注信息
         public void FM020407(string userSN, string note)
         {
             using (DBMA1DataContext dbma1 = new DBMA1DataContext())
             {
                 var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                 data.consultantNote = note;

                 dbma1.SubmitChanges();
             }
         }
         #endregion

         #region M020408 查看顾问服务申请时顾问基本信息
         public string FM020408(string userSN)
         {
             using(DBMA1DataContext dbma1 = new DBMA1DataContext())
             {
                 var data = (from c in dbma1.VP801041s
                            where c.userSN == userSN
                            orderby c.applyDate descending
                            select c).First();


                 return C101.FC10107(data);
             }
         }
         #endregion

         #region M020409 通过或者取消申请
         public string FM020409(string userSN,string act,string auditNote)
         {
             using (DBMA1DataContext dbma1 = new DBMA1DataContext())
             {
                 int? operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                 //int? operatorSN = 1032;

                 //更新U006
                 U006 u006 = (from c in dbma1.U006s
                              where c.userSN == userSN
                              orderby c.applyDate descending
                              select c).First();
                 u006.auditStatus = Convert.ToInt16(act == "1" ? "2" : "3");
                 u006.auditDate = DateTime.Now;
                 u006.auditerUserSN = operatorSN;
                 u006.auditNote = auditNote;

                 //更新U001
                 U001 u001 = dbma1.U001s.Where(c => c.userSN == userSN).First();
                 u001.consultantApplyStatus = Convert.ToInt16(act == "1" ? "2" : "3");

                 //添加B000
                 B000 b000 = new B000();
                 b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                 b000.tableName = "B005";
                 b000.pkSN = u006.recordSN;
                 b000.actionTypeSN = act == "1" ? "B0E" : "B0F";
                 b000.operatorSN = Convert.ToInt32(operatorSN);
                 b000.operateDate = DateTime.Now;
                 dbma1.B000s.InsertOnSubmit(b000);

                 string name = dbma1.internal_users.Where(c => c.internal_user_id == Convert.ToInt32(operatorSN)).First().name;

                 dbma1.SubmitChanges();

                 return name;
             }
         }
         #endregion

         #region M020412 修改服务状态
         public void FM020412(string userSN, string status)
         {
             using (DBMA1DataContext dbma1 = new DBMA1DataContext())
             {
                 var data = dbma1.U001s.Where(c => c.userSN == userSN).First();
                 data.consultantStatus = status == "1" ? true : false;

                 B000 b000 = new B000();
                 b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                 b000.tableName = "U001";
                 b000.pkSN = data.userSN;
                 b000.actionTypeSN = status == "1" ? "B0B" : "B0C";
                 int internalUserSN = Convert.ToInt32(session["internalUserId"].ToString());
                 //int internalUserSN = 1032;
                 b000.operatorSN = internalUserSN;
                 b000.operateDate = DateTime.Now;
                 dbma1.B000s.InsertOnSubmit(b000);

                 dbma1.SubmitChanges();
             }
         }
         #endregion


         //排序
         private IEnumerable<B400> Sort(DBBC1DataContext dbbc1, string sortStr)
         {
             List<string> sortList = new List<string>();
             sortList.Add("refuseReserveAmount");
             sortList.Add("auditingCrAmount");
             sortList.Add("auditedCrAmount");
             sortList.Add("closedCrAmount");

             string sqlWhereStr = "";
             string sqlOrderByStr = "";
             string[] sortStrSplit = sortStr.Split(',');
             for (int i = 1; i < (sortStrSplit.Length - 1); i++)
             {
                 string[] sortStrSplitSplit = sortStrSplit[i].Split('#');
                 if (sortStrSplitSplit[0] == "1")
                 {
                     sqlWhereStr = string.Format("where userSN like '%{0}%'", sortStrSplitSplit[1]);

                     continue;
                 }

                 if (sqlOrderByStr == "")
                 {
                     sqlOrderByStr += "order by";
                 }
                 else
                 {
                     sqlOrderByStr += ",";
                 }

                 int orderByFieldNum = Convert.ToInt32(sortStrSplitSplit[0]) - 2;
                 string orderByFieldName = sortList[orderByFieldNum];
                 string order = sortStrSplitSplit[1] == "A" ? "" : " desc";
                 sqlOrderByStr += string.Format(" {0} {1}", orderByFieldName, order);
             }

             string sqlStr = string.Format("select * from B400 {0} {1}", sqlWhereStr, sqlOrderByStr);

             IEnumerable<B400> crDataList = dbbc1.ExecuteQuery<B400>(sqlStr);

             return crDataList;
         }

        //获取服务状态列表
        private string GetServerStatusList(IEnumerable<B400> dataList)
        {
            //服务状态
            List<bool?> serverStatusList = new List<bool?>();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                foreach (var temp in dataList)
                {
                    U001 u001 = dbma1.U001s.Where(c => c.userSN == temp.userSN).First();
                    serverStatusList.Add(u001.consultantStatus);
                }
            }

            return C101.FC10107(serverStatusList);
        }

        //获取申请状态列表
        private string GetApplyStatusList(DBBS1DataContext dbbs1, IEnumerable<B400> dataList)
        {
            //服务状态
            List<short> applyStatusList = new List<short>();
            foreach (var b300 in dataList)
            {
                VP801021 vp801021 = dbbs1.VP801021s.Where(c => c.userSN == b300.userSN).First();
                applyStatusList.Add(vp801021.consultantApplyStatus);
            }

            return C101.FC10107(applyStatusList);
        }
    }
}
