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
    public class ReservationController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: Reservation
        public ActionResult Index()
        {
            var reservation = db.Reservation.Include(r => r.RTable);
            return View(reservation.ToList());
        }

        // GET: Reservation/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservation/Create
        public ActionResult Create()
        {
            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id_res,id_rtable,rdatetime,phone,rname,rsurname")] Reservation reservation)
        public ActionResult Create(Reservation reservation, string id)
        {
            if (ModelState.IsValid)
            {
                reservation.id_rtable = Convert.ToInt32(id);
                var tableInDb = db.RTable.FirstOrDefault(x => x.id_rtable == reservation.id_rtable);
                tableInDb.tstatus = "r";
                db.Reservation.Add(reservation);
                db.Entry(tableInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus", reservation.id_rtable);
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus", reservation.id_rtable);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_res,id_rtable,rdatetime,phone,rname,rsurname")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_rtable = new SelectList(db.RTable, "id_rtable", "tstatus", reservation.id_rtable);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
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
