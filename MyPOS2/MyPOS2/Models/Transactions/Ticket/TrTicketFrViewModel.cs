﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Models.Transactions.Ticket
{
    public class TrTicketFrViewModel
    {
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public string DateTicket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Langue")]
        public string Language { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "N° du ticket")]
        public string Ticket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "N° de la transaction")]
        public string Transaction { get; set; }
                               
        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Remise")]
        public string DiscountG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "TVA")]
        public string VatG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Total")]
        public string TotalG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Montant")]
        public string AmountP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Méthode de paiment")]
        public string MethodP { get; set; }

        public IList<PAYMENT> Payments { get; set; }

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