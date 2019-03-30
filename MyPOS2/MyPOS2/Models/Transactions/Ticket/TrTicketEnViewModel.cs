using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.Transactions.Ticket
{
    public class TrTicketEnViewModel
    {
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public string DateTicket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Language")]
        public string Language { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Ticket No.")]
        public string Ticket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Transaction No.")]
        public string Transaction { get; set; }

        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Discount")]
        public string DiscountG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "VAT")]
        public string VatG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Total")]
        public string TotalG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Amount")]
        public string AmountP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Payment method")]
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