﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uspa.Domain.LocalDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UspaDbEntities : DbContext
    {
        public UspaDbEntities()
            : base("name=UspaDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Banners> Banners { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<MenuTypes> MenuTypes { get; set; }
        public virtual DbSet<Sites> Sites { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
