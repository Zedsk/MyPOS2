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
    [Authorize]
    public class Payment_MethodsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Payment_Methods
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            return View(db.PAYMENT_METHODs.ToList());
        }

        // GET: Payment_Methods/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHODs.Find(id);
            if (pAYMENT_METHOD == null)
            {
                return HttpNotFound();
            }
            return View(pAYMENT_METHOD);
        }

        // GET: Payment_Methods/Create
        //[Authorize(Roles = "admin")]
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
                db.PAYMENT_METHODs.Add(pAYMENT_METHOD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pAYMENT_METHOD);
        }

        // GET: Payment_Methods/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHODs.Find(id);
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
        //[Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHODs.Find(id);
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
            PAYMENT_METHOD pAYMENT_METHOD = db.PAYMENT_METHODs.Find(id);
            db.PAYMENT_METHODs.Remove(pAYMENT_METHOD);
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
