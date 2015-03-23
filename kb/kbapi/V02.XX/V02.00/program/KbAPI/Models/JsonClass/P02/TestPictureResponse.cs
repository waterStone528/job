using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P02
{
    /// <summary>
    ///     获取测试题目（图片）的response
    /// </summary>
    public class TestPictureResponse
    {
        public int a { get; set; }       //题目id
        public byte[] b { get; set; }    //题目图片
        public int? v { get; set; }       //图片版本号
    }
}