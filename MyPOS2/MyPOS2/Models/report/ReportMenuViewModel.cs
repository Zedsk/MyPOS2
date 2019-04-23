using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static MyPOS2.BL.ReportBL;

namespace MyPOS2.Models.report
{
    public class ReportMenuViewModel
    {
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public string Date { get; set; }

        [Display(Name = "Journalière")]
        public bool Dayly { get; set; }

        [Display(Name = "Mensuelle")]
        public bool Monthly { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type de rapport")]
        public string TypeReportId { get; set; }

        public IList<TYPE_REPORT> TypesReport { get; set; }

        public string TotTotalSales { get; set; }

        public RTotalSalesViewModel ReportTotalSales { get; set; }

        public RTotalSalesByProductViewModel ReportTotalSalesByProduct { get; set; }
    }
}