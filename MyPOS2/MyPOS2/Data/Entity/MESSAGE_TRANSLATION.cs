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
    
    public partial class MESSAGE_TRANSLATION
    {
        public int messageId { get; set; }
        public int languageId { get; set; }
        public string title { get; set; }
        public string message { get; set; }
    
        public virtual LANGUAGES LANGUAGES { get; set; }
        public virtual TICKET_MESSAGE TICKET_MESSAGE { get; set; }
    }
}
