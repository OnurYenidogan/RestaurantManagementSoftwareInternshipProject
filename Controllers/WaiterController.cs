using MVCRestaurant27Tem2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCRestaurant27Tem2022.Controllers
{
    [Authorize(Users = "admin")]//bu sayfaya sadece admin adlı kullanıcı girebiliyor
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
                var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == newWaiter.Wnick);
                if (userInDb != null)
                {
                    ViewBag.Mesaj = "This username has already been taken";
                }
                else
                {
                    db.Waiter.Add(newWaiter);
                    db.SaveChanges();
                    ViewBag.Mesaj = "Account has been successfully created";
                }
            }
            return View();
        }
    }
}