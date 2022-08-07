using System;
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
    public class BillCompletedController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: BillCompleted
        public ActionResult Index()
        {
            var billCompleted = db.BillCompleted.Include(b => b.RTable).Include(b => b.Waiter);
            return View(billCompleted.ToList());
        }

        // GET: BillCompleted/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillCompleted billCompleted = db.BillCompleted.Find(id);
            if (billCompleted == null)
            {
                return HttpNotFound();
            }
            return View(billCompleted);
        }

        // GET: BillCompleted/Create
        public ActionResult Create()
        {
            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus");
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick");
            return View();
        }

        // POST: BillCompleted/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_bill,Bsum,id_rtable,id_waiter,bdatetime")] BillCompleted billCompleted)
        {
            if (ModelState.IsValid)
            {
                db.BillCompleted.Add(billCompleted);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus", billCompleted.id_rtable);
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", billCompleted.id_waiter);
            return View(billCompleted);
        }

        // GET: BillCompleted/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillCompleted billCompleted = db.BillCompleted.Find(id);
            if (billCompleted == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus", billCompleted.id_rtable);
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", billCompleted.id_waiter);
            return View(billCompleted);
        }

        // POST: BillCompleted/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_bill,Bsum,id_rtable,id_waiter,bdatetime")] BillCompleted billCompleted)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billCompleted).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus", billCompleted.id_rtable);
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", billCompleted.id_waiter);
            return View(billCompleted);
        }

        // GET: BillCompleted/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillCompleted billCompleted = db.BillCompleted.Find(id);
            if (billCompleted == null)
            {
                return HttpNotFound();
            }
            return View(billCompleted);
        }

        // POST: BillCompleted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BillCompleted billCompleted = db.BillCompleted.Find(id);
            db.BillCompleted.Remove(billCompleted);
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
