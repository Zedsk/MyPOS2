using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPOS2.Controllers
{
    public class ImportController : Controller
    {

        //[HttpGet]
        //public ActionResult ImportImage()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ImportImage(HttpPostedFileBase image)
        //{
        //    if (image.ContentLength > 0)
        //    {
        //        var filename = Path.GetFileName(image.FileName);

        //        System.Drawing.Image sourceimage =
        //            System.Drawing.Image.FromStream(image.InputStream);
        //    }

        //    return View();
        //}

        public ActionResult ImportImage(HttpPostedFileBase file, string filename, string source)
        {
            string src = source.ToLower();
            //string path = Server.MapPath("~/Content/image/" + src + "/" + filename);
            string path = "~/Content/image/" + src + "/" + filename;
            
            //file.SaveAs(path);
            file.SaveAs(Server.MapPath(path));
            
            ViewBag.path = path;
            //switch (src)
            //{
            //    case "hero":
            //        return PartialView("_PartialTerminalName");

            //    default:
            //        break;
            //}
            return View();
        }
    }
}