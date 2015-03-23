using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.Business.P03
{
    /// <summary>
    ///     食材相关信息
    /// </summary>
    public class Food
    {
        private EF.KbEntities db = new EF.KbEntities();

        /// <summary>
        ///     获取所有食材的文字信息
        /// </summary>
        public List<JsonClass.P03.FoodInfo> GetAllFoodText(int userId)
        {
            //获得体质类型
            EF.P0101 p0101 = db.P0101.Where(c => c.id == userId).First();
            List<string> constitutionTypeList = null;
            if (p0101.constitutionType != null)
            {
                constitutionTypeList = p0101.constitutionType.Split('%').ToList();
            }
            else if (p0101.possibleConstitutionType != null)
            {
                constitutionTypeList = p0101.possibleConstitutionType.Split('%').ToList();
            }
            else
            {
                return null;
            }

            //获得并筛选相应体质的食材
            var linqData1 = from c in db.V0301
                            where constitutionTypeList.Contains(c.constitutionType)
                            select new JsonClass.P03.FoodInfo
                            {
                                a = c.id,
                                b = c.type,
                                c = c.foodType,
                                d = c.name,
                                e = c.nutrition,
                                f = c.efficacy,
                                g = c.imageHeightCal,
                                v = c.updateCode
                            };

            //去除重复
            var linqData2 = (from c in linqData1
                              group c by new { c.a, c.b } into g
                              select g.FirstOrDefault()).ToList();

            //去除适宜与避免相同的食材
            var linqData3 = (from c in linqData2
                             where c.b == 1
                             select c.a).Intersect(
                            from o in linqData2
                            where o.b == 2
                            select o.a
                            );

            return linqData2.Where(c => !linqData3.Contains(c.a)).ToList();
        }
    }
}