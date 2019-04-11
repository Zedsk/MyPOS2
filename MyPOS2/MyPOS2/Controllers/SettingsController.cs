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
    public class SettingsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Settings
        public ActionResult Index()
        {
            return View(db.SETTINGs.ToList());
        }

        // GET: Settings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SETTING sETTING = db.SETTINGs.Find(id);
            if (sETTING == null)
            {
                return HttpNotFound();
            }
            return View(sETTING);
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSetting,nameSetting,valueSetting")] SETTING sETTING)
        {
            if (ModelState.IsValid)
            {
                db.SETTINGs.Add(sETTING);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sETTING);
        }

        // GET: Settings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SETTING sETTING = db.SETTINGs.Find(id);
            if (sETTING == null)
            {
                return HttpNotFound();
            }
            return View(sETTING);
        }

        // POST: Settings/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSetting,nameSetting,valueSetting")] SETTING setting)
        {
            if (ModelState.IsValid)
            {
                //check value setting
                bool edit = false;
                switch (setting.nameSetting)
                {
                    case "Vat":
                        if(decimal.Parse(setting.valueSetting) > 0 && decimal.Parse(setting.valueSetting) <= 1)
                        {
                            edit = true;
                        }
                        else
                        {
                            ViewBag.Error = "la valeur doit être comprise entre 0 et 1 => exemple: 0.21";
                        }
                        break;

                    case "Language":
                        var test = db.LANGUAGESs.Select(l => l.shortForm).ToList();
                        if (test.Contains(setting.valueSetting))
                        {
                            edit = true;
                        }
                        else
                        {
                            ViewBag.Error = "La valeur n'est pas correcte. Référez-vous au abréviation des langages dans le Menu Gestion -->  Langages";
                        }
                        break;
                    default:
                        ViewBag.Error = "La valeur n'est pas correcte.  Si le problème persiste, contactez l'administrateur";
                        break;
                }
                if (edit)
                {
                    db.Entry(setting).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(setting);
        }

        // GET: Settings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SETTING sETTING = db.SETTINGs.Find(id);
            if (sETTING == null)
            {
                return HttpNotFound();
            }
            return View(sETTING);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SETTING sETTING = db.SETTINGs.Find(id);
            db.SETTINGs.Remove(sETTING);
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
