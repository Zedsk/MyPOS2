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
    public class HeroesController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Heroes
        public ActionResult Index()
        {
            //return View(db.HEROs.ToList());
            if (Session["Language"] == null)
            {
                Session["Language"] = ConfigurationManager.AppSettings["Language"];
            }
            string language = Session["Language"].ToString();
            int lang;
            if (int.TryParse(language, out int codeL))
            {
                lang = codeL;
            }
            else
            {
                lang = LanguageBL.FindIdLanguageByShortForm(language);
            }
            //var heroesT = db.SPP_HeroesTrans().Where(h => h.languageId == lang).ToList();
            var heroesT = db.SPP_HeroesTransDistinct(lang).ToList();
            return View(heroesT);
        }

        // GET: Heroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //HERO hERO = db.HEROs.Find(id);
            //if (hERO == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(hERO);
            IList<SPP_HeroesTrans_Result> heroes = db.SPP_HeroesTrans().Where(h => h.idHero == id).ToList();
            return View(heroes);
        }

        // GET: Heroes/Create
        public ActionResult Create()
        {
            HeroViewModel vm = new HeroViewModel
            {
                ListLang = LanguageBL.FindLanguageListWithoutUniversal()
            };
            return View(vm);
        }

        //// POST: Heroes/Create
        //// Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        //// plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idHero,imageHero")] HERO hERO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HEROs.Add(hERO);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(hERO);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HeroViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                HERO hero = new HERO
                {
                    imageHero = vmodel.ImageHero
                };
                db.HEROs.Add(hero);
                db.SaveChanges();
                int id = hero.idHero;
                IList<HERO_TRANSLATION> heroesT = vmodel.HeroesTrans;
                int count = heroesT.Count();
                //IList<HERO_TRANSLATION> tempHeroesT = vmodel.HeroesTrans;
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
                    //foreach (var item in tempHeroesT)
                    //{
                    //    if (item.nameHero != null)
                    //    {
                    //        item.heroId = id;
                    //        //change language with universal
                    //        item.languageId = LanguageBL.FindIdLanguageByShortForm("all");
                    //    }
                    //    else
                    //    {
                    //        heroesT.Remove(item);
                    //    }
                    //}
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
            return View(vmodel);
        }

        // GET: Heroes/Edit/5
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
            return View(hERO);
        }

        // POST: Heroes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHero,imageHero")] HERO hERO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hERO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hERO);
        }

        // GET: Heroes/Delete/5
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
