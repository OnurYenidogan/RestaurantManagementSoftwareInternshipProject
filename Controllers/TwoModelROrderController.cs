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
    public class TwoModelROrderController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: TwoModelROrder
        public ActionResult Index()
        {
            var rOrder = db.ROrder.Include(r => r.Bill).Include(r => r.FoodDrink).Include(r => r.Waiter);
            return View(rOrder.ToList());
        }

        // GET: TwoModelROrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrder rOrder = db.ROrder.Find(id);
            if (rOrder == null)
            {
                return HttpNotFound();
            }
            return View(rOrder);
        }

        // GET: TwoModelROrder/Create
        public ActionResult Create()
        {
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill");
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname");
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick");
            return View();
        }

        // POST: TwoModelROrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,id_FD,id_bill,id_waiter,odatetime")] ROrder rOrder)
        {
            if (ModelState.IsValid)
            {
                db.ROrder.Add(rOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            return View(rOrder);
        }

        // GET: TwoModelROrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrder rOrder = db.ROrder.Find(id);
            if (rOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            return View(rOrder);
        }

        // POST: TwoModelROrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,id_FD,id_bill,id_waiter,odatetime")] ROrder rOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            return View(rOrder);
        }

        // GET: TwoModelROrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrder rOrder = db.ROrder.Find(id);
            if (rOrder == null)
            {
                return HttpNotFound();
            }
            return View(rOrder);
        }

        // POST: TwoModelROrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ROrder rOrder = db.ROrder.Find(id);
            db.ROrder.Remove(rOrder);
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
