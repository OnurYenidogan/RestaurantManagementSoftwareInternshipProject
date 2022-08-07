using MVCRestaurant27Tem2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

                case "FD Type":
                    return RedirectToAction("Index", "fdType");
            }
            return View();
        }

        public ActionResult Table()
        {
            return View(db.RTable.ToList());
        }
    }
}