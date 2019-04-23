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
    [Authorize]
    public class ShopsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Shops
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            int lang = LanguageBL.CheckLanguageSession();
            var shopsT = db.SPP_ShopTransDistinct(lang).ToList();

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!String.IsNullOrEmpty((searchString)))
            {
                shopsT = shopsT.Where(s => s.nameShop.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    shopsT = shopsT.OrderByDescending(d => d.nameShop).ToList();
                    break;
                default:
                    shopsT = shopsT.OrderBy(d => d.nameShop).ToList();
                    break;
            }

            return View(shopsT);
        }

        // GET: Shops/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IList<SPP_ShopTrans_Result> shops = db.SPP_ShopTrans().Where(s => s.idShop == id).ToList();
            return View(shops);
        }

        // GET: Shops/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            ShopViewModel vm = new ShopViewModel
            {
                ListLang = LanguageBL.FindLanguageListWithoutUniversal()
            };
            return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

                    //Add Translation
                    int id = shop.idShop;
                    IList<SHOP_TRANSLATION> shopsT = TranslationBL.VerifyIsUniversal(vmodel.ShopsTrans, id);

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
                    return View("Error");
                }
                
            }
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Shops/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShopViewModel vmodel)
        {
            if (ModelState.IsValid)
            {          
                db.Entry(vmodel.Shop).State = EntityState.Modified;
                IList<SHOP_TRANSLATION> shopsT = TranslationBL.VerifyIsUniversal(vmodel.ShopsTrans, vmodel.Shop.idShop);
                foreach (var item in shopsT)
                {                    
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Shops/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
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
