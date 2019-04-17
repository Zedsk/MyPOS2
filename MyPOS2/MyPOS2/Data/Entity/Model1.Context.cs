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
        public virtual DbSet<MESSAGE> MESSAGEs { get; set; }
        public virtual DbSet<MESSAGE_TRANSLATION> MESSAGE_TRANSLATIONs { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<PAYMENT_METHOD> PAYMENT_METHODs { get; set; }
        public virtual DbSet<PERSON> PERSONs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<PRODUCT_TRANSLATION> PRODUCT_TRANSLATIONs { get; set; }
        public virtual DbSet<SETTING> SETTINGs { get; set; }
        public virtual DbSet<SHOP> SHOPs { get; set; }
        public virtual DbSet<SHOP_PRODUCT> SHOP_PRODUCTs { get; set; }
        public virtual DbSet<SHOP_TRANSLATION> SHOP_TRANSLATIONs { get; set; }
        public virtual DbSet<STATUS> STATUSs { get; set; }
        public virtual DbSet<SUBCATEGORY> SUBCATEGORYs { get; set; }
        public virtual DbSet<TERMINAL> TERMINALs { get; set; }
        public virtual DbSet<TRANSACTION_DETAILS> TRANSACTION_DETAILSs { get; set; }
        public virtual DbSet<TRANSACTIONS> TRANSACTIONSs { get; set; }
        public virtual DbSet<TRANSACTIONS_MESSAGE> TRANSACTIONS_MESSAGEs { get; set; }
        public virtual DbSet<USERINFO> USERINFOs { get; set; }
        public virtual DbSet<VAT> VATs { get; set; }
    
        public virtual ObjectResult<SPP_CategoryTrans_Result> SPP_CategoryTrans()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_CategoryTrans_Result>("SPP_CategoryTrans");
        }
    
        public virtual ObjectResult<SPP_CategoryTransDistinct_Result> SPP_CategoryTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_CategoryTransDistinct_Result>("SPP_CategoryTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ChildCategoriesOfParent_Result> SPP_ChildCategoriesOfParent(Nullable<int> cat, Nullable<int> language)
        {
            var catParameter = cat.HasValue ?
                new ObjectParameter("cat", cat) :
                new ObjectParameter("cat", typeof(int));
    
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ChildCategoriesOfParent_Result>("SPP_ChildCategoriesOfParent", catParameter, languageParameter);
        }
    
        public virtual ObjectResult<SPP_ChildCategoriesOnlyTransDistinct_Result> SPP_ChildCategoriesOnlyTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ChildCategoriesOnlyTransDistinct_Result>("SPP_ChildCategoriesOnlyTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ChildCategoriesTransDistinct_Result> SPP_ChildCategoriesTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ChildCategoriesTransDistinct_Result>("SPP_ChildCategoriesTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_GetAllShop_Result> SPP_GetAllShop()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_GetAllShop_Result>("SPP_GetAllShop");
        }
    
        public virtual ObjectResult<SPP_GetShopById_Result> SPP_GetShopById(Nullable<int> shop)
        {
            var shopParameter = shop.HasValue ?
                new ObjectParameter("shop", shop) :
                new ObjectParameter("shop", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_GetShopById_Result>("SPP_GetShopById", shopParameter);
        }
    
        public virtual ObjectResult<SPP_HeroesTrans_Result> SPP_HeroesTrans()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_HeroesTrans_Result>("SPP_HeroesTrans");
        }
    
        public virtual ObjectResult<SPP_HeroesTransDistinct_Result> SPP_HeroesTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_HeroesTransDistinct_Result>("SPP_HeroesTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_MessageTrans_Result> SPP_MessageTrans()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_MessageTrans_Result>("SPP_MessageTrans");
        }
    
        public virtual ObjectResult<SPP_MessageTransDistinct_Result> SPP_MessageTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_MessageTransDistinct_Result>("SPP_MessageTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ParentCategoriesSubTransDistinct_Result> SPP_ParentCategoriesSubTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ParentCategoriesSubTransDistinct_Result>("SPP_ParentCategoriesSubTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ParentCategoriesTransDistinct_Result> SPP_ParentCategoriesTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ParentCategoriesTransDistinct_Result>("SPP_ParentCategoriesTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ParentCategoriesWithSubCatTransDistinct_Result> SPP_ParentCategoriesWithSubCatTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ParentCategoriesWithSubCatTransDistinct_Result>("SPP_ParentCategoriesWithSubCatTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ProductTrans_Result> SPP_ProductTrans(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ProductTrans_Result>("SPP_ProductTrans", languageParameter);
        }
    
        public virtual ObjectResult<SPP_ReportTotalSalesByProductsTransDistinct_Result> SPP_ReportTotalSalesByProductsTransDistinct(Nullable<int> tDate, Nullable<int> tMonth, Nullable<int> tDay, Nullable<int> tStatus, Nullable<bool> tReturn, Nullable<int> language)
        {
            var tDateParameter = tDate.HasValue ?
                new ObjectParameter("tDate", tDate) :
                new ObjectParameter("tDate", typeof(int));
    
            var tMonthParameter = tMonth.HasValue ?
                new ObjectParameter("tMonth", tMonth) :
                new ObjectParameter("tMonth", typeof(int));
    
            var tDayParameter = tDay.HasValue ?
                new ObjectParameter("tDay", tDay) :
                new ObjectParameter("tDay", typeof(int));
    
            var tStatusParameter = tStatus.HasValue ?
                new ObjectParameter("tStatus", tStatus) :
                new ObjectParameter("tStatus", typeof(int));
    
            var tReturnParameter = tReturn.HasValue ?
                new ObjectParameter("tReturn", tReturn) :
                new ObjectParameter("tReturn", typeof(bool));
    
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ReportTotalSalesByProductsTransDistinct_Result>("SPP_ReportTotalSalesByProductsTransDistinct", tDateParameter, tMonthParameter, tDayParameter, tStatusParameter, tReturnParameter, languageParameter);
        }
    
        public virtual ObjectResult<SPP_ReportTotalSalesTransDistinct_Result> SPP_ReportTotalSalesTransDistinct(Nullable<int> tDate, Nullable<int> tMonth, Nullable<int> tDay, Nullable<int> tStatus, Nullable<bool> tReturn, Nullable<int> language)
        {
            var tDateParameter = tDate.HasValue ?
                new ObjectParameter("tDate", tDate) :
                new ObjectParameter("tDate", typeof(int));
    
            var tMonthParameter = tMonth.HasValue ?
                new ObjectParameter("tMonth", tMonth) :
                new ObjectParameter("tMonth", typeof(int));
    
            var tDayParameter = tDay.HasValue ?
                new ObjectParameter("tDay", tDay) :
                new ObjectParameter("tDay", typeof(int));
    
            var tStatusParameter = tStatus.HasValue ?
                new ObjectParameter("tStatus", tStatus) :
                new ObjectParameter("tStatus", typeof(int));
    
            var tReturnParameter = tReturn.HasValue ?
                new ObjectParameter("tReturn", tReturn) :
                new ObjectParameter("tReturn", typeof(bool));
    
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ReportTotalSalesTransDistinct_Result>("SPP_ReportTotalSalesTransDistinct", tDateParameter, tMonthParameter, tDayParameter, tStatusParameter, tReturnParameter, languageParameter);
        }
    
        public virtual ObjectResult<SPP_ShopTrans_Result> SPP_ShopTrans()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ShopTrans_Result>("SPP_ShopTrans");
        }
    
        public virtual ObjectResult<SPP_ShopTransDistinct_Result> SPP_ShopTransDistinct(Nullable<int> language)
        {
            var languageParameter = language.HasValue ?
                new ObjectParameter("language", language) :
                new ObjectParameter("language", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_ShopTransDistinct_Result>("SPP_ShopTransDistinct", languageParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SPP_TicketNbItem(Nullable<System.DateTime> dateMin, Nullable<System.DateTime> dateMax, Nullable<int> nbItems)
        {
            var dateMinParameter = dateMin.HasValue ?
                new ObjectParameter("dateMin", dateMin) :
                new ObjectParameter("dateMin", typeof(System.DateTime));
    
            var dateMaxParameter = dateMax.HasValue ?
                new ObjectParameter("dateMax", dateMax) :
                new ObjectParameter("dateMax", typeof(System.DateTime));
    
            var nbItemsParameter = nbItems.HasValue ?
                new ObjectParameter("nbItems", nbItems) :
                new ObjectParameter("nbItems", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SPP_TicketNbItem", dateMinParameter, dateMaxParameter, nbItemsParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SPP_TicketTimeSure(Nullable<int> tDate, Nullable<int> tMonth, Nullable<int> tDay, Nullable<int> tHour, Nullable<int> tMinute, Nullable<int> tStatus, Nullable<bool> tReturn)
        {
            var tDateParameter = tDate.HasValue ?
                new ObjectParameter("tDate", tDate) :
                new ObjectParameter("tDate", typeof(int));
    
            var tMonthParameter = tMonth.HasValue ?
                new ObjectParameter("tMonth", tMonth) :
                new ObjectParameter("tMonth", typeof(int));
    
            var tDayParameter = tDay.HasValue ?
                new ObjectParameter("tDay", tDay) :
                new ObjectParameter("tDay", typeof(int));
    
            var tHourParameter = tHour.HasValue ?
                new ObjectParameter("tHour", tHour) :
                new ObjectParameter("tHour", typeof(int));
    
            var tMinuteParameter = tMinute.HasValue ?
                new ObjectParameter("tMinute", tMinute) :
                new ObjectParameter("tMinute", typeof(int));
    
            var tStatusParameter = tStatus.HasValue ?
                new ObjectParameter("tStatus", tStatus) :
                new ObjectParameter("tStatus", typeof(int));
    
            var tReturnParameter = tReturn.HasValue ?
                new ObjectParameter("tReturn", tReturn) :
                new ObjectParameter("tReturn", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SPP_TicketTimeSure", tDateParameter, tMonthParameter, tDayParameter, tHourParameter, tMinuteParameter, tStatusParameter, tReturnParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> SPP_TransactionMessageIds(Nullable<int> idTransac)
        {
            var idTransacParameter = idTransac.HasValue ?
                new ObjectParameter("idTransac", idTransac) :
                new ObjectParameter("idTransac", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("SPP_TransactionMessageIds", idTransacParameter);
        }
    
        public virtual ObjectResult<SPP_TransactionsDay_Result> SPP_TransactionsDay(Nullable<int> tDate, Nullable<int> tMonth, Nullable<int> tDay, Nullable<int> tStatus, Nullable<bool> tReturn)
        {
            var tDateParameter = tDate.HasValue ?
                new ObjectParameter("tDate", tDate) :
                new ObjectParameter("tDate", typeof(int));
    
            var tMonthParameter = tMonth.HasValue ?
                new ObjectParameter("tMonth", tMonth) :
                new ObjectParameter("tMonth", typeof(int));
    
            var tDayParameter = tDay.HasValue ?
                new ObjectParameter("tDay", tDay) :
                new ObjectParameter("tDay", typeof(int));
    
            var tStatusParameter = tStatus.HasValue ?
                new ObjectParameter("tStatus", tStatus) :
                new ObjectParameter("tStatus", typeof(int));
    
            var tReturnParameter = tReturn.HasValue ?
                new ObjectParameter("tReturn", tReturn) :
                new ObjectParameter("tReturn", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPP_TransactionsDay_Result>("SPP_TransactionsDay", tDateParameter, tMonthParameter, tDayParameter, tStatusParameter, tReturnParameter);
        }
    }
}
