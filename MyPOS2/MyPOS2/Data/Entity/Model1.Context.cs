﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPOS2.Data.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Pos1Entities : DbContext
    {
        public Pos1Entities()
            : base("name=Pos1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AGE> AGEs { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BRAND> BRANDs { get; set; }
        public virtual DbSet<CASH_BOTTOM_DAY> CASH_BOTTOM_DAYs { get; set; }
        public virtual DbSet<CATEGORY> CATEGORYs { get; set; }
        public virtual DbSet<CATEGORY_TRANSLATION> CATEGORY_TRANSLATIONs { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }
        public virtual DbSet<HERO> HEROs { get; set; }
        public virtual DbSet<HERO_TRANSLATION> HERO_TRANSLATIONs { get; set; }
        public virtual DbSet<LANGUAGES> LANGUAGESs { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<PAYMENT_METHOD> PAYMENT_METHODs { get; set; }
        public virtual DbSet<PERSON> PERSONs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<PRODUCT_TRANSLATION> PRODUCT_TRANSLATIONs { get; set; }
        public virtual DbSet<SHOP> SHOPs { get; set; }
        public virtual DbSet<SHOP_PRODUCT> SHOP_PRODUCTs { get; set; }
        public virtual DbSet<SHOP_TRANSLATION> SHOP_TRANSLATIONs { get; set; }
        public virtual DbSet<STATUS> STATUSs { get; set; }
        public virtual DbSet<SUBCATEGORY> SUBCATEGORYs { get; set; }
        public virtual DbSet<TERMINAL> TERMINALs { get; set; }
        public virtual DbSet<TICKET_MESSAGE> TICKET_MESSAGEs { get; set; }
        public virtual DbSet<TRANSACTION_DETAILS> TRANSACTION_DETAILSs { get; set; }
        public virtual DbSet<TRANSACTIONS> TRANSACTIONSs { get; set; }
        public virtual DbSet<USERINFO> USERINFOs { get; set; }
        public virtual DbSet<VAT> VATs { get; set; }
    
        public virtual ObjectResult<SPP_ParentCategories_Result> SPP_ParentCategories(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ParentCategories_Result>("SPP_ParentCategories", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ChildCategories_Result> SPP_ChildCategories(Nullable<int> cat, Nullable<int> language)
        {
            var catParameter = cat.HasValue ?
                new ObjectParameter("cat", cat) :
                new ObjectParameter("cat", typeof(int));
    
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ChildCategories_Result>("SPP_ChildCategories", catParameter, languageParameter);
        }
    
        public virtual ObjectResult<SPP_ProductTrans_Result> SPP_ProductTrans(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ProductTrans_Result>("SPP_ProductTrans", languageParameter);
        }
    }
}
