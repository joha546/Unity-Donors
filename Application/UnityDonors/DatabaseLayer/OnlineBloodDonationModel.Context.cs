﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Unity_DonorEntities : DbContext
    {
        public Unity_DonorEntities()
            : base("name=Unity_DonorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BloodBankStockTable> BloodBankStockTables { get; set; }
        public virtual DbSet<BloodBankTable> BloodBankTables { get; set; }
        public virtual DbSet<BloodGroupsTable> BloodGroupsTables { get; set; }
        public virtual DbSet<CityTable> CityTables { get; set; }
        public virtual DbSet<HospitalTable> HospitalTables { get; set; }
        public virtual DbSet<RequestTable> RequestTables { get; set; }
        public virtual DbSet<RequestTypeTable> RequestTypeTables { get; set; }
        public virtual DbSet<SeekerTable> SeekerTables { get; set; }
        public virtual DbSet<AccountStatusTable> AccountStatusTables { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }
        public virtual DbSet<UserTypeTable> UserTypeTables { get; set; }
        public virtual DbSet<DonorTable> DonorTables { get; set; }
        public virtual DbSet<GenderTable> GenderTables { get; set; }
        public virtual DbSet<BloodBankStockDetailTable> BloodBankStockDetailTables { get; set; }
    }
}
