using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P04
{
    public class P0401 : KbAPI.CL.Controller.Controller
    {
        public string GetGeneralKnowledge(List<Models.JsonClass.P04.GeneralKnowledgeRequest> generalKnowledgeRequest)
        {
            //获取所有的饮食常识
            Models.Business.P04.GeneralKnowledge generalKnowledge = new Models.Business.P04.GeneralKnowledge();
            List<Models.EF.P0401> p0401List = generalKnowledge.GetGeneralKnowledge();

            //处理
            //add
            var addId = (
                            from c in p0401List
                            select c.id
                        ).Except(
                            from o in generalKnowledgeRequest
                            select o.a
                        );
            List<Models.JsonClass.P04.GeneralKnowledgeResponse> generalKnowledgeResponseAdd = (
                                                                                                from c in p0401List
                                                                                                where addId.Contains(c.id)
                                                                                                select new Models.JsonClass.P04.GeneralKnowledgeResponse
                                                                                                {
                                                                                                    a = c.id,
                                                                                                    b = c.question,
                                                                                                    c = c.answer,
                                                                                                    v = c.updateCode
                                                                                                }
                                                                                               ).ToList();

            //update
            List<Models.JsonClass.P04.GeneralKnowledgeResponse> generalKnowledgeResponseUpdate = (
                                                                                                    from c in p0401List
                                                                                                    from o in generalKnowledgeRequest
                                                                                                    where c.id == o.a & c.updateCode > o.v
                                                                                                    select new Models.JsonClass.P04.GeneralKnowledgeResponse
                                                                                                    {
                                                                                                        a = c.id,
                                                                                                        b = c.question,
                                                                                                        c = c.answer,
                                                                                                        v = c.updateCode
                                                                                                    }
                                                                                                 ).ToList();

            //delete
            List<int> generalKnowledgeResponseDelete = ((
                                                         from c in generalKnowledgeRequest
                                                         select c.a
                                                       ).Except(
                                                         from o in p0401List
                                                         select o.id
                                                       )).ToList();

            //组装
            var retObj = new { A = generalKnowledgeResponseAdd, U = generalKnowledgeResponseUpdate, D = generalKnowledgeResponseDelete };
            return EnJson(retObj);
        }
    }
}