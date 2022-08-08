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
            else if (Convert.ToString(rTable.tstatus) == "x")
            {
                return RedirectToAction("Removed/" + rTable.id_rtable);
            }
            return View(rTable);
        }

        // GET: RTable/Create
        

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
            ViewBag.Title = "#"+id;
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
            return View(rTable);
        }
        // POST: RTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Free( string submit, [Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                switch (submit)
                {
                    case "make seated":
                        var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == User.Identity.Name);
                        int Wid;
                        Wid = userInDb.id_waiter;
                        Bill newbill = new Bill();
                        newbill.id_rtable = rTable.id_rtable;
                        newbill.id_waiter = Convert.ToInt32(Wid); /*for now anyway it assignes admin*/
                        newbill.bdatetime = DateTime.Now;
                        newbill.Bsum = 0;
                        rTable.tstatus = "s";
                        db.Entry(rTable).State = EntityState.Modified;
                        db.SaveChanges();
                        db.Bill.Add(newbill);
                        db.SaveChanges();
                        //ViewBag.Mesaj = "successfully created";
                        return RedirectToAction("Index");
                    case "Reservation":
                        return RedirectToAction("Create/" + rTable.id_rtable,"Reservation");

                }

            }
            return View(rTable);
        }

        public ActionResult Removed(int? id)
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
            ViewBag.BillSum =  Convert.ToDecimal(billInDb.Bsum) + "₺";
            var orderInDb = db.ROrder.FirstOrDefault(a => a.id_bill == billInDb.id_bill);
            if (orderInDb != null)
            {
                ViewBag.Error = "Before checkout there shouldn't be any incomplete order. Complete or cancel any incomplete order for this table before checkout.";
            }

                return View(rTable);
        }
        // POST: RTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seated(string submit, [Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                switch (submit)
                {
                    case "order":
                        return RedirectToAction("Create/"+ rTable.id_rtable,"ROrder");
                    case "Checkout":
                        var billInDb = db.Bill.FirstOrDefault(x => x.id_rtable == rTable.id_rtable);
                        var orderInDb= db.ROrder.FirstOrDefault(a => a.id_bill == billInDb.id_bill);
                        if (orderInDb == null)
                        {
                            var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == User.Identity.Name);
                            int Wid;
                            Wid = userInDb.id_waiter;
                            //FoodDrink foodDrink = db.FoodDrink.Find(id);
                            rTable.tstatus = "f";
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
                            db.Entry(rTable).State = EntityState.Modified;
                            //db.Entry(billInDb).State = EntityState.Modified;
                            db.SaveChanges();
                            //billInDb = await db.ROrder.FindAsync(id);
                            //await db.SaveChangesAsync();
                            //ViewBag.Mesaj = "successfully created";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Seated/" + rTable.id_rtable);
                        }
                }
            }
            return View(rTable);
        }
        public ActionResult Reserved(int? id)
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
            return View(rTable);
        }
        // POST: RTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserved([Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == User.Identity.Name);
                var ReservationInDb = db.Reservation.FirstOrDefault(y => y.id_rtable == rTable.id_rtable);
                ReservationHistory resHis = new ReservationHistory();
                resHis.id_rtable = ReservationInDb.id_rtable;
                resHis.rdatetime = ReservationInDb.rdatetime;
                resHis.phone = ReservationInDb.phone;
                resHis.rname = ReservationInDb.rname;
                resHis.rsurname = ReservationInDb.rsurname;
                db.ReservationHistory.Add(resHis);
                db.SaveChanges();
                db.Reservation.Remove(ReservationInDb);
                db.SaveChanges();
                int Wid;
                Wid = userInDb.id_waiter;
                rTable.tstatus = "f";
                db.Entry(rTable).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.Mesaj = "successfully created";
                return RedirectToAction("Index");
            }
            return View(rTable);
        }
    }
}
