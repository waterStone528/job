using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P03
{
    public class P0301 : KbAPI.CL.Controller.Controller
    {
        public string GetFoodText(int userId,List<Models.JsonClass.P03.FoodTextRequst> foodTextRequst)
        {
            //获得所有的食材
            Models.Business.P03.Food food = new Models.Business.P03.Food();
            List<Models.JsonClass.P03.FoodInfo> foodInfoAll = food.GetAllFoodText(userId);

            //过滤处理
            //add
            IEnumerable<int> addId = (
                                 from c in foodInfoAll
                                 select c.a
                              ).Except(
                                 from o in foodTextRequst
                                 select o.a
                              );
            List<Models.JsonClass.P03.FoodInfo> foodInfoAdd = (from c in foodInfoAll
                                                               where addId.Contains(c.a)
                                                               select c).ToList();

            //update
            List<Models.JsonClass.P03.FoodInfo> foodInfoUpdate = (from c in foodInfoAll
                                                                  from o in foodTextRequst
                                                                  where c.a == o.a & c.v > o.v
                                                                  select c).ToList();

            //delete
            var foodInfoDelete = (
                                    (
                                        from c in foodTextRequst
                                        select new
                                        {
                                            a = c.a
                                        }
                                    ).Except(
                                        from o in foodInfoAll
                                        select new
                                        {
                                            a = o.a
                                        }
                                    )
                                 ).ToList();

            var retObj = new { A = foodInfoAdd, U = foodInfoUpdate, D = foodInfoDelete };
            return EnJson(retObj);
        }
    }
}