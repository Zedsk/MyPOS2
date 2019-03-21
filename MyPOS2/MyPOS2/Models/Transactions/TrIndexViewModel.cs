using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Models.Transactions
{
    public class TrIndexViewModel
    {
        [Required(ErrorMessage = "Un n° de terminal est nécessaire")]
        [DataType(DataType.Text)]
        public int TerminalId { get; set; }

        [Required(ErrorMessage = "Un n° de transaction est nécessaire")]
        [DataType(DataType.Text)]
        [Display(Name = "n° Opération")]
        public string NumTransaction { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public string DateDay { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Heure")]
        public string HourDay { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Vendeur")]
        public string Vendor { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Remise globale (%)")]
        [Range(0, 100, ErrorMessage = "valeur devant être comprise entre 0 et 100")]
        public decimal? GlobalDiscount { get; set; }

        [Required(ErrorMessage = "Un Total est nécessaire")]
        [DataType(DataType.Text)]
        [Display(Name = "TOTAL")]
        public string GlobalTot { get; set; }

        //[Required(ErrorMessage = "TVA obligatoire")]
        //[DataType(DataType.Text)]
        //[Display(Name = "TVA")]
        //public decimal GlobalVAT { get; set; }

        //public IList<VAT> VatsList { get; set; }
        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }

        //[DataType(DataType.Text)]
        //[Display(Name = "Nom de caisse")]
        //public string TerminalName { get; set; }

        //public IList<string> TerminalsNames { get; set; }
        //public IList<TERMINAL> TerminalsList { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Produit")]
        //public string ProductName { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Prix")]
        //public string Price { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Qtité")]
        //public string Quantity { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Remise")]
        //public string Discount { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "TVA")]
        //public string ProductVat { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "TotalItem")]
        //public string TotalItem { get; set; }

        //public IList<TRANSACTION_DETAILS> TransactionDetailsListById { get; set; }
    }
}