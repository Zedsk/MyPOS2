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
    
    public partial class HERO_TRANSLATION
    {
        public int heroId { get; set; }
        public int languageId { get; set; }
        public string nameHero { get; set; }
    
        public virtual HERO HERO { get; set; }
        public virtual LANGUAGES LANGUAGES { get; set; }
    }
}
