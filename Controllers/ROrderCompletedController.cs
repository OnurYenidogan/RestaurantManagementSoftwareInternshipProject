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
    public class ROrderCompletedController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: ROrderCompleted
        public ActionResult Index()
        {
            return View(db.ROrderCompleted.ToList());
        }

        // GET: ROrderCompleted/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrderCompleted rOrderCompleted = db.ROrderCompleted.Find(id);
            if (rOrderCompleted == null)
            {
                return HttpNotFound();
            }
            return View(rOrderCompleted);
        }

        // GET: ROrderCompleted/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ROrderCompleted/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,id_FD,id_bill,id_waiter,odatetime")] ROrderCompleted rOrderCompleted)
        {
            if (ModelState.IsValid)
            {
                db.ROrderCompleted.Add(rOrderCompleted);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rOrderCompleted);
        }

        // GET: ROrderCompleted/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrderCompleted rOrderCompleted = db.ROrderCompleted.Find(id);
            if (rOrderCompleted == null)
            {
                return HttpNotFound();
            }
            return View(rOrderCompleted);
        }

        // POST: ROrderCompleted/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,id_FD,id_bill,id_waiter,odatetime")] ROrderCompleted rOrderCompleted)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOrderCompleted).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rOrderCompleted);
        }

        // GET: ROrderCompleted/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrderCompleted rOrderCompleted = db.ROrderCompleted.Find(id);
            if (rOrderCompleted == null)
            {
                return HttpNotFound();
            }
            return View(rOrderCompleted);
        }

        // POST: ROrderCompleted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ROrderCompleted rOrderCompleted = db.ROrderCompleted.Find(id);
            db.ROrderCompleted.Remove(rOrderCompleted);
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
