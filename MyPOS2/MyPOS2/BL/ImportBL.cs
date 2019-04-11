using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPOS2.BL
{
    public class ImportBL
    {
        internal static string ImportImage(HttpPostedFileBase file, string source)
        {
            string src = source.ToLower();

            //string path = Server.MapPath("~/Content/image/" + src + "/" + filename);
            string path = "~/Content/image/" + src + "/" + file.FileName;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return path;
        }
    }
}