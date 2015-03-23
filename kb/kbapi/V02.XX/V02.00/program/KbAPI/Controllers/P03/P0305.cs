using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P03
{
    public class P0305 : KbAPI.CL.Controller.Controller
    {
        private Models.EF.KbEntities db = new Models.EF.KbEntities();

        public string GetFoodPicture(List<Models.JsonClass.P03.FoodPictureRequest> foodPictureRequest)
        {
            //防止在过滤处理时出错。List类型和其他容器类型一起，报错。
            //不加image字段，节约内存
            var foodBasicInfo = (from c in db.P0302
                                 select new
                                 {
                                     c.id,
                                     c.pVersion
                                 }).ToList();

            //过滤处理
            var foodPictureResponseId = (from c in foodBasicInfo
                                       from o in foodPictureRequest
                                       where c.id == o.a & c.pVersion > o.v
                                       select c.id).ToList();

            //获取最终数据
            var foodPictureResponse = (from c in db.P0302
                                       where foodPictureResponseId.Contains(c.id)
                                       select new Models.JsonClass.P03.FoodPictureResponse
                                       {
                                           a = c.id,
                                           b = c.image,
                                           v = c.pVersion
                                       }).ToList();

            var retObj = new { U = foodPictureResponse };
            return EnJson(retObj);
        }
    }
}