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
    
    public partial class MESSAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MESSAGE()
        {
            this.MESSAGE_TRANSLATION = new HashSet<MESSAGE_TRANSLATION>();
            this.TRANSACTIONS_MESSAGE = new HashSet<TRANSACTIONS_MESSAGE>();
        }
    
        public int idMessage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE_TRANSLATION> MESSAGE_TRANSLATION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACTIONS_MESSAGE> TRANSACTIONS_MESSAGE { get; set; }
    }
}
