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
using MyPOS2.Models.management;

namespace MyPOS2.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Products
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            //int lang = LanguageBL.CheckLanguageSession();
            var products = db.PRODUCTs.Include(p => p.AGE).Include(p => p.BRAND).Include(p => p.CATEGORY).Include(p => p.HERO).Include(p => p.VAT).ToList();

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "Name";
            ViewBag.BrandSortParam = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            if (!String.IsNullOrEmpty((searchString)))
            {
                //products = products.Where(s => s.BRAND.nameBrand.ToLower().StartsWith(searchString.ToLower())
                //                            || s.PRODUCT_TRANSLATION.FirstOrDefault(t => t.languageId == lang).nameProduct.ToLower().StartsWith(searchString.ToLower())).ToList();
                products = products.Where(s => s.BRAND.nameBrand.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    //products = products.OrderByDescending(d => d.PRODUCT_TRANSLATION.Select(s => s.nameProduct)).ToList();
                    break;
                case "Name":
                    //products = products.OrderBy(d => d.PRODUCT_TRANSLATION.Select(s => s.nameProduct)).ToList();
                    break;
                case "brand_desc":
                    products = products.OrderByDescending(d => d.BRAND.nameBrand).ToList();
                    break;
                default:
                    products = products.OrderBy(d => d.BRAND.nameBrand).ToList();
                    break;
            }

            return View(products);
            //int lang = LanguageBL.CheckLanguageSession();
            //var productsT;
            //return View(productsT);
        }

        // GET: Products/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
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
            var translation = db.PRODUCT_TRANSLATIONs.Where(pt => pt.productId == id).ToList();
            ViewBag.ProductTrans = translation;
            return View(pRODUCT);
        }

        //// GET: Products/Create
        //public ActionResult Create()
        //{
        //    int lang = LanguageBL.CheckLanguageSession();
        //    ViewBag.ageId = new SelectList(db.AGEs, "idAge", "rangeAges");
        //    ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand");
        //    ViewBag.categoryId = new SelectList(db.CATEGORY_TRANSLATIONs.Where(c => c.languageId == lang), "categoryId", "nameCategory");
        //    ViewBag.heroId = new SelectList(db.HERO_TRANSLATIONs.Where(c => c.languageId == lang), "heroId", "nameHero");
        //    ViewBag.vatId = new SelectList(db.VATs, "idVat", "appliedVat");
        //    return View();
        //}

        // GET: Products/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            int lang = LanguageBL.CheckLanguageSession();
            int ULang = LanguageBL.FindUniversalId();
            ProductViewModel vm = new ProductViewModel
            {
                ListLang = LanguageBL.FindLanguageListWithoutUniversal(),
                CategoriesTrans = db.CATEGORY_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList(),
                Ages = db.AGEs.ToList(),
                Brands = db.BRANDs.ToList(),
                HeroesTrans = db.HERO_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList(),
                Vats = db.VATs.ToList()

            };
            return View(vm);
        }



        //// POST: Products/Create
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idProduct,barcode,salesPrice,discountRate,imageProduct,categoryId,ageId,brandId,heroId,vatId")] PRODUCT pRODUCT)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PRODUCTs.Add(pRODUCT);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ageId = new SelectList(db.AGEs, "idAge", "rangeAges");
        //    ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand");
        //    ViewBag.categoryId = new SelectList(db.CATEGORY_TRANSLATIONs.Where(c => c.languageId == lang), "categoryId", "nameCategory");
        //    ViewBag.heroId = new SelectList(db.HERO_TRANSLATIONs.Where(c => c.languageId == lang), "heroId", "nameHero");
        //    ViewBag.vatId = new SelectList(db.VATs, "idVat", "appliedVat");
        //    return View(pRODUCT);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //if no image, assign default image
                    string img = vmodel.ImageProduct;
                    if (img == null)
                    {
                        img = "~/Content/image/logo_noImage.png";
                    }
                    // check if hero is null
                    int? hero = null;
                    if (vmodel.Hero != null)
                    {
                        hero = int.Parse(vmodel.Hero);
                    }
                    PRODUCT product = new PRODUCT
                    {
                        barcode = vmodel.Barcode,
                        salesPrice = vmodel.Price,
                        discountRate = vmodel.Discount,
                        imageProduct = img,
                        categoryId = int.Parse(vmodel.Category),
                        ageId = int.Parse(vmodel.Age),
                        brandId = int.Parse(vmodel.Brand),
                        heroId = hero,
                        vatId = int.Parse(vmodel.Vat)
                    };
                    db.PRODUCTs.Add(product);
                    db.SaveChanges();

                    //Add Translation
                    int id = product.idProduct;
                    IList<PRODUCT_TRANSLATION> productsT = TranslationBL.VerifyIsUniversal(vmodel.ProductsTrans, id);

                    db.PRODUCT_TRANSLATIONs.AddRange(productsT);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //to do insert to log file
                    var e1 = ex.GetBaseException(); // --> log
                    var e4 = ex.Message; // --> log
                    var e5 = ex.Source; // --> log
                    var e8 = ex.GetType(); // --> log
                    var e9 = ex.GetType().Name; // --> log
                    //TempData["Error"] = "L'initialisation de la transaction ne s'est pas déroulé correctement, veuillez contacter l'administrateur";
                    return View("Error");
                }
            }
            int lang = LanguageBL.CheckLanguageSession();
            int ULang = LanguageBL.FindUniversalId();
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            vmodel.CategoriesTrans = db.CATEGORY_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList();
            vmodel.Ages = db.AGEs.ToList();
            vmodel.Brands = db.BRANDs.ToList();
            vmodel.HeroesTrans = db.HERO_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList();
            vmodel.Vats = db.VATs.ToList();
            return View(vmodel);
        }

        //// GET: Products/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PRODUCT pRODUCT = db.PRODUCTs.Find(id);
        //    if (pRODUCT == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ageId = new SelectList(db.AGEs, "idAge", "imageAge", pRODUCT.ageId);
        //    ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand", pRODUCT.brandId);
        //    ViewBag.categoryId = new SelectList(db.CATEGORYs, "idCategory", "imageCat", pRODUCT.categoryId);
        //    ViewBag.heroId = new SelectList(db.HEROs, "idHero", "imageHero", pRODUCT.heroId);
        //    ViewBag.vatId = new SelectList(db.VATs, "idVat", "idVat", pRODUCT.vatId);
        //    return View(pRODUCT);
        //}

        // GET: Products/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
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
            var translation = db.PRODUCT_TRANSLATIONs.Where(pt => pt.productId == id).ToList();
            
            ProductViewModel vm = new ProductViewModel();
            bool isUniversal = false;
            if (translation.Count() == 1)
            {
                isUniversal = true;
            }
            ViewBag.isUniversal = isUniversal;
            //vm.ListLang = LanguageBL.FindLanguageListWithoutUniversal();

            vm.Product = pRODUCT;

            vm.IdProduct = pRODUCT.idProduct;
            vm.Barcode = pRODUCT.barcode;
            vm.Price = pRODUCT.salesPrice;
            vm.Discount = pRODUCT.discountRate;
            vm.ImageProduct = pRODUCT.imageProduct;
            vm.Category = pRODUCT.categoryId.ToString();
            vm.Age = pRODUCT.ageId.ToString();
            vm.Brand = pRODUCT.brandId.ToString();
            vm.Hero = pRODUCT.heroId.ToString();
            //vm.Vat = pRODUCT.vatId;

            vm.ProductsTrans = translation;

            int lang = LanguageBL.CheckLanguageSession();
            int ULang = LanguageBL.FindUniversalId();
            vm.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            vm.CategoriesTrans = db.CATEGORY_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList();
            vm.Ages = db.AGEs.ToList();
            vm.Brands = db.BRANDs.ToList();
            vm.HeroesTrans = db.HERO_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList();
            vm.Vats = db.VATs.ToList();
            return View(vm);
        }

        //// POST: Products/Edit/5
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idProduct,barcode,salesPrice,discountRate,imageProduct,categoryId,ageId,brandId,heroId,vatId")] PRODUCT pRODUCT)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(pRODUCT).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ageId = new SelectList(db.AGEs, "idAge", "imageAge", pRODUCT.ageId);
        //    ViewBag.brandId = new SelectList(db.BRANDs, "idBrand", "nameBrand", pRODUCT.brandId);
        //    ViewBag.categoryId = new SelectList(db.CATEGORYs, "idCategory", "imageCat", pRODUCT.categoryId);
        //    ViewBag.heroId = new SelectList(db.HEROs, "idHero", "imageHero", pRODUCT.heroId);
        //    ViewBag.vatId = new SelectList(db.VATs, "idVat", "idVat", pRODUCT.vatId);
        //    return View(pRODUCT);
        //}

        // POST: Products/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                ////if no image, assign default image
                //string img = vmodel.ImageProduct;
                //if (img == null)
                //{
                //    img = "~/Content/image/logo_noImage.png";
                //}
                //// check if hero is null
                //int? hero = null;
                //if (vmodel.Hero != null)
                //{
                //    hero = int.Parse(vmodel.Hero);
                //}
                //PRODUCT product = new PRODUCT
                //{
                //    barcode = vmodel.Barcode,
                //    salesPrice = vmodel.Price,
                //    discountRate = vmodel.Discount,
                //    imageProduct = img,
                //    categoryId = int.Parse(vmodel.Category),
                //    ageId = int.Parse(vmodel.Age),
                //    brandId = int.Parse(vmodel.Brand),
                //    heroId = hero,
                //    vatId = int.Parse(vmodel.Vat)
                //};
                //db.Entry(product).State = EntityState.Modified;

                db.Entry(vmodel.Product).State = EntityState.Modified;
                //db.SaveChanges();
                IList<PRODUCT_TRANSLATION> productsT = TranslationBL.VerifyIsUniversal(vmodel.ProductsTrans, vmodel.IdProduct);
                foreach (var item in vmodel.ProductsTrans)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            bool isUniversal = false;
            if (vmodel.ProductsTrans.Count() == 1)
            {
                isUniversal = true;
            }
            ViewBag.isUniversal = isUniversal;
            int lang = LanguageBL.CheckLanguageSession();
            int ULang = LanguageBL.FindUniversalId();
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            vmodel.CategoriesTrans = db.CATEGORY_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList();
            vmodel.Ages = db.AGEs.ToList();
            vmodel.Brands = db.BRANDs.ToList();
            vmodel.HeroesTrans = db.HERO_TRANSLATIONs.Where(c => c.languageId == lang || c.languageId == ULang).ToList();
            vmodel.Vats = db.VATs.ToList();

            return View(vmodel);
        }

        // GET: Products/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
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
