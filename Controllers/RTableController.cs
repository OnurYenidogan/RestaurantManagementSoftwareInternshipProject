﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCRestaurant27Tem2022.Models;

namespace MVCRestaurant27Tem2022.Controllers
{
    public class RTableController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: RTable
        public ActionResult Index()
        {
            return View(db.RTable.ToList());
        }

        // GET: RTable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTable rTable = db.RTable.Find(id);
            if (rTable == null)
            {
                return HttpNotFound();
            }
            if (Convert.ToString(rTable.tstatus)=="f"){
                return RedirectToAction("Free/"+ rTable.id_rtable);
            }
            else if(Convert.ToString(rTable.tstatus) == "s"){
                return RedirectToAction("Seated/" + rTable.id_rtable);
            }
            else if (Convert.ToString(rTable.tstatus) == "r")
            {
                return RedirectToAction("Reserved/" + rTable.id_rtable);
            }
            return View(rTable);
        }

        // GET: RTable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                db.RTable.Add(rTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rTable);
        }

        // GET: RTable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTable rTable = db.RTable.Find(id);
            if (rTable == null)
            {
                return HttpNotFound();
            }
            return View(rTable);
        }

        // POST: RTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rTable);
        }

        // GET: RTable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTable rTable = db.RTable.Find(id);
            if (rTable == null)
            {
                return HttpNotFound();
            }
            return View(rTable);
        }

        // POST: RTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RTable rTable = db.RTable.Find(id);
            db.RTable.Remove(rTable);
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
        public ActionResult Free(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTable rTable = db.RTable.Find(id);
            if (rTable == null)
            {
                return HttpNotFound();
            }
            return View(rTable);
        }
        // POST: RTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Free([Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == User.Identity.Name);
                int Wid;
                Wid = userInDb.id_waiter;
                Bill newbill = new Bill();
                newbill.id_rtable = rTable.id_rtable;
                newbill.id_waiter =  Convert.ToInt32(Wid); /*for now anyway it assignes admin*/
                newbill.bdatetime = DateTime.Now;
                newbill.ispaid = false;
                rTable.tstatus = "s";
                db.Entry(rTable).State = EntityState.Modified;
                db.SaveChanges();
                db.Bill.Add(newbill);
                db.SaveChanges();
                //ViewBag.Mesaj = "successfully created";
                return RedirectToAction("Index");
            }
            return View(rTable);
        }
        public ActionResult Seated(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTable rTable = db.RTable.Find(id);
            if (rTable == null)
            {
                return HttpNotFound();
            }
            return View(rTable);
        }
        // POST: RTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seated([Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == User.Identity.Name);
                int Wid;
                Wid = userInDb.id_waiter;
                //FoodDrink foodDrink = db.FoodDrink.Find(id);
                var billInDb = db.Bill.FirstOrDefault(x => x.id_rtable == rTable.id_rtable && x.ispaid == false);
                rTable.tstatus = "f";
                billInDb.ispaid = true;
                db.Entry(rTable).State = EntityState.Modified;
                db.Entry(billInDb).State = EntityState.Modified;
                db.SaveChanges();
                db.SaveChanges();
                //ViewBag.Mesaj = "successfully created";
                return RedirectToAction("Index");
            }
            return View(rTable);
        }
    }
}
