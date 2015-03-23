using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P04
{
    /// <summary>
    ///     饮食常识response
    /// </summary>
    public class GeneralKnowledgeResponse
    {
        public int a { get; set; }     //饮食常识id
        public string b { get; set; }  //问题
        public string c { get; set; }  //答案
        public int? v { get; set; }    //版本号
    }
}