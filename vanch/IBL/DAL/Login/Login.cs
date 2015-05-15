using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Login
{
    public class Login
    {
        //登录
        public int IsLogin(string workNum, string pwd)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var sqlData = from c in vdc.internal_users
                              where c.work_num == workNum && c.pwd == pwd && c.use_status == '5'
                              select c;

                internal_user data = sqlData.FirstOrDefault<internal_user>();

                if (data == null)
                {
                    return 0;
                }
                else
                {
                    return data.internal_user_id;
                }
            }
        }
    }
}
