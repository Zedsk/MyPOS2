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
    public class Cash_Bottom_DayController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Cash_Bottom_Day
        public ActionResult Index()
        {
            var cASH_BOTTOM_DAY = db.CASH_BOTTOM_DAY.Include(c => c.TERMINAL);
            return View(cASH_BOTTOM_DAY.ToList());
        }

        // GET: Cash_Bottom_Day/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CASH_BOTTOM_DAY cASH_BOTTOM_DAY = db.CASH_BOTTOM_DAY.Find(id);
            if (cASH_BOTTOM_DAY == null)
            {
                return HttpNotFound();
            }
            return View(cASH_BOTTOM_DAY);
        }

        // GET: Cash_Bottom_Day/Create
        public ActionResult Create()
        {
            ViewBag.terminalId = new SelectList(db.TERMINAL, "idTerminal", "nameTerminal");
            return View();
        }

        // POST: Cash_Bottom_Day/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dateDay,terminalId,beginningCash,endCash")] CASH_BOTTOM_DAY cASH_BOTTOM_DAY)
        {
            if (ModelState.IsValid)
            {
                db.CASH_BOTTOM_DAY.Add(cASH_BOTTOM_DAY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.terminalId = new SelectList(db.TERMINAL, "idTerminal", "nameTerminal", cASH_BOTTOM_DAY.terminalId);
            return View(cASH_BOTTOM_DAY);
        }

        // GET: Cash_Bottom_Day/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CASH_BOTTOM_DAY cASH_BOTTOM_DAY = db.CASH_BOTTOM_DAY.Find(id);
            if (cASH_BOTTOM_DAY == null)
            {
                return HttpNotFound();
            }
            ViewBag.terminalId = new SelectList(db.TERMINAL, "idTerminal", "nameTerminal", cASH_BOTTOM_DAY.terminalId);
            return View(cASH_BOTTOM_DAY);
        }

        // POST: Cash_Bottom_Day/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dateDay,terminalId,beginningCash,endCash")] CASH_BOTTOM_DAY cASH_BOTTOM_DAY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cASH_BOTTOM_DAY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.terminalId = new SelectList(db.TERMINAL, "idTerminal", "nameTerminal", cASH_BOTTOM_DAY.terminalId);
            return View(cASH_BOTTOM_DAY);
        }

        // GET: Cash_Bottom_Day/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CASH_BOTTOM_DAY cASH_BOTTOM_DAY = db.CASH_BOTTOM_DAY.Find(id);
            if (cASH_BOTTOM_DAY == null)
            {
                return HttpNotFound();
            }
            return View(cASH_BOTTOM_DAY);
        }

        // POST: Cash_Bottom_Day/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            CASH_BOTTOM_DAY cASH_BOTTOM_DAY = db.CASH_BOTTOM_DAY.Find(id);
            db.CASH_BOTTOM_DAY.Remove(cASH_BOTTOM_DAY);
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
