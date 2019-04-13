using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Data.Entity;

namespace MyPOS2.Models.report
{
    public class RTotalSalesByProductViewModel
    {
        public SPP_ReportTotalSalesByProductsTransDistinct_Result Report { get; set; }

        public IList<SPP_ReportTotalSalesByProductsTransDistinct_Result> Reports { get; set; }
    }
}