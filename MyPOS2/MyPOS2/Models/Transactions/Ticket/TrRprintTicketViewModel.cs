using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.Transactions.Ticket
{
    public class TrRprintTicketViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateDay { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Heure")]
        public DateTime TimeDay { get; set; }

        [Display(Name = "Heure exacte")]
        public bool TimeSure { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "N° de client")]
        public string Client { get; set; }

        [RegularExpression("^[0-9.0-9 || 0-9,0-9]*$", ErrorMessage = "seul les nombres décimaux sont acceptés, ex: 199,99 ou 199.99")]
        [DataType(DataType.Text)]
        [Display(Name = "Montant")]
        public string GlobalTotal { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Méthodes de paiement")]
        public string MethodP { get; set; }

        public IList<PAYMENT_METHOD> MethodsP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Nbre de produits")]
        public string NbItem { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Langue")]
        public string Language { get; set; }

        public IList<LANGUAGES> Languages { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "n° Opération")]
        public string NumTransaction { get; set; }

        public TrTicketViewModel Ticket { get; set; }

        public IList<TrTicketViewModel> Tickets { get; set; }


    }
}