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
    public class AgesController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        //// GET: Ages
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            return View(db.AGEs.ToList());
        }

        //// GET: Ages/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGE aGE = db.AGEs.Find(id);
            if (aGE == null)
            {
                return HttpNotFound();
            }
            return View(aGE);
        }

        //// GET: Ages/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Ages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAge,imageAge,rangeAges")] AGE aGE)
        {
            if (ModelState.IsValid)
            {
                db.AGEs.Add(aGE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aGE);
        }

        //// GET: Ages/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGE aGE = db.AGEs.Find(id);
            if (aGE == null)
            {
                return HttpNotFound();
            }
            return View(aGE);
        }

        //// POST: Ages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAge,imageAge,rangeAges")] AGE aGE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aGE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aGE);
        }

        //// GET: Ages/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AGE aGE = db.AGEs.Find(id);
            if (aGE == null)
            {
                return HttpNotFound();
            }
            return View(aGE);
        }

        // POST: Ages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AGE aGE = db.AGEs.Find(id);
            db.AGEs.Remove(aGE);
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
