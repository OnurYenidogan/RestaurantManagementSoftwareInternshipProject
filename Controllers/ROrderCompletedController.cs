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
        public ActionResult Seated(int? id)
        {
            ViewBag.Title = "#" + id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RTable rTable = db.RTable.Find(id);
            if (rTable == null)
            {
                return HttpNotFound();
            }
            else if (rTable.tstatus == "x")
            {
                return RedirectToAction("Removed/" + rTable.id_rtable);
            }
            var billInDb = db.Bill.FirstOrDefault(x => x.id_rtable == rTable.id_rtable);
            Session["billId"] = billInDb.id_bill;
            decimal sum = billInDb.Bsum;
            sum = Math.Round(sum, 2);
            ViewBag.BillSum = sum + "₺";
            var orderInDb = db.ROrder.FirstOrDefault(a => a.id_bill == billInDb.id_bill);
            if (orderInDb != null)
            {
                ViewBag.Error = "Before checkout there shouldn't be any incomplete order. Complete or cancel any incomplete order for this table before checkout.";
            }

            return View(db.ROrderCompleted.ToList());
        }
        // POST: RTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seated(string submit,  int id)
        {
            if (ModelState.IsValid)
            {
                switch (submit)
                {
                    case "order":
                        Session["tableid"] = id;
                        return RedirectToAction("ItemSelect/" + id, "FoodDrink");
                    case "Checkout":
                        var billInDb = db.Bill.FirstOrDefault(x => x.id_rtable == id);
                        var tableInDb = db.RTable.FirstOrDefault(x => x.id_rtable == id);
                        var orderInDb = db.ROrder.FirstOrDefault(a => a.id_bill == billInDb.id_bill);
                        if (orderInDb == null)
                        {
                            var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == User.Identity.Name);
                            int Wid;
                            Wid = userInDb.id_waiter;
                            //FoodDrink foodDrink = db.FoodDrink.Find(id);
                            tableInDb.tstatus = "f";
                            BillCompleted newbillComp = new BillCompleted();
                            newbillComp.Bsum = billInDb.Bsum;
                            newbillComp.id_rtable = billInDb.id_rtable;
                            newbillComp.id_waiter = billInDb.id_waiter;
                            newbillComp.bdatetime = billInDb.bdatetime;
                            int dltID = Convert.ToInt32(billInDb.id_bill);
                            Bill deletebill = db.Bill.Find(dltID);
                            db.Bill.Remove(deletebill);
                            db.SaveChanges();
                            //billInDb.ispaid = true;
                            db.BillCompleted.Add(newbillComp);
                            db.Entry(tableInDb).State = EntityState.Modified;
                            //db.Entry(billInDb).State = EntityState.Modified;
                            db.SaveChanges();
                            //billInDb = await db.ROrder.FindAsync(id);
                            //await db.SaveChangesAsync();
                            //ViewBag.Mesaj = "successfully created";
                            return RedirectToAction("Index","RTable");
                        }
                        else
                        {
                            return RedirectToAction("Seated/" + tableInDb.id_rtable);
                        }
                }
            }
            return View();
        }
    }
}
