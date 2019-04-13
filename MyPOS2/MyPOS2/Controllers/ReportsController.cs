using MyPOS2.BL;
using MyPOS2.Models.report;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPOS2.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            ReportMenuViewModel vm = new ReportMenuViewModel();
            vm.TypesReport = ReportBL.FindAllTypesReport();
            ViewBag.report = false;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(ReportMenuViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                int language = LanguageBL.CheckLanguageSession();
                switch (vmodel.TypeReportId)
                {
                    case "1":
                        vmodel.ReportTotalSales = ReportBL.FindReportTotalSales(vmodel, language);
                        ViewBag.report = true;
                        ViewBag.TypeReportId = vmodel.TypeReportId;
                        return PartialView("_PartialReportTotalSales", vmodel.ReportTotalSales);
                        
                    case "2":
                        vmodel.ReportTotalSalesByProduct = ReportBL.FindReportTotalSalesByProduct(vmodel, language);
                        ViewBag.report = true;
                        ViewBag.TypeReportId = vmodel.TypeReportId;
                        return PartialView("_PartialReportTotalSalesByProduct", vmodel.ReportTotalSalesByProduct);
                        
                    default:
                        break;
                }
            }
            vmodel.TypesReport = ReportBL.FindAllTypesReport();
            return View(vmodel);
        }
    }
}