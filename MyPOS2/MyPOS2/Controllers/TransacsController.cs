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
            //var transactions = db.TRANSACTIONSs.Include(t => t.CUSTOMER).Include(t => t.SHOP).Include(t => t.STATUS).Include(t => t.TERMINAL).Include(t => t.USERINFO);
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
            //if (Session["Language"] == null)
            //{
            //    Session["Language"] = ConfigurationManager.AppSettings["Language"];
            //}
            //string language = Session["Language"].ToString();
            //int lang;
            //if (int.TryParse(language, out int codeL))
            //{
            //    lang = codeL;
            //}
            //else
            //{
            //    lang = LanguageBL.FindIdLanguageByShortForm(language);
            //}
            //ViewBag.language = lang;
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
        //public ActionResult Create([Bind(Include = "idTransaction,transactionDateBegin,transactionDateEnd,total,discountGlobal,isReturn,terminalId,shopId,customerId,statusId,userId")] TRANSACTIONS tRANSACTIONS)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TRANSACTIONSs.Add(tRANSACTIONS);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.customerId = new SelectList(db.CUSTOMERs, "customerId", "customerId", tRANSACTIONS.customerId);
        //    ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", tRANSACTIONS.shopId);
        //    ViewBag.statusId = new SelectList(db.STATUSs, "idStatus", "nameStatus", tRANSACTIONS.statusId);
        //    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", tRANSACTIONS.terminalId);
        //    ViewBag.userId = new SelectList(db.USERINFOs, "userId", "nameUser", tRANSACTIONS.userId);
        //    return View(tRANSACTIONS);
        //}

        //// GET: Transacs/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TRANSACTIONS tRANSACTIONS = db.TRANSACTIONSs.Find(id);
        //    if (tRANSACTIONS == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.customerId = new SelectList(db.CUSTOMERs, "customerId", "customerId", tRANSACTIONS.customerId);
        //    ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", tRANSACTIONS.shopId);
        //    ViewBag.statusId = new SelectList(db.STATUSs, "idStatus", "nameStatus", tRANSACTIONS.statusId);
        //    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", tRANSACTIONS.terminalId);
        //    ViewBag.userId = new SelectList(db.USERINFOs, "userId", "nameUser", tRANSACTIONS.userId);
        //    return View(tRANSACTIONS);
        //}

        //// POST: Transacs/Edit/5
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idTransaction,transactionDateBegin,transactionDateEnd,total,discountGlobal,isReturn,terminalId,shopId,customerId,statusId,userId")] TRANSACTIONS tRANSACTIONS)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tRANSACTIONS).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.customerId = new SelectList(db.CUSTOMERs, "customerId", "customerId", tRANSACTIONS.customerId);
        //    ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", tRANSACTIONS.shopId);
        //    ViewBag.statusId = new SelectList(db.STATUSs, "idStatus", "nameStatus", tRANSACTIONS.statusId);
        //    ViewBag.terminalId = new SelectList(db.TERMINALs, "idTerminal", "nameTerminal", tRANSACTIONS.terminalId);
        //    ViewBag.userId = new SelectList(db.USERINFOs, "userId", "nameUser", tRANSACTIONS.userId);
        //    return View(tRANSACTIONS);
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
        public ActionResult DeleteConfirmed(int id)
        {
            TRANSACTIONS tRANSACTIONS = db.TRANSACTIONSs.Find(id);
            db.TRANSACTIONSs.Remove(tRANSACTIONS);
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
