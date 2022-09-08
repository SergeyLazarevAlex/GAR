﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAR.BL.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbGARContext : DbContext
    {
        public DbGARContext()
            : base("name=DbGARContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AS_ADDR_OBJ> AS_ADDR_OBJ { get; set; }
        public virtual DbSet<AS_ADDR_OBJ_DIVISION> AS_ADDR_OBJ_DIVISION { get; set; }
        public virtual DbSet<AS_ADDR_OBJ_PARAMS> AS_ADDR_OBJ_PARAMS { get; set; }
        public virtual DbSet<AS_ADDR_OBJ_TYPES> AS_ADDR_OBJ_TYPES { get; set; }
        public virtual DbSet<AS_ADM_HIERARCHY> AS_ADM_HIERARCHY { get; set; }
        public virtual DbSet<AS_APARTMENT_TYPES> AS_APARTMENT_TYPES { get; set; }
        public virtual DbSet<AS_APARTMENTS> AS_APARTMENTS { get; set; }
        public virtual DbSet<AS_APARTMENTS_PARAMS> AS_APARTMENTS_PARAMS { get; set; }
        public virtual DbSet<AS_CARPLACES> AS_CARPLACES { get; set; }
        public virtual DbSet<AS_CARPLACES_PARAMS> AS_CARPLACES_PARAMS { get; set; }
        public virtual DbSet<AS_HOUSE_TYPES> AS_HOUSE_TYPES { get; set; }
        public virtual DbSet<AS_HOUSES> AS_HOUSES { get; set; }
        public virtual DbSet<AS_HOUSES_PARAMS> AS_HOUSES_PARAMS { get; set; }
        public virtual DbSet<AS_MUN_HIERARCHY> AS_MUN_HIERARCHY { get; set; }
        public virtual DbSet<AS_NORMATIVE_DOCS_KINDS> AS_NORMATIVE_DOCS_KINDS { get; set; }
        public virtual DbSet<AS_NORMATIVE_DOCS_TYPES> AS_NORMATIVE_DOCS_TYPES { get; set; }
        public virtual DbSet<AS_OBJECT_LEVELS> AS_OBJECT_LEVELS { get; set; }
        public virtual DbSet<AS_OPERATION_TYPES> AS_OPERATION_TYPES { get; set; }
        public virtual DbSet<AS_PARAM_TYPES> AS_PARAM_TYPES { get; set; }
        public virtual DbSet<AS_ROOM_TYPES> AS_ROOM_TYPES { get; set; }
        public virtual DbSet<AS_ROOMS> AS_ROOMS { get; set; }
        public virtual DbSet<AS_ROOMS_PARAMS> AS_ROOMS_PARAMS { get; set; }
        public virtual DbSet<AS_STEADS> AS_STEADS { get; set; }
        public virtual DbSet<AS_STEADS_PARAMS> AS_STEADS_PARAMS { get; set; }
        public virtual DbSet<AS_ADDHOUSE_TYPES> AS_ADDHOUSE_TYPES { get; set; }
    
        public virtual int GettingAnAddress(string kadnum, ObjectParameter adress)
        {
            var kadnumParameter = kadnum != null ?
                new ObjectParameter("kadnum", kadnum) :
                new ObjectParameter("kadnum", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GettingAnAddress", kadnumParameter, adress);
        }
    }
}