//------------------------------------------------------------------------------
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
    
    public partial class SPP_ReportTotalSalesTransDistinct_Result
    {
        public int idTransaction { get; set; }
        public System.DateTime transactionDateBegin { get; set; }
        public System.DateTime transactionDateEnd { get; set; }
        public decimal total { get; set; }
        public Nullable<decimal> discountGlobal { get; set; }
        public string cancelDescritpion { get; set; }
        public bool isReturn { get; set; }
        public string nameTerminal { get; set; }
        public string namePerson { get; set; }
        public string nameStatus { get; set; }
        public string nameUser { get; set; }
        public string nameShop { get; set; }
    }
}
