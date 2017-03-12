using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWeb.Models;
namespace ShoppingWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (CartsEntities db = new CartsEntities())
            {
                var result = (from s in db.ProductSet select s).ToList();
                return View(result);
            }
        }

        //Home/Index2
        public ActionResult Index2()
        {
            return View();
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