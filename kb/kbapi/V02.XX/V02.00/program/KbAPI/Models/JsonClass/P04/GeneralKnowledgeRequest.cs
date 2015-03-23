using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P04
{
    /// <summary>
    ///     饮食常识request参数
    /// </summary>
    public class GeneralKnowledgeRequest
    {
        public int a { get; set; }  //饮食常识id
        public int v { get; set; }  //版本号
    }
}