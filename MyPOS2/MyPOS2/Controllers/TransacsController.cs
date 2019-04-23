using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class TransacsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        //// GET: Transacs
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        //[Authorize(Roles = "vendor")]
        public ActionResult Index()
        {
            DateTime date = DateTime.Today;
            var transactions = db.TRANSACTIONSs.Where(t => t.transactionDateEnd.Year == date.Year && t.transactionDateEnd.Month == date.Month && t.transactionDateEnd.Day == date.Day).Include(t => t.CUSTOMER).Include(t => t.SHOP).Include(t => t.STATUS).Include(t => t.TERMINAL).Include(t => t.USERINFO).Include(t => t.SHOP.SHOP_TRANSLATION).ToList();
            
            return View(transactions.ToList());
        }

        //// GET: Transacs/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        //[Authorize(Roles = "vendor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTIONS transaction = db.TRANSACTIONSs.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //// GET: Transacs/Create
        //public ActionResult Create()
        //{
        //    ViewBag.customerId = new SelectList(db.CUSTOMERs, "customerId", "customerId");
        //    ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone");
        //    ViewBag.statusId = new SelectList(db.STATUSs, "idStatus", "nameStatus");
        //    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal");
        //    ViewBag.userId = new SelectList(db.USERINFOs, "userId", "nameUser");
        //    return View();
        //}

        //// POST: Transacs/Create
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idTransaction,transactionDateBegin,transactionDateEnd,total,discountGlobal,isReturn,terminalId,shopId,customerId,statusId,userId")] TRANSACTIONS transaction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TRANSACTIONSs.Add(transaction);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.customerId = new SelectList(db.CUSTOMERs, "customerId", "customerId", transaction.customerId);
        //    ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", transaction.shopId);
        //    ViewBag.statusId = new SelectList(db.STATUSs, "idStatus", "nameStatus", transaction.statusId);
        //    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", transaction.terminalId);
        //    ViewBag.userId = new SelectList(db.USERINFOs, "userId", "nameUser", transaction.userId);
        //    return View(transaction);
        //}

        //// GET: Transacs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TRANSACTIONS transaction = db.TRANSACTIONSs.Find(id);
        //    if (transaction == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.customerId = new SelectList(db.CUSTOMERs, "customerId", "customerId", transaction.customerId);
        //    ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", transaction.shopId);
        //    ViewBag.statusId = new SelectList(db.STATUSs, "idStatus", "nameStatus", transaction.statusId);
        //    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", transaction.terminalId);
        //    ViewBag.userId = new SelectList(db.USERINFOs, "userId", "nameUser", transaction.userId);
        //    return View(transaction);
        //}

        //// POST: Transacs/Edit/5
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idTransaction,transactionDateBegin,transactionDateEnd,total,discountGlobal,isReturn,terminalId,shopId,customerId,statusId,userId")] TRANSACTIONS transaction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(transaction).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.customerId = new SelectList(db.CUSTOMERs, "customerId", "customerId", transaction.customerId);
        //    ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", transaction.shopId);
        //    ViewBag.statusId = new SelectList(db.STATUSs, "idStatus", "nameStatus", transaction.statusId);
        //    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", transaction.terminalId);
        //    ViewBag.userId = new SelectList(db.USERINFOs, "userId", "nameUser", transaction.userId);
        //    return View(transaction);
        //}

        // GET: Transacs/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTIONS tRANSACTIONS = db.TRANSACTIONSs.Find(id);
            if (tRANSACTIONS == null)
            {
                return HttpNotFound();
            }
            return View(tRANSACTIONS);
        }

        // POST: Transacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(TRANSACTIONS tr, int id)
        {
            TRANSACTIONS transaction = db.TRANSACTIONSs.Find(id);
            if (tr.cancelDescritpion != null)
            {
                transaction.cancelDescritpion = tr.cancelDescritpion;
                transaction.statusId = db.STATUSs.Where(s => s.nameStatus == "deleted").Select(st => st.idStatus).Single();
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.noReason = "Un motif est obligatoire";
            return View(transaction);
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
