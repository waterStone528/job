using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P02
{
    /// <summary>
    ///     获取测试题目（图片）request的参数
    /// </summary>
    public class TestPictureRequest
    {
        public int a { get; set; }  //题目id
        public int v { get; set; }  //图片版本号
    }
}