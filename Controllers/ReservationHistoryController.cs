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
    public class ReservationHistoryController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: ReservationHistory
        public ActionResult Index()
        {
            return View(db.ReservationHistory.ToList());
        }

        // GET: ReservationHistory/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationHistory reservationHistory = db.ReservationHistory.Find(id);
            if (reservationHistory == null)
            {
                return HttpNotFound();
            }
            return View(reservationHistory);
        }

        // GET: ReservationHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_res,id_rtable,rdatetime,phone,rname,rsurname")] ReservationHistory reservationHistory)
        {
            if (ModelState.IsValid)
            {
                db.ReservationHistory.Add(reservationHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservationHistory);
        }

        // GET: ReservationHistory/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationHistory reservationHistory = db.ReservationHistory.Find(id);
            if (reservationHistory == null)
            {
                return HttpNotFound();
            }
            return View(reservationHistory);
        }

        // POST: ReservationHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_res,id_rtable,rdatetime,phone,rname,rsurname")] ReservationHistory reservationHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservationHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservationHistory);
        }

        // GET: ReservationHistory/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationHistory reservationHistory = db.ReservationHistory.Find(id);
            if (reservationHistory == null)
            {
                return HttpNotFound();
            }
            return View(reservationHistory);
        }

        // POST: ReservationHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ReservationHistory reservationHistory = db.ReservationHistory.Find(id);
            db.ReservationHistory.Remove(reservationHistory);
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
