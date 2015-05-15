using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.OnlineCusSvr
{
    public class OnlineCusSvr
    {
        public List<string> GetCusSvrWorkNum()
        {
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                int cusSvrMenuId = vdc.menus.Where(c => c.menu_code == "M01").First().menu_id;

                List<string> cusSvrWorkNumList = (from c in vdc.user_groups
                                                  from o in c.internal_users
                                                  from p in c.user_group_menus
                                                  where p.fk_menu_id == cusSvrMenuId
                                                     && p.delete_date == null
                                                  select o.work_num).ToList();

                return cusSvrWorkNumList;
            }
        }

        //查找用户的客户经理工号
        public string GetClientManagerWorkNum(string userSN)
        {
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var data = (from c in vdc.B005s
                            from o in vdc.internal_users
                            where c.userSN == userSN
                             && c.internalUserSN == o.internal_user_id
                            select o).FirstOrDefault();
 
                if(data == null)
                {
                    return null;
                }

                return data.work_num;
            }
        }

    }
}
