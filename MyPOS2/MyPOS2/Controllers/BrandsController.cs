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
    public class BrandsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Brands
        public ActionResult Index()
        {
            return View(db.BRAND.ToList());
        }

        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRAND.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idBrand,nameBrand")] BRAND bRAND)
        {
            if (ModelState.IsValid)
            {
                db.BRAND.Add(bRAND);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bRAND);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRAND.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idBrand,nameBrand")] BRAND bRAND)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bRAND).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bRAND);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRAND.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BRAND bRAND = db.BRAND.Find(id);
            db.BRAND.Remove(bRAND);
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
