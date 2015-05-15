using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.OpeAccount
{
    public class OpeAccount
    {
        //修改密码
        //true-修改成功；false-原密码错误
        public bool ModifyPwd(int internalUserId, string oldPwd, string newPwd)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var linqData1 = vdc.internal_users.Where(c => c.internal_user_id == internalUserId && c.pwd == oldPwd).FirstOrDefault();

                //原密码错误
                if (linqData1 == null)
                {
                    return false;
                }

                linqData1.pwd = newPwd;

                vdc.SubmitChanges();

                return true;
            }
        }
    }
}
