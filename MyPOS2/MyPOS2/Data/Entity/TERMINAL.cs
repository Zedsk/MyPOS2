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
    
    public partial class TERMINAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TERMINAL()
        {
            this.CASH_BOTTOM_DAY = new HashSet<CASH_BOTTOM_DAY>();
            this.TRANSACTIONS = new HashSet<TRANSACTIONS>();
        }
    
        public int idTerminal { get; set; }
        public string nameTerminal { get; set; }
        public int shopId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CASH_BOTTOM_DAY> CASH_BOTTOM_DAY { get; set; }
        public virtual SHOP SHOP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACTIONS> TRANSACTIONS { get; set; }
    }
}
