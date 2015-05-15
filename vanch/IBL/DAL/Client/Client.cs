using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Client
{
    public class Client
    {
        /// <summary>
        /// Get client name by client number
        /// </summary>
        public string GetClientName(string clientNum)
        {
            //模拟数据，需从数据库获得
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                return vdc.U000s.Where(c => c.userSN == clientNum).First().phone;
            }
        }
    }
}
