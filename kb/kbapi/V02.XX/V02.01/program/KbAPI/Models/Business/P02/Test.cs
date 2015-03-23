using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using JsonClassP02 = KbAPI.API.Models.JsonClass.P02;

namespace KbAPI.API.Models.Business.P02
{
    /// <summary>
    ///     测试相关的资源
    /// </summary>
    public class Test
    {
        private EF.KbEntities db = new EF.KbEntities();

        /// <summary>
        ///     获取所有的测试题目（文字）
        /// </summary>
        public List<JsonClass.P02.TestWordResponse> GetTestText(int userId)
        {
            //不同性别，题目不同
            int exceptId = db.P0101.Where(c => c.id == userId).First().gender == true ? 41 : 42;

            List<JsonClass.P02.TestWordResponse> testWordResponse = (from c in db.P0201
                                                                     where c.id != 62 & c.id != 63 & c.id != exceptId
                                                                    select new JsonClass.P02.TestWordResponse
                                                                    {
                                                                         a = c.id,
                                                                         b = c.sequenceId,
                                                                         c = c.questionContent,
                                                                         v = c.updateCode
                                                                    }).ToList();

            return testWordResponse;
        }
    }
}