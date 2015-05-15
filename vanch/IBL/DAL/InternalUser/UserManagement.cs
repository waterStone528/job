using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InternalUser
{
    //用户管理
    public class UserManagement
    {
        public List<Model.InternalUser> GetInternalUserList()
        {
            List<Model.InternalUser> list = new List<Model.InternalUser>();

            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                list = (from c in vdc.internal_users
                        select new Model.InternalUser
                        {
                            InternalUserId = c.internal_user_id,
                            WorkNum = c.work_num,
                            Pwd = c.pwd,
                            Name = c.name,
                            Gender = c.gender,
                            RegDate = c.reg_date,
                            DepartmentName = c.department_name,
                            Jobs = c.jobs,
                            UserGroup = c.user_group,
                            FkUserGroupId = c.fk_user_group_id,
                            AllocateDate = c.allocate_date,
                            Operater = c.operater,
                            UseStatus = c.use_status
                        }).ToList<Model.InternalUser>();
            }

            return list;
        }

        //添加一个内部用户
        public string AddNewInternalUser(internal_user dataObj)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                if (dataObj.operater != null)
                {
                    var operatorNameData = from c in vdc.internal_users
                                           where c.internal_user_id == Convert.ToInt32(dataObj.operater)
                                           select c.name;

                    dataObj.operater = operatorNameData.FirstOrDefault<string>().ToString();
                }

                vdc.internal_users.InsertOnSubmit(dataObj);
                vdc.SubmitChanges();
            }

            string jsonStr = string.Format("{{'internalUserId':'{0}','operator':'{1}'}}", dataObj.internal_user_id.ToString(), dataObj.operater);

            return jsonStr;
        }

        //修改一个内部用户信息
        public void EditInternalUserInfo(internal_user dataObj)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                internal_user sqlData = vdc.internal_users.First<internal_user>(c => c.internal_user_id == dataObj.internal_user_id);

                sqlData.work_num = dataObj.work_num;
                sqlData.name = dataObj.name;
                sqlData.gender = dataObj.gender;
                sqlData.department_name = dataObj.department_name;
                sqlData.jobs = dataObj.jobs;

                vdc.SubmitChanges();
            }
        }

        //删除一个内部用户
        public void DeleteInternalUser(int internalUserId)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                internal_user inteUser = vdc.internal_users.First(c => c.internal_user_id == internalUserId) as internal_user;

                vdc.internal_users.DeleteOnSubmit(inteUser);
                vdc.SubmitChanges();
            }
        }

        //启用或者停用一个内部用户
        //0：待用;1：暂停；5：启用
        public void EnableOrDisableInternalUser(int internalUserId, char status)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                internal_user inteUser = vdc.internal_users.First<internal_user>(c => c.internal_user_id == internalUserId);

                inteUser.use_status = status;

                vdc.SubmitChanges();
            }
        }

        //修改密码
        public void ChangePassword(int internalUserId, string newPassword)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                internal_user inteUser = vdc.internal_users.FirstOrDefault<internal_user>(c => c.internal_user_id == internalUserId);

                inteUser.pwd = newPassword;

                vdc.SubmitChanges();
            }
        }
    }
}
