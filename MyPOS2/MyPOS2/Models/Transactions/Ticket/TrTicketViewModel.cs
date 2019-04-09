using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPOS2.Models.Transactions.Ticket
{
    public class TrTicketViewModel
    {
        [DataType(DataType.DateTime)]
        public string DateTicket { get; set; }

        [DataType(DataType.Text)]
        public string Language { get; set; }


        [DataType(DataType.Text)]
        public string Ticket { get; set; }

        [DataType(DataType.Text)]
        public string Transaction { get; set; }

        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }

        [DataType(DataType.Text)]
        public string DiscountG { get; set; }

        [DataType(DataType.Text)]
        public string VatG { get; set; }

        [DataType(DataType.Text)]
        public string TotalG { get; set; }

        [DataType(DataType.Text)]
        public string AmountP { get; set; }

        [DataType(DataType.Text)]
        public string MethodP { get; set; }

        public IList<PAYMENT> Payments { get; set; }

        public IList<string> MethodsTicket { get; set; }

        [DataType(DataType.Text)]
        public string Message { get; set; }

        [DataType(DataType.Text)]
        public string MessageId { get; set; }

        [DataType(DataType.Text)]
        public List<string> Messages { get; set; }

        ////------après intégration
        public SPP_GetShopById_Result Shop { get; set; }

        public List<SPP_GetShopById_Result> Shops { get; set; }

    }
}