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
    
    public partial class P0302
    {
        public P0302()
        {
            this.P0301 = new HashSet<P0301>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> foodType { get; set; }
        public byte[] image { get; set; }
        public Nullable<int> imageHeightCal { get; set; }
        public string nutrition { get; set; }
        public string efficacy { get; set; }
        public Nullable<int> updateCode { get; set; }
        public Nullable<int> pVersion { get; set; }
    
        public virtual ICollection<P0301> P0301 { get; set; }
    }
}
