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
    public class ROrderController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: ROrder
        public ActionResult Index()
        {
            var rOrder = db.ROrder.Include(r => r.Waiter).Include(r => r.FoodDrink).Include(r => r.Bill);
            return View(rOrder.ToList());
        }

        // GET: ROrder/Details/5
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

        // GET: ROrder/Create
        public ActionResult Create()
        {
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick");
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname");
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill");
            return View();
        }

        // POST: ROrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ROrder rOrder, string id , string id2, int numberOfOrders)
        {
            if (ModelState.IsValid)
            {
                int idInt = Convert.ToInt32(id);
                int id2Int = Convert.ToInt32(id2);
                var billInDb = db.Bill.FirstOrDefault(y => y.id_rtable == id2Int);
                //var FoodInDb = db.FoodDrink.FirstOrDefault(z => z.id_FD == rOrder.id_FD);
                rOrder.id_bill = billInDb.id_bill;
                var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == User.Identity.Name);
                int Wid;
                //billInDb.Bsum = billInDb.Bsum + FoodInDb.price;
                Wid = userInDb.id_waiter;
                rOrder.id_waiter = Wid;
                rOrder.odatetime = DateTime.Now;
                rOrder.id_FD = idInt;
                for (int i = 0; i < numberOfOrders; i++)
                {
                    db.ROrder.Add(rOrder);
                    db.SaveChanges();
                }
                //db.Entry(billInDb).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("ItemSelect/" + id, "FoodDrink");
            }
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            return View(rOrder);
        }

        // GET: ROrder/Edit/5
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
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            return View(rOrder);
        }

        // POST: ROrder/Edit/5
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
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            return View(rOrder);
        }

        // GET: ROrder/Delete/5
        public ActionResult Complete(int? id)
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

        // POST: ROrder/Delete/5
        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteConfirmed(int id)
        {
            ROrder rOrder = db.ROrder.Find(id);
            ROrderCompleted rOrderCompleted = new ROrderCompleted();
            int idInt = Convert.ToInt32(rOrder.id_bill);
            var billInDb = db.Bill.FirstOrDefault(y => y.id_bill == idInt);
            var FoodInDb = db.FoodDrink.FirstOrDefault(z => z.id_FD == rOrder.id_FD);
            billInDb.Bsum = billInDb.Bsum + FoodInDb.price;
            rOrderCompleted.id_FD = rOrder.id_FD;
            rOrderCompleted.id_bill = rOrder.id_bill;
            rOrderCompleted.id_waiter = rOrder.id_waiter;
            rOrderCompleted.odatetime = rOrder.odatetime;
            db.ROrderCompleted.Add(rOrderCompleted);
            db.SaveChanges();
            db.Entry(billInDb).State = EntityState.Modified;
            db.SaveChanges();
            db.ROrder.Remove(rOrder);
            db.SaveChanges();
            return RedirectToAction("Index");



            //ROrder rOrder = db.ROrder.Find(id);
            //db.ROrder.Remove(rOrder);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }
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

        // POST: ROrder/Delete/5
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
