using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPOS2.Data.Entity;

namespace MyPOS2.Controllers
{
    public class LanguagesController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Languages
        public ActionResult Index()
        {
            return View(db.LANGUAGESs.ToList());
        }

        // GET: Languages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANGUAGES lANGUAGES = db.LANGUAGESs.Find(id);
            if (lANGUAGES == null)
            {
                return HttpNotFound();
            }
            return View(lANGUAGES);
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLanguage,nameLanguage,shortForm")] LANGUAGES lANGUAGES)
        {
            if (ModelState.IsValid)
            {
                db.LANGUAGESs.Add(lANGUAGES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lANGUAGES);
        }

        // GET: Languages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANGUAGES lANGUAGES = db.LANGUAGESs.Find(id);
            if (lANGUAGES == null)
            {
                return HttpNotFound();
            }
            return View(lANGUAGES);
        }

        // POST: Languages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLanguage,nameLanguage,shortForm")] LANGUAGES lANGUAGES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lANGUAGES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lANGUAGES);
        }

        // GET: Languages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LANGUAGES lANGUAGES = db.LANGUAGESs.Find(id);
            if (lANGUAGES == null)
            {
                return HttpNotFound();
            }
            return View(lANGUAGES);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LANGUAGES lANGUAGES = db.LANGUAGESs.Find(id);
            db.LANGUAGESs.Remove(lANGUAGES);
            db.SaveChanges();
            return RedirectToAction("Index");
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
