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
    
    public partial class CATEGORY_TRANSLATION
    {
        public int categoryId { get; set; }
        public int languageId { get; set; }
        public string nameCategory { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        public virtual LANGUAGES LANGUAGES { get; set; }
    }
}
