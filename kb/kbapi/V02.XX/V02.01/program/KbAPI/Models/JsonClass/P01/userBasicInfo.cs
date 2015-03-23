using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.P01
{
    public class UserBasicInfo
    {
        //用户id
        public int a { get; set; }
        //性别
        public bool? b { get; set; }
        //生日
        public DateTime? c { get; set; }
        //是否完成答题
        public bool d { get; set; }
    }
}