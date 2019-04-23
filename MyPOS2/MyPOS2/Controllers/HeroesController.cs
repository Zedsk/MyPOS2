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
using MyPOS2.Models.management;

namespace MyPOS2.Controllers
{
    [Authorize]
    public class HeroesController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Heroes
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index(string sortOrder, string searchString)
        {            
            int lang = LanguageBL.CheckLanguageSession();
            var heroesT = db.SPP_HeroesTransDistinct(lang).ToList();
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!String.IsNullOrEmpty((searchString)))
            {
                heroesT = heroesT.Where(s => s.nameHero.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    heroesT = heroesT.OrderByDescending(d => d.nameHero).ToList();
                    break;
                default:
                    heroesT = heroesT.OrderBy(d => d.nameHero).ToList();
                    break;
            }

            return View(heroesT);
        }

        // GET: Heroes/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IList<SPP_HeroesTrans_Result> heroes = db.SPP_HeroesTrans().Where(h => h.idHero == id).ToList();
            return View(heroes);
        }

        // GET: Heroes/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            HeroViewModel vm = new HeroViewModel
            {
                ListLang = LanguageBL.FindLanguageListWithoutUniversal()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HeroViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                //Check if nameHero have min  1 value
                IList<HERO_TRANSLATION> heroesT = vmodel.HeroesTrans;
                bool nameHeroIsValid = TranslationBL.CheckIfMinOneValued(heroesT);
                if (nameHeroIsValid)
                {
                    //Check if Hero exist by nameHero before create
                    bool nameExist = TranslationBL.CheckIfNameExist(heroesT);
                    if (!nameExist)
                    {
                        HERO hero = new HERO
                        {
                            imageHero = vmodel.ImageHero
                        };
                        db.HEROs.Add(hero);
                        db.SaveChanges();
                        int id = hero.idHero;
                        int count = heroesT.Count();
                        //Check if nameHero isUniversal
                        bool isUniversal = TranslationBL.CheckIfUniversal(heroesT);
                        if (isUniversal)
                        {
                            for (int i = 0; i < heroesT.Count(); i++)
                            {
                                if (heroesT[i].nameHero != null)
                                {
                                    heroesT[i].heroId = id;
                                    //change language with universal
                                    heroesT[i].languageId = LanguageBL.FindIdLanguageByShortForm("all");
                                }
                                else
                                {
                                    heroesT.Remove(heroesT[i]);
                                    i--;
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in heroesT)
                            {
                                item.heroId = id;
                            }
                        }
                        db.HERO_TRANSLATIONs.AddRange(heroesT);
                        db.SaveChanges();
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
            vmodel.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            return View(vmodel);
        }

        // GET: Heroes/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HERO hERO = db.HEROs.Find(id);
            if (hERO == null)
            {
                return HttpNotFound();
            }
            var translation = db.HERO_TRANSLATIONs.Where(ht => ht.heroId == id).ToList();
            var lang = db.LANGUAGESs.Count();
            HeroViewModel vm = new HeroViewModel();
            bool isUniversal = false;
            if (translation.Count() == 1)
            {
                isUniversal = true;
                HERO_TRANSLATION empty = new HERO_TRANSLATION
                {
                    heroId = 0,
                    languageId = 0,
                    nameHero = ""
                };
                for (int i = 0; i < (lang-2); i++)
                {
                    translation.Add(empty);
                }
            }
            ViewBag.isUniversal = isUniversal;
            vm.ListLang = LanguageBL.FindLanguageListWithoutUniversal();
            vm.Hero = hERO;
            vm.HeroesTrans = translation;
            return View(vm);
        }

        // POST: 
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HeroViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                bool isUniversal = TranslationBL.CheckIfUniversal(vmodel.HeroesTrans);
                db.Entry(vmodel.Hero).State = EntityState.Modified;
                foreach (var item in vmodel.HeroesTrans)
                {
                    if (item.nameHero != null)
                    {
                        if (isUniversal)
                        {
                            item.languageId = LanguageBL.FindIdLanguageByShortForm("all");
                            db.Entry(item).State = EntityState.Modified;
                        }
                        else
                        {
                            db.Entry(item).State = EntityState.Modified;
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vmodel);
        }

        // GET: Heroes/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HERO hERO = db.HEROs.Find(id);
            if (hERO == null)
            {
                return HttpNotFound();
            }
            return View(hERO);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<HERO_TRANSLATION> heroTrans = db.HERO_TRANSLATIONs.Where(h =>h.heroId == id).ToList();
            HERO hERO = db.HEROs.Find(id);
            db.HERO_TRANSLATIONs.RemoveRange(heroTrans);
            db.HEROs.Remove(hERO);
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
