using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P02
{
    /// <summary>
    ///     获取测试题目（图片）
    /// </summary>
    public class P0205 : KbAPI.CL.Controller.Controller
    {
        private Models.EF.KbEntities db = new Models.EF.KbEntities();

        public string GetTestPicture(List<Models.JsonClass.P02.TestPictureRequest> testPictureRequest)
        {
            //防止在过滤处理时出错。List类型和其他容器类型一起，报错。
            //不加image字段，节约内存
            var testBasicInfo = (
                                    from c in db.P0201
                                    where c.id != 62 & c.id != 63
                                    select new
                                    {
                                        c.id,
                                        c.pVersion
                                    }
                                ).ToList();

            //处理过滤
            var testPictureId = (
                                    from c in testBasicInfo
                                    from o in testPictureRequest
                                    where c.id == o.a & c.pVersion > o.v
                                    select c.id
                                ).ToList();

            //获取最终数据
            var testPirctureResponse = (
                                            from c in db.P0201
                                            where testPictureId.Contains(c.id)
                                            select new Models.JsonClass.P02.TestPictureResponse
                                            {
                                                a = c.id,
                                                b = c.questionImage,
                                                v = c.pVersion
                                            }
                                       ).ToList();

            return EnJson(testPirctureResponse);
        }
    }
}