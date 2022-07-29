using MVCRestaurant27Tem2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCRestaurant27Tem2022.Controllers
{

    public class HomeController : Controller
    {
        RestaurantDBEntities db = new RestaurantDBEntities();
        [Authorize]
        public ActionResult Index()
        {

            return View(db.RTable.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}