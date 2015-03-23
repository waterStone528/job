
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P02
{
    /// <summary>
    ///     获取测试题目（文字）response的内容
    /// </summary>
    public class TestWordResponse
    {
        public int a { get; set; }         //题目id
        public int? b { get; set; }        //排序id
        public string c { get; set; }      //问题内容
        public int? v { get; set; }        //版本号
    }
}