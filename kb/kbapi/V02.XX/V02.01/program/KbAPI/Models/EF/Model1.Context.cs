﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KbEntities : DbContext
    {
        public KbEntities()
            : base("name=KbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<P0101> P0101 { get; set; }
        public virtual DbSet<P0103> P0103 { get; set; }
        public virtual DbSet<P0104> P0104 { get; set; }
        public virtual DbSet<P0301> P0301 { get; set; }
        public virtual DbSet<P0302> P0302 { get; set; }
        public virtual DbSet<P0401> P0401 { get; set; }
        public virtual DbSet<V0301> V0301 { get; set; }
        public virtual DbSet<P0102> P0102 { get; set; }
        public virtual DbSet<P0109> P0109 { get; set; }
        public virtual DbSet<P0201> P0201 { get; set; }
    }
}