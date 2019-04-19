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
    public class CategoriesController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Categories
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            //return View(db.CATEGORYs.ToList());
            CategoryViewModel vm = new CategoryViewModel();
            //Find all cat by language
            int lang = LanguageBL.CheckLanguageSession();
            var categoriesT = db.SPP_CategoryTransDistinct(lang).ToList();
            //Find relation for all cat
            IList<int> parentOnly = db.SPP_ParentCategoriesSubTransDistinct(lang).Select(p => p.idCategory).ToList();
            IList<int> childOnly = db.SPP_ChildCategoriesTransDistinct(lang).Select(p => p.idCategory).ToList();
            IList<CategoryViewModel> list = new List<CategoryViewModel>();
            string rel;
            foreach (var item in categoriesT)
            {
                if (childOnly.Contains(item.idCategory) && parentOnly.Contains(item.idCategory))
                {
                    rel = "parent/sous-catégorie";
                }
                else if (childOnly.Contains(item.idCategory))
                {
                    rel = "sous-catégorie";
                }
                else
                {
                    rel = "catégorie parent";
                }

                CategoryViewModel cat = new CategoryViewModel
                {
                    IdCat = item.idCategory,
                    NameCat = item.nameCategory,
                    Image = item.imageCat, 
                    Relation = rel
                };
                list.Add(cat);
            }

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!String.IsNullOrEmpty((searchString)))
            {
                //heroesT = heroesT.Where(s => s.nameHero == searchString).ToList();
                list = list.Where(s => s.NameCat.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(d => d.NameCat).ToList();
                    break;
                default:
                    list = list.OrderBy(d => d.NameCat).ToList();
                    break;
            }

            return View(list);           

        }

        // GET: Categories/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORYs.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            CategoryViewModel vm = new CategoryViewModel();
            vm.Cat = cATEGORY;
            vm.CatsTr = db.SPP_CategoryTrans().Where(ct => ct.idCategory == id).ToList();

            //IList<SPP_CategoryTrans_Result> category = db.SPP_CategoryTrans().Where(ct => ct.idCategory == id).ToList();

            return View(vm);
        }

        // GET: Categories/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            int lang = LanguageBL.CheckLanguageSession();
            CategoryViewModel vm = new CategoryViewModel
            {
                ListLang = LanguageBL.FindLanguageListWithoutUniversal(), 
                Categories = db.SPP_CategoryTransDistinct(lang).ToList()
            };
            return View(vm);
        }

        // POST: Categories/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //Check if nameHero have min  1 value
                IList<CATEGORY_TRANSLATION> catsT = vmodel.CategoriesTrans;
                bool nameCatIsValid = TranslationBL.CheckIfMinOneValued(catsT);
                if (nameCatIsValid)
                {
                    //Check if Cat exist by nameCategory before create
                    bool nameExist = TranslationBL.CheckIfNameExist(catsT);
                    if (!nameExist)
                    {
                        CATEGORY cat = new CATEGORY
                        {
                            imageCat = vmodel.Image
                        };
                        db.CATEGORYs.Add(cat);
                        db.SaveChanges();
                        //Add Translation
                        int id = cat.idCategory;
                        IList<CATEGORY_TRANSLATION> newcatsT = TranslationBL.VerifyIsUniversal(catsT, id);
                        db.CATEGORY_TRANSLATIONs.AddRange(newcatsT);
                        db.SaveChanges();

                        if (vmodel.Parent != null)
                        {
                            SUBCATEGORY subCat = new SUBCATEGORY {
                                parentCategory = vmodel.Parent.Value,
                                childCategory = cat.idCategory
                            };
                            db.SUBCATEGORYs.Add(subCat);
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //to do --> match the error with the name that causes the error
                        ViewBag.nameIsNotValid = "Le nom existe déjà, veuillez saisir un autre nom!";
                    }
                }
                else
                {
                    ViewBag.nameIsNotValid = "Veuillez saisir un nom!";
                }
            }
            int lang = LanguageBL.CheckLanguageSession();
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            vmodel.Categories = db.SPP_CategoryTransDistinct(lang).ToList();
            return View(vmodel);
        }

        // GET: Categories/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY category = db.CATEGORYs.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var translation = db.CATEGORY_TRANSLATIONs.Where(ct => ct.categoryId == id).ToList();
            CategoryViewModel vm = new CategoryViewModel();
            bool isUniversal = false;
            if (translation.Count() == 1)
            {
                isUniversal = true;
            }
            ViewBag.isUniversal = isUniversal;
            vm.ListLang = LanguageBL.FindLanguageListWithoutUniversal();

            vm.Cat = category;
            vm.CategoriesTrans = translation;

            return View(vm);
        }

        // POST: Categories/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vmodel.Cat).State = EntityState.Modified;
                IList<CATEGORY_TRANSLATION> catsT = TranslationBL.VerifyIsUniversal(vmodel.CategoriesTrans, vmodel.Cat.idCategory);
                foreach (var item in catsT)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Categories/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORYs.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATEGORY cATEGORY = db.CATEGORYs.Find(id);
            List<SUBCATEGORY> subCat = db.SUBCATEGORYs.Where(c => c.parentCategory == id || c.childCategory == id).ToList();
            db.SUBCATEGORYs.RemoveRange(subCat);
            db.CATEGORYs.Remove(cATEGORY);
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
