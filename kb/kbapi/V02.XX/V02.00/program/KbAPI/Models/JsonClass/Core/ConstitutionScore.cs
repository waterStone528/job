using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.JsonClass.Core
{
    /// <summary>
    ///     体质的得分
    /// </summary>
    public class ConstitutionScore
    {
        public int? pinHZ { get; set; }
        public int? qiXZ { get; set; }
        public int? yinXZ { get; set; }
        public int? yangXZ { get; set; }
        public int? tanSZ { get; set; }
        public int? shiRZ { get; set; }
        public int? xueYZ { get; set; }
        public int? qiYZ { get; set; }
        public int? teBZ { get; set; }
    }
}