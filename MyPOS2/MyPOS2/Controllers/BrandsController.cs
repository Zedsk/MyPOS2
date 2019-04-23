using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPOS2.BL;
using MyPOS2.Data.Entity;

namespace MyPOS2.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        //// GET: Brands
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            var brands = db.BRANDs.ToList();
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!String.IsNullOrEmpty((searchString)))
            {
                brands = brands.Where(s => s.nameBrand.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    brands = brands.OrderByDescending(d => d.nameBrand).ToList();
                    break;
                default:
                    brands = brands.OrderBy(d => d.nameBrand).ToList();
                    break;
            }

            return View(brands);
        }

        //// GET: Brands/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRANDs.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        //// GET: Brands/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Brands/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idBrand,nameBrand,imageBrand")] BRAND bRAND)
        {
            if (ModelState.IsValid)
            {
                db.BRANDs.Add(bRAND);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bRAND);
        }

        //// GET: Brands/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRANDs.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        //// POST: Brands/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idBrand,nameBrand,imageBrand")] BRAND bRAND)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bRAND).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bRAND);
        }

        //// GET: Brands/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRANDs.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        //// POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BRAND bRAND = db.BRANDs.Find(id);
            db.BRANDs.Remove(bRAND);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Import(HttpPostedFileBase file, string source)
        {
            string path = ImportBL.ImportImage(file, source);
            file.SaveAs(Server.MapPath(path));
            ViewBag.path = path;
            return PartialView("_PartialImageName");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
