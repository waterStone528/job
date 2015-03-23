using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P03
{
    /// <summary>
    ///     食材信息
    /// </summary>
    public class FoodInfo
    {
        public int a { get; set; }     //食材id
        public int? b { get; set; }    //类型（适宜还是避免）
        public int? c { get; set; }    //食材类型（如肉类）
        public string d { get; set; }  //名称
        public string e { get; set; }  //营养
        public string f { get; set; }  //功效
        public int? g { get; set; }    //计算后的图片高度
        public int? v { get; set; }    //版本号
    }
}