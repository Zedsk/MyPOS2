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
    using System.Collections.Generic;
    
    public partial class PAYMENT
    {
        public int idPayment { get; set; }
        public int paymentMethodId { get; set; }
        public decimal amount { get; set; }
        public int transactionId { get; set; }
    
        public virtual TRANSACTIONS TRANSACTIONS { get; set; }
        public virtual PAYMENT_METHOD PAYMENT_METHOD { get; set; }
    }
}