using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Data;

namespace BLL.InternalUser
{
    public class UserManagement
    {
        private static DAL.InternalUser.UserManagement dal = new DAL.InternalUser.UserManagement();
        private static DAL.CommCtl dalCommJudge = new DAL.CommCtl();

        //获得内部用户信息列表
        public string GetInternalUserList()
        {
            List<Model.InternalUser> list = dal.GetInternalUserList();

            string jsonStr = Helper.Serialize(list);

            return jsonStr;
        }

        //添加一个内部用户
        public string AddNewInternalUser(string jsonStr)
        {
            DAL.internal_user data = Helper.Deserialize(jsonStr, typeof(DAL.internal_user)) as DAL.internal_user;

            if (HttpContext.Current.Session["internalUserId"] != null)
            {
                data.operater = HttpContext.Current.Session["internalUserId"].ToString();
            }

            return dal.AddNewInternalUser(data);
        }

        //修改一个内部用户信息
        public void EditInternalUserInfo(string jsonStr)
        {
            DAL.internal_user data = Helper.Deserialize(jsonStr, typeof(DAL.internal_user)) as DAL.internal_user;

            dal.EditInternalUserInfo(data);
        }

        //删除一个内部用户
        public void DeleteInternalUser(string internalUserId)
        {
            dal.DeleteInternalUser(Convert.ToInt32(internalUserId));
        }

        //启用或者停用一个内部用户
        public void EnableOrDisableInternalUser(string internalUserId, string status)
        {
            dal.EnableOrDisableInternalUser(Convert.ToInt32(internalUserId), Convert.ToChar(status));
        }

        //修改密码
        public void ChangePassword(string internalUserId, string newPassword)
        {
            dal.ChangePassword(Convert.ToInt32(internalUserId), newPassword);
        }
    }
}
