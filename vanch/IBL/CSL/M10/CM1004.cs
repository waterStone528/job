using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSL.Comm;
using System.Web;

namespace CSL.M10
{
    //岗位分配
    public class CM1004
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region INIT
        public string FM1004INIT()
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var dataList = (from c in dbma1.B004s
                                where c.deleteDate == null
                                select new
                                {
                                    c.roleTypeSN,
                                    roleName = c.value,
                                    userAmount = dbma1.B003s.Where(o => o.roleTypeSN == c.roleTypeSN && o.deleteDate == null).Count(),
                                    c.note
                                }).ToList();       

                return C101.FC10107(dataList);
            }
        }
        #endregion

        #region M100401 查看内部用户 
        //0:成功； 1：岗位已经删除
        public string FM100401(string roleSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //岗位已经删除
                var dataVarify = dbma1.B004s.Where(c => c.roleTypeSN == roleSN && c.deleteDate == null).FirstOrDefault();
                if(dataVarify == null)
                {
                    return "{\"status\":\"1\"}";
                }

                //属于该岗位的用户
                var data1 = from c in dbma1.B003s.Where(c => c.roleTypeSN == roleSN && c.deleteDate == null)
                            from o in dbma1.internal_users.Where(o => o.internal_user_id == c.internalUserSN)
                            select new
                            {
                                o.work_num,
                                o.name,
                                o.gender,
                                o.department_name,
                                o.jobs,
                                o.internal_user_id,
                                o.fk_user_group_id
                            };
                var dataUse = (from c in data1
                              join p in dbma1.user_groups on c.fk_user_group_id equals p.user_group_id into g
                              from p in g.DefaultIfEmpty()
                              select new
                              {
                                  c.work_num,
                                  c.name,
                                  c.gender,
                                  c.department_name,
                                  c.jobs,
                                  p.user_group_name,
                                  c.internal_user_id
                              }).ToList();

                //未分配岗位的用户
                var dataNotUse = (from o in dbma1.internal_users.Where(o => !dbma1.B003s.Where(c => c.deleteDate == null && c.internalUserSN == o.internal_user_id).Any())
                                  join p in dbma1.user_groups on o.fk_user_group_id equals p.user_group_id into g
                                  from p in g.DefaultIfEmpty()
                                  select new
                                  {
                                      o.work_num,
                                      o.name,
                                      o.gender,
                                      o.department_name,
                                      o.jobs,
                                      p.user_group_name,
                                      o.internal_user_id
                                  }).ToList();

                return string.Format("{{\"status\":\"0\",\"dataUse\":{0},\"dataNotUse\":{1}}}", C101.FC10107(dataUse), C101.FC10107(dataNotUse));
            }
        }
        #endregion

        #region M100402 删除用户
        //0：成功； 1：客户经理有服务的客户，无法删除；
        public string FM100402(string roleSN,int internalUserSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                if(roleSN.Trim() == "B01")
                {
                    var dataTemp = dbma1.B005s.Where(c => c.internalUserSN == internalUserSN).FirstOrDefault();

                    if(dataTemp != null)
                    {
                        return "1";
                    }
                }

                var data = dbma1.B003s.Where(c => c.roleTypeSN == roleSN && c.internalUserSN == internalUserSN && c.deleteDate == null).FirstOrDefault();

                if(data == null)
                {
                    return "0";
                }

                data.deleteDate = DateTime.Now;

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "B003";
                b000.pkSN = data.roleAssignmentSN;
                b000.actionTypeSN = "B1A";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();

                return "0";
            }
        }
        #endregion 

        #region M100403 添加用户
        //0：成功； 1:岗位已经删除； 2：用户已经添加到此岗位； 3：用户已经添加到其他岗位； 
        public string FM100403(string roleSN,int internalUserSN)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //岗位已经删除
                var dataVarify1 = dbma1.B004s.Where(c => c.roleTypeSN == roleSN && c.deleteDate == null).FirstOrDefault();
                if (dataVarify1 == null)
                {
                    return "1";
                }

                //用户已经添加到此岗位
                var dataVarify2 = dbma1.B003s.Where(c => c.roleTypeSN == roleSN && c.internalUserSN == internalUserSN && c.deleteDate == null).FirstOrDefault();
                if (dataVarify2 != null)
                {
                    return "2";
                }

                //用户已经添加到其他岗位
                var dataVarify3 = dbma1.B003s.Where(c => c.roleTypeSN != roleSN && c.internalUserSN == internalUserSN && c.deleteDate == null).FirstOrDefault();
                if (dataVarify3 != null)
                {
                    return "3";
                }

                B003 b003 = new B003();
                b003.roleAssignmentSN = C101.FC10102("B003", 3, "B");
                b003.internalUserSN = internalUserSN;
                b003.roleTypeSN = roleSN.Trim();;
                b003.assignDate = DateTime.Now;

                dbma1.B003s.InsertOnSubmit(b003);

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "B003";
                b000.pkSN = b003.roleAssignmentSN;
                b000.actionTypeSN = "B1B";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();

                return "0";
            }
        }
        #endregion

        #region M100404 添加新岗位
        //"0":成功； “1“：相同名称的岗位已经存在
        public string FM100404(string roleName,string note)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                B004 b004 = new B004();

                var data = dbma1.B004s.Where(c => c.value == roleName && c.deleteDate == null).FirstOrDefault();
                if(data != null)
                {
                    return "{\"status\":\"1\"}";
                }

                b004.roleTypeSN = C101.FC10102("B004", 2, "B");
                b004.value = roleName;
                b004.note = note;
                b004.assignDate = DateTime.Now;

                dbma1.B004s.InsertOnSubmit(b004);

                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "B004";
                b000.pkSN = b004.roleTypeSN;
                b000.actionTypeSN = "B1C";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();

                return string.Format("{{\"status\":\"0\",\"SN\":\"{0}\"}}", b004.roleTypeSN);
            }
        }
        #endregion

        #region M100405 删除岗位
        //"0":成功； “1“：岗位已经删除； ”2“：该岗位有用户
        public string FM100405(string roleSN)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //岗位已经删除
                var data = dbma1.B004s.Where(c => c.roleTypeSN == roleSN && c.deleteDate == null).FirstOrDefault();
                if (data == null)
                {
                    return "1";
                }

                //该岗位有用户
                var data1 = (from c in dbma1.B004s
                             from o in c.B003s
                             where c.roleTypeSN == roleSN
                                 && c.deleteDate == null
                                 && o.deleteDate == null
                             select c).FirstOrDefault();
                if(data1 != null)
                {
                    return "2";
                }

                var data2 = (from c in dbma1.B004s
                            where c.roleTypeSN == roleSN
                            select c).First();
                data2.deleteDate = DateTime.Now;


                //添加后台修改记录 B00
                B000 b000 = new B000();
                b000.bgOperateSN = C101.FC10102("B000", 5, "B");
                b000.tableName = "B004";
                b000.pkSN = data2.roleTypeSN;
                b000.actionTypeSN = "B1D";
                int operatorSN = Convert.ToInt32(session["internalUserId"].ToString());
                //int operatorSN = 1032;
                b000.operatorSN = operatorSN;
                b000.operateDate = DateTime.Now;
                dbma1.B000s.InsertOnSubmit(b000);

                dbma1.SubmitChanges();

                return "0";
            }
        }
        #endregion
    }
}
