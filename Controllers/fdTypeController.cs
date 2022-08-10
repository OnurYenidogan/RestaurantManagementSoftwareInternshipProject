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
    public class fdTypeController : Controller
    {
        private RestaurantDBEntities db = new RestaurantDBEntities();

        // GET: fdType
        public ActionResult Index()
        {
            return View(db.fdType.ToList());
        }
        public ActionResult TypeSelect()
        {
            return View(db.fdType.ToList());
        }

        // GET: fdType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fdType fdType = db.fdType.Find(id);
            if (fdType == null)
            {
                return HttpNotFound();
            }
            return View(fdType);
        }

        // GET: fdType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fdType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_type,typeName")] fdType fdType)
        {
            if (ModelState.IsValid)
            {
                db.fdType.Add(fdType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fdType);
        }

        // GET: fdType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fdType fdType = db.fdType.Find(id);
            if (fdType == null)
            {
                return HttpNotFound();
            }
            return View(fdType);
        }

        // POST: fdType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_type,typeName")] fdType fdType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fdType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fdType);
        }

        // GET: fdType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fdType fdType = db.fdType.Find(id);
            if (fdType == null)
            {
                return HttpNotFound();
            }
            return View(fdType);
        }

        // POST: fdType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fdType fdType = db.fdType.Find(id);
            db.fdType.Remove(fdType);
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
