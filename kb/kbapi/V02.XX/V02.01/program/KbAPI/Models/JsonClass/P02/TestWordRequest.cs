using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P02
{
    /// <summary>
    ///     获取测试题目（文字）的request参数
    /// </summary>
    public class TestWordRequest
    {
        public int a { get; set; }     //问题id
        public int v { get; set; }     //版本号
    }
}