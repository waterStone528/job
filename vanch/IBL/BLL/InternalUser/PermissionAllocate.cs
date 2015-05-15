using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Data;

namespace BLL.InternalUser
{
    public class PermissionAllocate
    {
        private static DAL.InternalUser.PermissionAllocate dal = new DAL.InternalUser.PermissionAllocate();
        private HttpApplicationState application = HttpContext.Current.Application;

        //获取用户组的统计信息
        public string GetUserGroupStatisticsInfo()
        {
            return Helper.Serialize(dal.GetUserGroupStatisticsInfo());
        }

        //添加一个用户组
        public string AddUserGroup(string userGroupName)
        {
            return dal.AddUserGroup(userGroupName).ToString();
        }

        //删除一个用户组
        public void DelUserGroup(string userGroupId)
        {
            dal.DelUserGroup(Convert.ToInt32(userGroupId));
        }

        //获取一个用户组的用户以及未分配用户组的用户
        public string GetGroupAndUnAllocateUser(string userGroupId)
        {
            return  Helper.Serialize(dal.GetGroupAndUnAllocateUser(Convert.ToInt32(userGroupId)));
        }

        //向一个用户组中添加一个用户
        public void AddUserToGroup(string userGroupId, string userGroupName, string internalUserId)
        {
            string workNum = dal.AddUserToGroup(Convert.ToInt32(userGroupId), userGroupName, Convert.ToInt32(internalUserId));

            if(workNum != null)
            {
                AddCusSvrToDatatable(workNum);
            }
        }

        //从一个用户组中删除一个用户
        public void DelUserFromGroup(string internalUserId)
        {
            string workNum = dal.DelUserFromGroup(Convert.ToInt32(internalUserId));

            if (workNum != null)
            {
                DelInternalUserFromCusSvrDatatable(workNum);
            }
        }

        //向在线客服datatable中添加一个客服
        public void AddCusSvrToDatatable(string workNum)
        {
            BLL.Config.OnlineCusSvrConfig bll = new BLL.Config.OnlineCusSvrConfig();
            Dictionary<string, int> configData = bll.GetConfigDataG();

            try
            {
                application.Lock();

                DataTable dt = application["cusSvrConditionTable"] as DataTable;
                DataRow dr = dt.NewRow();
                dr["cusSvrNum"] = workNum;
                dr["connLevel"] = configData["maxCusSvrConnLevel"];
                dt.Rows.Add(dr);
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

        //在在线客服datatable中，删除具有在线客服权限的用户
        public void DelInternalUserFromCusSvrDatatable(string workNum)
        {
            try
            {
                application.Lock();
                DataTable dt = application["cusSvrConditionTable"] as DataTable;

                DataRow dr = dt.AsEnumerable().Where(c => c.Field<string>("cusSvrNum") == workNum).First();
                dr.Delete();
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
