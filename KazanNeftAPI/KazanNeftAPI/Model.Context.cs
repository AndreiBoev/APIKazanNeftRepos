﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KazanNeftAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KazanNeftEntities : DbContext
    {
        public KazanNeftEntities()
            : base("name=KazanNeftEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AssetGroups> AssetGroups { get; set; }
        public virtual DbSet<AssetPhotos> AssetPhotos { get; set; }
        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<AssetTransferLogs> AssetTransferLogs { get; set; }
        public virtual DbSet<DepartmentLocations> DepartmentLocations { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
    }
}
