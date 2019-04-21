using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPOS2.BL;
using MyPOS2.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPOS2.Tests.BL
{
    [TestClass]
    public class ReportBLTest
    {
        [TestMethod]
        public void CalculateTotalReport_ListSPP_ReportTotalSalesTransDistinct_Result_Return100()
        {
            SPP_ReportTotalSalesTransDistinct_Result detail1 = new SPP_ReportTotalSalesTransDistinct_Result();
            SPP_ReportTotalSalesTransDistinct_Result detail2 = new SPP_ReportTotalSalesTransDistinct_Result();
            SPP_ReportTotalSalesTransDistinct_Result detail3 = new SPP_ReportTotalSalesTransDistinct_Result();
            SPP_ReportTotalSalesTransDistinct_Result detail4 = new SPP_ReportTotalSalesTransDistinct_Result();

            detail1.total = 25;
            detail2.total = 25;
            detail3.total = 10;
            detail4.total = 40;

            IList<SPP_ReportTotalSalesTransDistinct_Result> detailsList = new List<SPP_ReportTotalSalesTransDistinct_Result>
            {
                detail1,
                detail2,
                detail3,
                detail4
            };

            string result = ReportBL.CalculateTotalReport(detailsList);
            Assert.AreEqual("100", result);
        }

        [TestMethod]
        public void CalculateTotalReport_ListSPP_ReportTotalSalesByProductsTransDistinct_Result_Return100()
        {
            SPP_ReportTotalSalesByProductsTransDistinct_Result detail1 = new SPP_ReportTotalSalesByProductsTransDistinct_Result();
            SPP_ReportTotalSalesByProductsTransDistinct_Result detail2 = new SPP_ReportTotalSalesByProductsTransDistinct_Result();
            SPP_ReportTotalSalesByProductsTransDistinct_Result detail3 = new SPP_ReportTotalSalesByProductsTransDistinct_Result();
            SPP_ReportTotalSalesByProductsTransDistinct_Result detail4 = new SPP_ReportTotalSalesByProductsTransDistinct_Result();

            detail1.totItem = 25;
            detail2.totItem = 25;
            detail3.totItem = 10;
            detail4.totItem = 40;

            IList<SPP_ReportTotalSalesByProductsTransDistinct_Result> detailsList = new List<SPP_ReportTotalSalesByProductsTransDistinct_Result>
            {
                detail1,
                detail2,
                detail3,
                detail4
            };

            string result = ReportBL.CalculateTotalReport(detailsList);
            Assert.AreEqual("100", result);
        }

        [TestMethod]
        public void ModifyFormatDisplayDiscount_Receive0dotNumber_ReturnNumber()
        {
            
            SPP_ReportTotalSalesTransDistinct_Result detail1 = new SPP_ReportTotalSalesTransDistinct_Result();
            SPP_ReportTotalSalesTransDistinct_Result detail2 = new SPP_ReportTotalSalesTransDistinct_Result();
            SPP_ReportTotalSalesTransDistinct_Result detail3 = new SPP_ReportTotalSalesTransDistinct_Result();

            detail1.discountGlobal = (decimal)0.10;
            detail2.discountGlobal = (decimal)0.05;
            detail3.discountGlobal = (decimal)0.25;

            IList<SPP_ReportTotalSalesTransDistinct_Result> detailsList = new List<SPP_ReportTotalSalesTransDistinct_Result>
            {
                detail1,
                detail2,
                detail3
            };

            IList<SPP_ReportTotalSalesTransDistinct_Result> result = ReportBL.ModifyFormatDisplayDiscount(detailsList);
            Assert.AreEqual(10, result[0].discountGlobal);
            Assert.AreEqual(5, result[1].discountGlobal);
            Assert.AreEqual(25, result[2].discountGlobal);
        }
    }
}
