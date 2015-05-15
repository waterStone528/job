using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //用户组统计信息
    public class UserGroupStatisticsInfo
    {
        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public int UserAmount { get; set; }
        public int FirstMenuAmount { get; set; }
        public int SecondMenuAmount { get; set; }
    }
}
