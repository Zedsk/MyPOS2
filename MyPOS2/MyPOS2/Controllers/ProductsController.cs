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
    public class ProductsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.PRODUCTs.Include(p => p.AGE).Include(p => p.BRAND).Include(p => p.CATEGORY).Include(p => p.HERO).Include(p => p.VAT);
            return View(product.ToList());
            //int lang = LanguageBL.CheckLanguageSession();
            //var productsT;
            //return View(productsT);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ageId = new SelectList(db.AGEs, "idAge", "imageAge");
            ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand");
            ViewBag.categoryId = new SelectList(db.CATEGORYs, "idCategory", "imageCat");
            ViewBag.heroId = new SelectList(db.HEROs, "idHero", "imageHero");
            ViewBag.vatId = new SelectList(db.VATs, "idVat", "idVat");
            return View();
        }

        // POST: Products/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduct,barcode,salesPrice,discountRate,imageProduct,categoryId,ageId,brandId,heroId,vatId")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTs.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ageId = new SelectList(db.AGEs, "idAge", "imageAge", pRODUCT.ageId);
            ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand", pRODUCT.brandId);
            ViewBag.categoryId = new SelectList(db.CATEGORYs, "idCategory", "imageCat", pRODUCT.categoryId);
            ViewBag.heroId = new SelectList(db.HEROs, "idHero", "imageHero", pRODUCT.heroId);
            ViewBag.vatId = new SelectList(db.VATs, "idVat", "idVat", pRODUCT.vatId);
            return View(pRODUCT);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ageId = new SelectList(db.AGEs, "idAge", "imageAge", pRODUCT.ageId);
            ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand", pRODUCT.brandId);
            ViewBag.categoryId = new SelectList(db.CATEGORYs, "idCategory", "imageCat", pRODUCT.categoryId);
            ViewBag.heroId = new SelectList(db.HEROs, "idHero", "imageHero", pRODUCT.heroId);
            ViewBag.vatId = new SelectList(db.VATs, "idVat", "idVat", pRODUCT.vatId);
            return View(pRODUCT);
        }

        // POST: Products/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduct,barcode,salesPrice,discountRate,imageProduct,categoryId,ageId,brandId,heroId,vatId")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ageId = new SelectList(db.AGEs, "idAge", "imageAge", pRODUCT.ageId);
            ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand", pRODUCT.brandId);
            ViewBag.categoryId = new SelectList(db.CATEGORYs, "idCategory", "imageCat", pRODUCT.categoryId);
            ViewBag.heroId = new SelectList(db.HEROs, "idHero", "imageHero", pRODUCT.heroId);
            ViewBag.vatId = new SelectList(db.VATs, "idVat", "idVat", pRODUCT.vatId);
            return View(pRODUCT);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            db.PRODUCTs.Remove(pRODUCT);
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
