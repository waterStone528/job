//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KbAPI.API.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class P0102
    {
        public int id { get; set; }
        public int userId { get; set; }
        public Nullable<int> pinHZScore { get; set; }
        public Nullable<int> qiXZScore { get; set; }
        public Nullable<int> yangXZScore { get; set; }
        public Nullable<int> yinXZScore { get; set; }
        public Nullable<int> tanSZScore { get; set; }
        public Nullable<int> shiRZScore { get; set; }
        public Nullable<int> xueYZScore { get; set; }
        public Nullable<int> qiYZScore { get; set; }
        public Nullable<int> teBZScore { get; set; }
        public Nullable<int> times { get; set; }
        public Nullable<System.DateTime> testTime { get; set; }
    
        public virtual P0101 P0101 { get; set; }
    }
}