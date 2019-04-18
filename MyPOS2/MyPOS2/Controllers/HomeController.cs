using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPOS2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        [Authorize]
        public ActionResult Management()
        {
            ViewBag.Message = "Votre page de gestion.";
            return View();
        }

        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        //[Authorize(Roles = "vendor")]
        [Authorize]
        public ActionResult Transaction()
        {
            if (TempData["Error"] == null)
            {
                return View("Transaction");
            }
            else
            {
                ViewBag.Error = TempData["Error"].ToString();
                return View("Transaction");
            }
        }
    }
}