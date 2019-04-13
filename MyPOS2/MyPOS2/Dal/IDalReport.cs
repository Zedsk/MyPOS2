using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPOS2.Data.Entity;
using MyPOS2.Models.report;

namespace MyPOS2.Dal
{
    interface IDalReport : IDisposable
    {
        IList<SPP_ReportTotalSalesTransDistinct_Result> CreateReportTotalSales(int yearD, int monthD, int dayD, int status, bool isReturn, int language);
        IList<SPP_ReportTotalSalesByProductsTransDistinct_Result> CreateReportTotalSalesByProduct(int yearD, int monthD, int dayD, int status, bool isReturn, int language);
    }
}
