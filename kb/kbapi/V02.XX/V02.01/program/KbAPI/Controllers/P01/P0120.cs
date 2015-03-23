using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P01
{
    /// <summary>
    ///     获得用户的9种体质得分
    /// </summary>
    public class P0120 : KbAPI.CL.Controller.Controller
    {
        public string GetConstitutionScoreHistory(int userId, List<Models.JsonClass.P01.ConstitutionScoreId> constitutionScoreId)
        {
            //获取
            Models.Business.P01.UserConstitutionInfo userConstitutionInfo = new Models.Business.P01.UserConstitutionInfo();
            List<Models.EF.P0102> p0102List = userConstitutionInfo.GetConstitutionScoreHistory(userId);

            //筛选
            var addId = (
                            from c in p0102List
                            select c.id
                        ).Except(
                            from o in constitutionScoreId
                            select o.a
                        );

            var ConstitutionScoreHistoryAdd = (from c in p0102List
                                               where addId.Contains(c.id)
                                               select new
                                               {
                                                   a = c.id,
                                                   b = c.times,
                                                   c = c.testTime,
                                                   d = c.pinHZScore,
                                                   e = c.qiXZScore,
                                                   f = c.yinXZScore,
                                                   g = c.yangXZScore,
                                                   h = c.tanSZScore,
                                                   i = c.shiRZScore,
                                                   j = c.xueYZScore,
                                                   k = c.qiYZScore,
                                                   l = c.teBZScore
                                               }).ToList();

            return EnJson(ConstitutionScoreHistoryAdd);
        }
    }
}