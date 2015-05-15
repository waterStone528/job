using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSL.Comm
{
    public class C102
    {
        /// <summary>
        /// 获取资产类型 A008
        /// </summary>
        public static string FC20108(DBBS1DataContext dbbs1)
        {
            var linqData = (from c in dbbs1.A008s
                            select new
                            {
                                ID = c.assetsTypeSN,
                                Name = c.value.Trim()
                            }).ToList();

            return C101.FC10107(linqData);
        }
    }
}
