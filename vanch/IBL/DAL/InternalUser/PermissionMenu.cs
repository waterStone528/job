using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InternalUser
{
    public class PermissionMenu
    {
        //获得某用户组的权限菜单
        public string GetGroupPermissionMenu(int userGroupId)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var linqData = (from c in vdc.menus
                                select c).ToList();

                var dd = (from c in vdc.user_group_menus
                          where c.fk_user_group_id == userGroupId && c.delete_date == null
                          select c.fk_menu_id).ToList<int>();

                foreach (var data in linqData)
                {
                    if (dd.Contains(data.menu_id))
                    {
                        data.parent_menu_code = "true";
                    }
                    else
                    {
                        data.parent_menu_code = "false";
                    }
                }

                var jsonObj = (from c in linqData
                               orderby c.parent_menu_code descending
                               select new
                               {
                                   menuId = c.menu_id,
                                   menuCode = c.menu_code,
                                   menuParentTitle = c.menu_parent_title,
                                   menuTitle = c.menu_title,
                                   moduleCode = c.module_code,
                                   elementVersion = c.element_version,
                                   controlVersion = c.control_version,
                                   isAdd = c.parent_menu_code
                               }).ToList();

                string jsonStr = Helper.Serialize(jsonObj);

                return jsonStr;
            }
        }

        //添加一个权限菜单到一个用户组中
        public List<string> AddPermissionMenuToGroup(int menuId, int groupId)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                user_group_menu data = new user_group_menu();
                data.fk_menu_id = menuId;
                data.fk_user_group_id = groupId;

                vdc.user_group_menus.InsertOnSubmit(data);
                vdc.SubmitChanges();

                //假如添加的是在线客服权限菜单，返回该用户组中的用户编号
                List<string> userWorkNumList = null;
                if(menuId == 4)
                {
                    userWorkNumList = (from c in vdc.internal_users
                                    where c.fk_user_group_id == groupId
                                    select c.work_num).ToList();
                }

                return userWorkNumList;
            }
        }

        //从一个用户组中删除一个权限菜单
        public List<string> DelPermissionMenuFromGroup(int menuId, int groupId)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                user_group_menu data = vdc.user_group_menus.FirstOrDefault<user_group_menu>(c => c.fk_menu_id == menuId && c.fk_user_group_id == groupId && c.delete_date == null);

                data.delete_date = DateTime.Now;

                vdc.SubmitChanges();

                //假如删除的是在线客服权限菜单，返回该用户组中的用户编号
                List<string> userWorkNumList = null;
                if (menuId == 4)
                {
                    userWorkNumList = (from c in vdc.internal_users
                                       where c.fk_user_group_id == groupId
                                       select c.work_num).ToList();
                }

                return userWorkNumList;
            }
        }
    }
}
