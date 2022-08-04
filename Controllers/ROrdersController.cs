﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCRestaurant27Tem2022.Models;

namespace MVCRestaurant27Tem2022.Controllers
{
    public class ROrdersController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: ROrders
        public async Task<ActionResult> Index()
        {
            var rOrder = db.ROrder.Include(r => r.Waiter).Include(r => r.FoodDrink).Include(r => r.Bill);
            return View(await rOrder.ToListAsync());
        }

        // GET: ROrders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrder rOrder = await db.ROrder.FindAsync(id);
            if (rOrder == null)
            {
                return HttpNotFound();
            }
            return View(rOrder);
        }

        // GET: ROrders/Create
        public ActionResult Create()
        {
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick");
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname");
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill");
            return View();
        }

        // POST: ROrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_order,id_FD,id_bill,id_waiter,odatetime")] ROrder rOrder)
        {
            if (ModelState.IsValid)
            {
                db.ROrder.Add(rOrder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            return View(rOrder);
        }

        // GET: ROrders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrder rOrder = await db.ROrder.FindAsync(id);
            if (rOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            return View(rOrder);
        }

        // POST: ROrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_order,id_FD,id_bill,id_waiter,odatetime")] ROrder rOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_waiter = new SelectList(db.Waiter, "id_waiter", "Wnick", rOrder.id_waiter);
            ViewBag.id_FD = new SelectList(db.FoodDrink, "id_FD", "FDname", rOrder.id_FD);
            ViewBag.id_bill = new SelectList(db.Bill, "id_bill", "id_bill", rOrder.id_bill);
            return View(rOrder);
        }

        // GET: ROrders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROrder rOrder = await db.ROrder.FindAsync(id);
            if (rOrder == null)
            {
                return HttpNotFound();
            }
            return View(rOrder);
        }

        // POST: ROrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ROrder rOrder = await db.ROrder.FindAsync(id);
            db.ROrder.Remove(rOrder);
            await db.SaveChangesAsync();
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
