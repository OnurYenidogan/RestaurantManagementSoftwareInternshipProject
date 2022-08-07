using MVCRestaurant27Tem2022.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MVCRestaurant27Tem2022.Controllers
{
    public class AdminPanelController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();
        // GET: AdminPanel
        public ActionResult Index(string submit)
        {
            switch (submit)
            {
                case "Table":
                    return RedirectToAction("Table");
                case "Categories":
                    return RedirectToAction("Index", "fdType");
                case "Menu Items":
                    return RedirectToAction("Index", "FoodDrink");
                case "Waiter Management":
                    return RedirectToAction("Index", "Waiter");
            }
            return View();
        }

        public ActionResult Table()
        {
            return View(db.RTable.ToList());
        }
        public ActionResult FreeTable(int? id)
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
        public ActionResult FreeTable(string submit, [Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                switch (submit)
                {
                    case "Remove":
                        rTable.tstatus = "x";
                        db.Entry(rTable).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Table");
                }

            }
            return View(rTable);
        }
        public ActionResult RemovedTable(int? id)
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
        public ActionResult RemovedTable(string submit, [Bind(Include = "id_rtable,tstatus")] RTable rTable)
        {
            if (ModelState.IsValid)
            {
                switch (submit)
                {
                    case "Bring it back":
                        rTable.tstatus = "f";
                        db.Entry(rTable).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Table");
                }

            }
            return View(rTable);
        }
    }
}