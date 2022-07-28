using MVCRestaurant27Tem2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRestaurant27Tem2022.Controllers
{
    public class WaiterController : Controller
    {
        RestaurantDBEntities db = new RestaurantDBEntities();
        public ActionResult Index()
        {
            return View(db.Waiter.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Waiter newWaiter)
        {
            if (ModelState.IsValid)
            {
                db.Waiter.Add(newWaiter);
                db.SaveChanges();
            }
            return View();
        }
    }
}