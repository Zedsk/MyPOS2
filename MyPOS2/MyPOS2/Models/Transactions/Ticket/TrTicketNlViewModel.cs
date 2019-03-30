using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.Transactions.Ticket
{
    public class TrTicketNlViewModel
    {
        [DataType(DataType.DateTime)]
        [Display(Name = "Datum")]
        public string DateTicket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Taal")]
        public string Language { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Ticketnr.")]
        public string Ticket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Transactienr.")]
        public string Transaction { get; set; }

        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Korting")]
        public string DiscountG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "BTW")]
        public string VatG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Totaal ")]
        public string TotalG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Bedrag")]
        public string AmountP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Wijze van betaling")]
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