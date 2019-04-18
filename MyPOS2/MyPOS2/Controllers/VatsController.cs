﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPOS2.BL;
using MyPOS2.Data.Entity;

namespace MyPOS2.Controllers
{
    [Authorize]
    public class VatsController : Controller
    {
        private Pos1Entities db = new Pos1Entities();

        // GET: Vats
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Index()
        {
            //to do --> find setting message
            string nameSetting = "Vat";
            string vatSetting = SettingBL.FindSettingValueByName(nameSetting);
            var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
            var value = Decimal.Parse(vatSetting, numberFormatInfo);
            ViewBag.vatSetting = value;
            return View(db.VATs.ToList());
        }

        // GET: Vats/Details/5
        //[Authorize(Roles = "admin")]
        //[Authorize(Roles = "manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAT vAT = db.VATs.Find(id);
            if (vAT == null)
            {
                return HttpNotFound();
            }
            return View(vAT);
        }

        // GET: Vats/Create
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVat,appliedVat")] VAT vAT)
        {
            if (ModelState.IsValid)
            {
                db.VATs.Add(vAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vAT);
        }

        // GET: Vats/Edit/5
        //[Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAT vAT = db.VATs.Find(id);
            if (vAT == null)
            {
                return HttpNotFound();
            }
            return View(vAT);
        }

        // POST: Vats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVat,appliedVat")] VAT vAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vAT);
        }

        // GET: Vats/Delete/5
        //[Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAT vAT = db.VATs.Find(id);
            if (vAT == null)
            {
                return HttpNotFound();
            }
            return View(vAT);
        }

        // POST: Vats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VAT vAT = db.VATs.Find(id);
            db.VATs.Remove(vAT);
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
