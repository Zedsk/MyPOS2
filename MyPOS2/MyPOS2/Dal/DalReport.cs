using MyPOS2.Data.Entity;
using MyPOS2.Models.report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.Dal
{
    public class DalReport : IDalReport
    {
        #region DB

        private Pos1Entities db;

        public DalReport()
        {
            db = new Pos1Entities();
        }
                
        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        public IList<SPP_ReportTotalSalesTransDistinct_Result> CreateReportTotalSales(int yearD, int monthD, int dayD, int status, bool isReturn, int language)
        {
            List<SPP_ReportTotalSalesTransDistinct_Result> result = new List<SPP_ReportTotalSalesTransDistinct_Result>();
            result = db.SPP_ReportTotalSalesTransDistinct(yearD, monthD, dayD, status, isReturn, language).ToList();
            return result;
        }

        public IList<SPP_ReportTotalSalesByProductsTransDistinct_Result> CreateReportTotalSalesByProduct(int yearD, int monthD, int dayD, int status, bool isReturn, int language)
        {
            return db.SPP_ReportTotalSalesByProductsTransDistinct(yearD, monthD, dayD, status, isReturn, language).ToList();
        }
    }
}