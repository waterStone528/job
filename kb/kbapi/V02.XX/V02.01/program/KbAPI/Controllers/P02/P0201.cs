using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JsonClassP02 = KbAPI.API.Models.JsonClass.P02;
using BusinessP02 = KbAPI.API.Models.Business.P02;

namespace KbAPI.API.Controllers.P02
{
    /// <summary>
    ///     获取测试题目（文字）
    /// </summary>
    public class P0201 : KbAPI.CL.Controller.Controller
    {
        public string GetTestWord(int userId, List<JsonClassP02.TestWordRequest> testWordRequest)
        {
            //获取所有题库
            BusinessP02.Test test = new Models.Business.P02.Test();
            List<JsonClassP02.TestWordResponse> testWordResponseAll = test.GetTestText(userId);

            //处理获取
            //add
            var addId = (
                            from c in testWordResponseAll
                            select c.a
                        ).Except(
                            from o in testWordRequest
                            select o.a
                        );
            List<JsonClassP02.TestWordResponse> testWordResponseAdd = (from c in testWordResponseAll
                                                                       where addId.Contains(c.a)
                                                                       orderby c.b
                                                                       select new JsonClassP02.TestWordResponse
                                                                      {
                                                                          a = c.a,
                                                                          b = c.b,
                                                                          c = c.c,
                                                                          v = c.v
                                                                      }).ToList();

            //update
            List<JsonClassP02.TestWordResponse> testWordResponseUpdate = (from c in testWordResponseAll
                                                                          from o in testWordRequest
                                                                          where c.a == o.a
                                                                                & c.v > o.v
                                                                          select new JsonClassP02.TestWordResponse
                                                                          {
                                                                              a = c.a,
                                                                              b = c.b,
                                                                              c = c.c,
                                                                              v = c.v
                                                                          }).ToList();

            //delete
            var testWordResponseDelete = ((
                                             from c in testWordRequest
                                             select c.a
                                         ).Except(
                                             from c in testWordResponseAll
                                             select c.a
                                         )).ToList();

            //组装
            var ret = new { A = testWordResponseAdd, U = testWordResponseUpdate, D = testWordResponseDelete };
            return EnJson(ret);
        }
    }
}