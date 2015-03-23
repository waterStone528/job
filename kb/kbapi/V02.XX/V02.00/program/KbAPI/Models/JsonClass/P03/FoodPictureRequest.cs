using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P03
{
    /// <summary>
    ///     食材图片request的参数
    /// </summary>
    public class FoodPictureRequest
    {
        public int a { get; set; }  //食材id
        public int v { get; set; }  //版本号
    }
}