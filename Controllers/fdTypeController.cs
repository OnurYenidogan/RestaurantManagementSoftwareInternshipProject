using MVCRestaurant27Tem2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRestaurant27Tem2022.Controllers
{
    public class fdTypeController : Controller 
    {
        RestaurantDBEntities db = new RestaurantDBEntities();
        public ActionResult Index()
        {
            return View(db.fdType.ToList());
        }
    }
}