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
    public class TerminalsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Terminals
        public ActionResult Index()
        {
            var terminal = db.TERMINALs.Include(t => t.SHOP);
            return View(terminal.ToList());
        }

        // GET: Terminals/Details/5
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
        public ActionResult Create()
        {
            ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone");
            //ViewBag.nameT = "";
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

            ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", terminal.shopId);
            //ViewBag.nameT = "";
            return View(terminal);
        }

        // GET: Terminals/Edit/5
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
            ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", terminal.shopId);
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
            ViewBag.shopId = new SelectList(db.SHOPs, "idShop", "phone", terminal.shopId);
            return View(terminal);
        }

        // GET: Terminals/Delete/5
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
            //string T = System.Net.Dns.GetHostName();
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
