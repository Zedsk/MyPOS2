using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPOS2.BL;
using MyPOS2.Data.Entity;
using MyPOS2.Models.management;

namespace MyPOS2.Controllers
{
    public class ShopsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Shops
        public ActionResult Index()
        {
            //return View(db.SHOPs.ToList());
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
            int lang = LanguageBL.CheckLanguageSession();
            var shopsT = db.SPP_ShopTransDistinct(lang).ToList();
            return View(shopsT);
        }

        // GET: Shops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SHOP sHOP = db.SHOPs.Find(id);
            //if (sHOP == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(sHOP);
            IList<SPP_ShopTrans_Result> shops = db.SPP_ShopTrans().Where(s => s.idShop == id).ToList();
            return View(shops);
        }

        // GET: Shops/Create
        public ActionResult Create()
        {
            ShopViewModel vm = new ShopViewModel
            {
                ListLang = LanguageBL.FindLanguageListWithoutUniversal()
            };
            return View(vm);
        }

        //// POST: Shops/Create
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idShop,logoShop,phone,email,zipCode")] SHOP sHOP)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SHOPs.Add(sHOP);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(sHOP);
        //}

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IList<SHOP_TRANSLATION> shopsT = vmodel.ShopsTrans;
                    //if no image, assign default image
                    string logo = vmodel.LogoShop;
                    if (logo == null)
                    {
                        logo = "~/Content/image/logo_noImage.png";
                    }
                    SHOP shop = new SHOP
                    {
                        logoShop = logo,
                        phone = vmodel.Phone,
                        email = vmodel.Email,
                        zipCode = vmodel.ZipCode
                    };
                    db.SHOPs.Add(shop);
                    db.SaveChanges();
                    int id = shop.idShop;
                    //int id = 3;

                    //int count = shopsT.Count();
                    //bool isUniversal = TranslationBL.CheckIfUniversal(shopsT);
                    //if (isUniversal)
                    //{
                    //    for (int i = 0; i < shopsT.Count(); i++)
                    //    {
                    //        if (shopsT[i].nameShop != null)
                    //        {
                    //            shopsT[i].shopId = id;
                    //            //change language with universal
                    //            shopsT[i].languageId = LanguageBL.FindIdLanguageByShortForm("all");
                    //            //shopsT[i].street =
                    //            //shopsT[i].city =
                    //            //shopsT[i].opening =
                    //        }
                    //        else
                    //        {
                    //            shopsT.Remove(shopsT[i]);
                    //            i--;
                    //        }
                    //    }
                    //}
                    //else
                    //{

                    //}
                    foreach (var item in shopsT)
                    {
                        item.shopId = id;
                    }
                    db.SHOP_TRANSLATIONs.AddRange(shopsT);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(DbEntityValidationException ex)
                {
                    // to do insert to log file
                    var e1 = ex.GetBaseException(); // --> log
                    var e4 = ex.Message; // --> log
                    var e5 = ex.Source; // --> log
                    var e8 = ex.GetType(); // --> log
                    var e9 = ex.GetType().Name; // --> log
                    //TempData["Error"] = "L'initialisation de la transaction ne s'est pas déroulé correctement, veuillez contacter l'administrateur";
                    return View("Error");
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
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Shops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHOP sHOP = db.SHOPs.Find(id);
            if (sHOP == null)
            {
                return HttpNotFound();
            }
            var translation = db.SHOP_TRANSLATIONs.Where(st => st.shopId == id).ToList();
            var lang = db.LANGUAGESs.Count();
            ShopViewModel vm = new ShopViewModel();
            bool isUniversal = false;
            if (translation.Count() == 1)
            {
                isUniversal = true;
                SHOP_TRANSLATION empty = new SHOP_TRANSLATION
                {
                    shopId = 0,
                    languageId = 0,
                    nameShop = "",
                    street = "",
                    city = "",
                    opening = ""
                };
                for (int i = 0; i < (lang - 2); i++)
                {
                    translation.Add(empty);
                }
            }
            ViewBag.isUniversal = isUniversal;
            vm.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            vm.Shop = sHOP;
            vm.ShopsTrans = translation;
            return View(vm);
        }

        //// POST: Shops/Edit/5
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idShop,logoShop,phone,email,zipCode")] SHOP sHOP)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sHOP).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(sHOP);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShopViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //bool isUniversal = TranslationBL.CheckIfUniversal(vmodel.ShopsTrans);
                db.Entry(vmodel.Shop).State = EntityState.Modified;
                foreach (var item in vmodel.ShopsTrans)
                {
                    //// to do --> manage if universal translation 
                    //if (item.nameShop != null)
                    //{
                    //    if (isUniversal)
                    //    {
                    //        item.languageId = LanguageBL.FindIdLanguageByShortForm("all");
                    //        db.Entry(item).State = EntityState.Modified;
                    //    }
                    //    else
                    //    {
                    //        db.Entry(item).State = EntityState.Modified;
                    //    }
                    //}

                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Shops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SHOP sHOP = db.SHOPs.Find(id);
            if (sHOP == null)
            {
                return HttpNotFound();
            }
            return View(sHOP);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SHOP sHOP = db.SHOPs.Find(id);
            db.SHOPs.Remove(sHOP);
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
