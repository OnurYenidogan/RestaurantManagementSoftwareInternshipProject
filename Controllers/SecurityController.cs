using MVCRestaurant27Tem2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCRestaurant27Tem2022.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        
        RestaurantDBEntities db = new RestaurantDBEntities();
        
        public ActionResult Login()
        {
            return View(new Waiter());
        }

        [HttpPost]
        public ActionResult Login(Waiter user)
        {
            var userInDb = db.Waiter.FirstOrDefault(x => x.Wnick == user.Wnick && x.wpassword == user.wpassword);
            if (userInDb !=null)
            {
                FormsAuthentication.SetAuthCookie(user.Wnick, false);
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Index", "RTable");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı adı ya da şifre hatalı.";
                return View();
            }  
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }

    }
}