using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    //一些基本的判断
    public class CommCtl
    {
        //获得用户的一级权限菜单
        public string GetFirstLevelPermissionMenu(int internalUserId)
        {
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var linqData1 = from c in vdc.internal_users
                                where c.internal_user_id == internalUserId
                                select c.fk_user_group_id;

                var linqData2 = linqData1.FirstOrDefault();

                if (linqData2 == null)
                {
                    return "";
                }

                int userGroupId = Convert.ToInt32(linqData2);

                var linqData3 = (from c in vdc.menus
                                 from cc in c.user_group_menus
                                 where c.parent_menu_code == null
                                     && cc.fk_user_group_id == userGroupId
                                     && cc.delete_date == null
                                 orderby c.sequence
                                 select new
                                 {
                                      Name = c.menu_title,
                                      ID = c.menu_code
                                 }).ToList();

                return Helper.Serialize(linqData3);
            }
        }

        //获得用户的二级权限菜单
        public string IsPermited(int internalUserId,string parentMenuCode)
        {
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var linqData1 = from c in vdc.internal_users
                                where c.internal_user_id == internalUserId
                                select c.fk_user_group_id;

                var linqData2 = linqData1.FirstOrDefault();

                if(linqData2 == null)
                {
                    return "[]";
                }

                int userGroupId = Convert.ToInt32(linqData2);

                var linqData3 = (from c in vdc.menus
                                 from cc in c.user_group_menus
                                 where c.parent_menu_code == parentMenuCode
                                     && cc.fk_user_group_id == userGroupId
                                     && cc.delete_date == null
                                 orderby c.sequence
                                 select new
                                 {
                                     ID = c.menu_code,
                                     Name = c.menu_title
                                 }).ToList();

                return Helper.Serialize(linqData3);
            }
        }
    }
}
