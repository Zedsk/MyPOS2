using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPOS2.Dal;
using MyPOS2.Data.Entity;
using MyPOS2.Models.report;

namespace MyPOS2.BL
{
    public class ReportBL
    {
        internal static IList<TYPE_REPORT> FindAllTypesReport()
        {
            //provisoire list types report
            TYPE_REPORT type1 = new TYPE_REPORT
            {
                idTypeReport = 1,
                nameTypeReport = "Vente totale"
            };
            TYPE_REPORT type2 = new TYPE_REPORT
            {
                idTypeReport = 2,
                nameTypeReport = "Vente totale par article"
            };

            List<TYPE_REPORT> typesReport = new List<TYPE_REPORT>();
            typesReport.Add(type1);
            typesReport.Add(type2);
            return typesReport;
        }

        //provisoire type report
        public class TYPE_REPORT
        {
            public TYPE_REPORT() { }

            public int idTypeReport { get; set; }
            public string nameTypeReport { get; set; }
        }

        internal static RTotalSalesViewModel FindReportTotalSales(ReportMenuViewModel vmodel, int language)
        {
            using (IDalReport dal = new DalReport())
            {
                DateTime date;
                int yearD;
                int monthD;
                int dayD = 0;
                if (vmodel.Date != null)
                {
                    date = DateTime.Parse(vmodel.Date);
                    yearD = date.Year;
                    monthD = date.Month;
                    dayD = date.Day;
                }
                else
                {
                    date = DateTime.Now;
                    yearD = date.Year;
                    monthD = date.Month;
                }
                // to do --> change statusId = 2   and  isReturn = 
                int status = 2;
                bool isReturn = false;

                IList<SPP_ReportTotalSalesTransDistinct_Result> list = dal.CreateReportTotalSales(yearD, monthD, dayD, status, isReturn, language);
                RTotalSalesViewModel vm = new RTotalSalesViewModel
                {
                    Reports = list,
                    TotReport = CalculateTotalReport(list)
                };

                return vm;
                //return dal.CreateReportTotalSales(yearD, monthD, dayD, status, isReturn, language);
            }
        }

        private static string CalculateTotalReport(IList<SPP_ReportTotalSalesTransDistinct_Result> list)
        {
            decimal result = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                result += list[i].total;
            }
            return result.ToString();
        }

        private static string CalculateTotalReport(IList<SPP_ReportTotalSalesByProductsTransDistinct_Result> list)
        {
            decimal? result = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                result += list[i].totItem;
            }
            return result.ToString();
        }

        internal static RTotalSalesByProductViewModel FindReportTotalSalesByProduct(ReportMenuViewModel vmodel, int language)
        {
            using (IDalReport dal = new DalReport())
            {
                DateTime date;
                int yearD;
                int monthD;
                int dayD = 0;
                if (vmodel.Date != null)
                {
                    date = DateTime.Parse(vmodel.Date);
                    yearD = date.Year;
                    monthD = date.Month;
                    dayD = date.Day;
                }
                else
                {
                    date = DateTime.Now;
                    yearD = date.Year;
                    monthD = date.Month;
                }
                // to do --> change statusId = 2   and  isReturn = 
                int status = 2;
                bool isReturn = false;

                IList < SPP_ReportTotalSalesByProductsTransDistinct_Result > list = dal.CreateReportTotalSalesByProduct(yearD, monthD, dayD, status, isReturn, language);
                RTotalSalesByProductViewModel vm = new RTotalSalesByProductViewModel
                {
                    Reports = list,
                    TotReport = CalculateTotalReport(list)
                };
                return vm;
                //return dal.CreateReportTotalSalesByProduct(yearD, monthD, dayD, status, isReturn, language);
            }
        }
    }
}