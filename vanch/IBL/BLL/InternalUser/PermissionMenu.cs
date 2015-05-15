using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Data;

namespace BLL.InternalUser
{
    public class PermissionMenu
    {
        private static DAL.InternalUser.PermissionMenu dal = new DAL.InternalUser.PermissionMenu();
        private HttpApplicationState application = HttpContext.Current.Application;

        //获得某用户组的权限菜单
        public string GetGroupPermissionMenu(string userGroupId)
        {
            return dal.GetGroupPermissionMenu(Convert.ToInt32(userGroupId));
        }

        //添加一个权限菜单到一个用户组中
        public void AddPermissionMenuToGroup(string menuId, string groupId)
        {
            List<string> workNumList = dal.AddPermissionMenuToGroup(Convert.ToInt32(menuId), Convert.ToInt32(groupId));

            if(workNumList != null)
            {
                //添加具有在线客服权限的用户到在线客服datatable中
                AddInternalUserToCusSvrDatatable(workNumList);
            }
        }

        //从一个用户组中删除一个权限菜单
        public void DelPermissionMenuFromGroup(string menuId, string groupId)
        {
            dal.DelPermissionMenuFromGroup(Convert.ToInt32(menuId), Convert.ToInt32(groupId));
        }

        //添加具有在线客服权限的用户到在线客服datatable中
        public void AddInternalUserToCusSvrDatatable(List<string> workNumList)
        {
            BLL.Config.OnlineCusSvrConfig bll = new BLL.Config.OnlineCusSvrConfig();
            Dictionary<string, int> configData = bll.GetConfigDataG();

            try
            {
                application.Lock();

                foreach (string workNum in workNumList)
                {
                    DataTable dt = application["cusSvrConditionTable"] as DataTable;
                    DataRow dr = dt.NewRow();
                    dr["cusSvrNum"] = workNum;
                    dr["connLevel"] = configData["maxCusSvrConnLevel"];
                    dt.Rows.Add(dr);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }

        //在在线客服datatable中，删除具有在线客服权限的用户
        public void DelInternalUserFromCusSvrDatatable(List<string> workNumList)
        {
            try
            {
                application.Lock();
                DataTable dt = application["cusSvrConditionTable"] as DataTable;

                foreach (string workNum in workNumList)
                {
                    DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == workNum).First();
                    dr.Delete();
                }

                dt.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                application.UnLock();
            }
        }
    }
}
