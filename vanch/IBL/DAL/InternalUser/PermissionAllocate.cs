using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InternalUser
{
    //allocate permission
    public class PermissionAllocate
    {
        //获取用户组的统计信息
        public List<Model.UserGroupStatisticsInfo> GetUserGroupStatisticsInfo()
        {
            List<Model.UserGroupStatisticsInfo> resList = new List<Model.UserGroupStatisticsInfo>();

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var userGroupList = from c in vdc.user_groups
                                    where c.delete_date == null
                                    select new
                                    {
                                        userGroupId = c.user_group_id,
                                        userGroupName = c.user_group_name
                                    };

                Dictionary<int?, int> userAmount = (from user in vdc.internal_users
                                                    where user.fk_user_group_id != null
                                                    group user by user.fk_user_group_id into g
                                                    select new
                                                    {
                                                        fk_user_group_id = g.Key,
                                                        amount = g.Count()

                                                    }).ToDictionary(c => c.fk_user_group_id, c => c.amount);

                var firstMenuLinq = from c in vdc.menus
                                    from cc in c.user_group_menus
                                    where c.parent_menu_code == null && cc.delete_date == null
                                    select cc;

                Dictionary<int, int> firstMenuAmount = (from c in firstMenuLinq
                                                        group c by c.fk_user_group_id into g
                                                        select new
                                                        {
                                                            fk_user_group_id = g.Key,
                                                            amount = g.Count()
                                                        }).ToDictionary(c => c.fk_user_group_id, c => c.amount);

                var secondMenuLinq = from c in vdc.menus
                                     from cc in c.user_group_menus
                                     where c.parent_menu_code != null && cc.delete_date == null
                                     select cc;

                Dictionary<int, int> secondMenuAmount = (from c in secondMenuLinq
                                                         group c by c.fk_user_group_id into g
                                                         select new
                                                         {
                                                             fk_user_group_id = g.Key,
                                                             amount = g.Count()
                                                         }).ToDictionary(c => c.fk_user_group_id, c => c.amount);

                foreach (var userGroup in userGroupList)
                {
                    Model.UserGroupStatisticsInfo data = new Model.UserGroupStatisticsInfo();

                    #region 老的写法，速度大概在4秒左右
                    //data.UserGroupId = userGroupId;

                    //data.UserGroupName = vdc.user_groups.FirstOrDefault<user_group>(c => c.user_group_id == userGroupId).user_group_name;

                    //var linqUser = from user in vdc.internal_users
                    //               where user.fk_user_group_id == userGroupId
                    //               select user;
                    //data.UserAmount = linqUser.Count();

                    //var linqFirstMenu = from firstMenu in vdc.menus
                    //                    where (from menuId in vdc.user_group_menus where menuId.fk_user_group_id == userGroupId select menuId.fk_menu_id).Contains(firstMenu.menu_id)
                    //                        && firstMenu.parent_menu_code == null
                    //                    select firstMenu;
                    //data.FirstMenuAmount = linqFirstMenu.Count();

                    //var linqSecondMenu = from secondMenu in vdc.menus
                    //                     where (from menuId in vdc.user_group_menus where menuId.fk_user_group_id == userGroupId select menuId.fk_menu_id).Contains(secondMenu.menu_id)
                    //                        && secondMenu.parent_menu_code != null
                    //                     select secondMenu;
                    //data.SecondMenuAmount = linqSecondMenu.Count();

                    #endregion

                    data.UserGroupId = userGroup.userGroupId;
                    data.UserGroupName = userGroup.userGroupName;
                    if (userAmount.ContainsKey(data.UserGroupId))
                    {
                        data.UserAmount = userAmount[data.UserGroupId];
                    }
                    if (firstMenuAmount.ContainsKey(data.UserGroupId))
                    {
                        data.FirstMenuAmount = firstMenuAmount[data.UserGroupId];
                    }
                    if (secondMenuAmount.ContainsKey(data.UserGroupId))
                    {
                        data.SecondMenuAmount = secondMenuAmount[data.UserGroupId];
                    }

                    resList.Add(data);
                }
            }

            stopwatch.Stop();

            TimeSpan s = stopwatch.Elapsed;

            return resList;
        }

        //添加一个用户组
        public int AddUserGroup(string userGroupName)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                user_group userGroupObj = new user_group();
                userGroupObj.user_group_name = userGroupName;

                vdc.user_groups.InsertOnSubmit(userGroupObj);
                vdc.SubmitChanges();

                return userGroupObj.user_group_id;
            }
        }

        //删除一个用户组
        public void DelUserGroup(int userGroupId)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                user_group userGrp = vdc.user_groups.First<user_group>(c => c.user_group_id == userGroupId);
                userGrp.delete_date = DateTime.Now;
                //vdc.user_groups.DeleteOnSubmit(userGrp);

                var linqUserGrpMenu = from c in vdc.user_group_menus
                                      where c.fk_user_group_id == userGroupId
                                      select c;

                foreach (user_group_menu userGrpMenu in linqUserGrpMenu)
                {
                    userGrpMenu.delete_date = DateTime.Now;
                }

                vdc.SubmitChanges();
            }
        }

        //获取一个用户组的用户以及未分配用户组的用户
        public List<Model.InternalUser> GetGroupAndUnAllocateUser(int userGroupId)
        {
            List<Model.InternalUser> resList = new List<Model.InternalUser>();

            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                resList = (from c in vdc.internal_users
                           where c.fk_user_group_id == userGroupId
                             || (c.fk_user_group_id == null && c.use_status == '5')
                           orderby c.fk_user_group_id descending
                           select new Model.InternalUser
                           {
                               InternalUserId = c.internal_user_id,
                               WorkNum = c.work_num,
                               //Pwd = c.pwd,
                               Name = c.name,
                               Gender = c.gender,
                               //RegDate = c.reg_date,
                               DepartmentName = c.department_name,
                               Jobs = c.jobs,
                               UserGroup = c.user_group,
                               //FkUserGroupId = c.fk_user_group_id,
                               //AllocateDate = c.allocate_date,
                               //Operater = c.operater,
                               //UseStatus = c.use_status
                           }).ToList<Model.InternalUser>();

            }

            return resList;
        }

        //在一个用户组中添加一个用户
        public string AddUserToGroup(int userGroupId, string userGroupName, int internalUserId)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                internal_user inteUser = vdc.internal_users.FirstOrDefault<internal_user>(c => c.internal_user_id == internalUserId);

                inteUser.fk_user_group_id = userGroupId;
                inteUser.user_group = userGroupName;

                vdc.SubmitChanges();

                return GetWorkNum(vdc, userGroupId, internalUserId);
            }
        }

        //从一个用户组中删除一个用户
        public string DelUserFromGroup(int internalUserId)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                internal_user inteUser = vdc.internal_users.FirstOrDefault<internal_user>(c => c.internal_user_id == internalUserId);

                int userGroupId = Convert.ToInt32(inteUser.fk_user_group_id);
                inteUser.fk_user_group_id = null;
                inteUser.user_group = null;

                vdc.SubmitChanges();

                return GetWorkNum(vdc, userGroupId, internalUserId);
            }
        }

        //如果该用户所在的用户组具有在线客服的权限菜单，则返回用户的工号
        public string GetWorkNum(VanchBgDataContext vdc,int userGroupId, int internalUserId)
        {
            int cusSvrMenuId = 4;

            var data = from c in vdc.user_groups
                       from o in c.user_group_menus
                       where c.user_group_id == userGroupId
                        && o.fk_menu_id == cusSvrMenuId
                        && o.delete_date == null
                       select c;

            if(data.Count() != 0)
            {
                var linqData = vdc.internal_users.Where(c => c.internal_user_id == internalUserId).First();

                return linqData.work_num;
            }

            return null;
        }
    }
}
