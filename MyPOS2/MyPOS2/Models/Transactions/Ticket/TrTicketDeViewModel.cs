using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.Transactions.Ticket
{
    public class TrTicketDeViewModel
    {
        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public string DateTicket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Sprache")]
        public string Language { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Ticket-Nr.")]
        public string Ticket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Transaktions-Nr.")]
        public string Transaction { get; set; }

        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Rabatt")]
        public string DiscountG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "MEHRWERTSTEUER")]
        public string VatG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Total ")]
        public string TotalG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Betrag")]
        public string AmountP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Zahlungsmethode")]
        public string MethodP { get; set; }

        public IList<PAYMENT> Payments { get; set; }

        [DataType(DataType.Text)]
        public string Message { get; set; }

        [DataType(DataType.Text)]
        public string MessageId { get; set; }

        [DataType(DataType.Text)]
        public List<string> Messages { get; set; }
    }
}