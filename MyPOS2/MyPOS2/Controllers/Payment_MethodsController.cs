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
    public class Payment_MethodsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Payment_Methods
        public ActionResult Index()
        {
            return View(db.PAYMENT_METHOD.ToList());
        }

        // GET: Payment_Methods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHOD.Find(id);
            if (pAYMENT_METHOD == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_METHOD);
        }

        // GET: Payment_Methods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment_Methods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPaymentMethod,method")] PAYMENT_METHOD pAYMENT_METHOD)
        {
            if (ModelState.IsValid)
            {
                db.PAYMENT_METHOD.Add(pAYMENT_METHOD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pAYMENT_METHOD);
        }

        // GET: Payment_Methods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHOD.Find(id);
            if (pAYMENT_METHOD == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_METHOD);
        }

        // POST: Payment_Methods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaymentMethod,method")] PAYMENT_METHOD pAYMENT_METHOD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAYMENT_METHOD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pAYMENT_METHOD);
        }

        // GET: Payment_Methods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHOD.Find(id);
            if (pAYMENT_METHOD == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_METHOD);
        }

        // POST: Payment_Methods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHOD.Find(id);
            db.PAYMENT_METHOD.Remove(pAYMENT_METHOD);
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
