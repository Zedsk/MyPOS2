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
    public class TerminalsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Terminals
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index(string sortOrder, string searchString)
        {
            var terminals = db.TERMINALs.Include(t => t.SHOP).ToList();

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!String.IsNullOrEmpty((searchString)))
            {
                terminals = terminals.Where(s => s.nameTerminal.ToLower().StartsWith(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    terminals = terminals.OrderByDescending(d => d.nameTerminal).ToList();
                    break;
                default:
                    terminals = terminals.OrderBy(d => d.nameTerminal).ToList();
                    break;
            }

            return View(terminals);
        }

        // GET: Terminals/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TERMINAL terminal = db.TERMINALs.Find(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }

        // GET: Terminals/Create
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            int lang = LanguageBL.CheckLanguageSession();
            int universal = db.LANGUAGESs.Where(l => l.shortForm == "all").Select(s => s.idLanguage).Single();
            ViewBag.shopId = new SelectList(db.SHOP_TRANSLATIONs.Include(t => t.SHOP).Where(s => s.languageId == lang || s.languageId == universal), "shopId", "nameShop");
            return View();
        }

        // POST: Terminals/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTerminal,nameTerminal,shopId")] TERMINAL terminal)
        {
            if (ModelState.IsValid)
            {
                db.TERMINALs.Add(terminal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int lang = LanguageBL.CheckLanguageSession();
            int universal = db.LANGUAGESs.Where(l => l.shortForm == "all").Select(s => s.idLanguage).Single();
            ViewBag.shopId = new SelectList(db.SHOP_TRANSLATIONs.Include(t => t.SHOP).Where(s => s.languageId == lang || s.languageId == universal), "shopId", "nameShop");
            return View(terminal);
        }

        // GET: Terminals/Edit/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TERMINAL terminal = db.TERMINALs.Find(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            int lang = LanguageBL.CheckLanguageSession();
            int universal = db.LANGUAGESs.Where(l => l.shortForm == "all").Select(s => s.idLanguage).Single();
            ViewBag.shopId = new SelectList(db.SHOP_TRANSLATIONs.Include(t => t.SHOP).Where(s => s.languageId == lang || s.languageId == universal), "shopId", "nameShop", terminal.shopId);
            return View(terminal);
        }

        // POST: Terminals/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTerminal,nameTerminal,shopId")] TERMINAL terminal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terminal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int lang = LanguageBL.CheckLanguageSession();
            int universal = db.LANGUAGESs.Where(l => l.shortForm == "all").Select(s => s.idLanguage).Single();
            ViewBag.shopId = new SelectList(db.SHOP_TRANSLATIONs.Include(t => t.SHOP).Where(s => s.languageId == lang || s.languageId == universal), "shopId", "nameShop", terminal.shopId);
            return View(terminal);
        }

        // GET: Terminals/Delete/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TERMINAL terminal = db.TERMINALs.Find(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }

        // POST: Terminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TERMINAL terminal = db.TERMINALs.Find(id);
            db.TERMINALs.Remove(terminal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SearchHostName()
        {
            string T = Dns.GetHostName();
            ViewBag.nameT = T;
            return PartialView("_PartialTerminalName");
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
