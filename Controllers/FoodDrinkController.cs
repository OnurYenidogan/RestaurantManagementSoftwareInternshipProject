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
    public class FoodDrinkController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: FoodDrink
        public ActionResult Index()
        {
            var foodDrink = db.FoodDrink.Include(f => f.fdType);
            return View(foodDrink.ToList());
        }
        public ActionResult ItemSelect()
        {
            ViewBag.Title = "Table " + Session["Tableid"];
            var foodDrink = db.FoodDrink.Include(f => f.fdType);
            return View(foodDrink.ToList());
        }

        // GET: FoodDrink/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDrink foodDrink = db.FoodDrink.Find(id);
            if (foodDrink == null)
            {
                return HttpNotFound();
            }
            return View(foodDrink);
        }

        // GET: FoodDrink/Create
        public ActionResult Create()
        {
            ViewBag.id_type = new SelectList(db.fdType, "id_type", "typeName");
            return View();
        }

        // POST: FoodDrink/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_FD,FDname,id_type,price,onmenu")] FoodDrink foodDrink)
        {
            if (ModelState.IsValid)
            {
                db.FoodDrink.Add(foodDrink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_type = new SelectList(db.fdType, "id_type", "typeName", foodDrink.id_type);
            return View(foodDrink);
        }

        // GET: FoodDrink/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDrink foodDrink = db.FoodDrink.Find(id);
            if (foodDrink == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_type = new SelectList(db.fdType, "id_type", "typeName", foodDrink.id_type);
            return View(foodDrink);
        }

        // POST: FoodDrink/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_FD,FDname,id_type,price,onmenu")] FoodDrink foodDrink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodDrink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_type = new SelectList(db.fdType, "id_type", "typeName", foodDrink.id_type);
            return View(foodDrink);
        }

        // GET: FoodDrink/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDrink foodDrink = db.FoodDrink.Find(id);
            if (foodDrink == null)
            {
                return HttpNotFound();
            }
            return View(foodDrink);
        }

        // POST: FoodDrink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FoodDrink foodDrink = db.FoodDrink.Find(id);
            db.FoodDrink.Remove(foodDrink);
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
