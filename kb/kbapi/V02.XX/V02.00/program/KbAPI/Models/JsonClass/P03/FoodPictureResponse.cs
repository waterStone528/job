using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P03
{
    /// <summary>
    ///     获取食材图片的response
    /// </summary>
    public class FoodPictureResponse
    {
        public int a { get; set; }     //食材id
        public byte[] b { get; set; }  //图片
        public int? v { get; set; }    //图片版本号
    }
}