using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanglaKhabarWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile() 
        {
            return View();  
        }

        public ActionResult MyOrders()
        {
            return View(); 
        }

        public ActionResult NewOrder()
        {
            return View();
        }


    }
}