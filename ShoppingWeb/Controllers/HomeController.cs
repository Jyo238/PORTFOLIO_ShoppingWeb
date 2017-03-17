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

            ViewBag.ResultMessage = TempData["ResultMessage"];
            using (CartsEntities db = new CartsEntities())
            {
                var result = (from s in db.ProductSet select s).ToList();
                return View(result);
            }
        }


        public ActionResult Detail(int id)
        {
            using (CartsEntities db = new CartsEntities())
            {
                var result = (from s in db.ProductSet where s.Id == id select s).FirstOrDefault();
                if (result == default(Product))
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddComment(int id, string Content)
        { 
            //取得使用者Id
            var UserId = HttpContext.User.Identity.Name;
            var currentDateTime = DateTime.Now;

            var comment = new ProductCommet()
            {
                ProductId=id,
                UserId=UserId,
                Content=Content,
                CreateDate=currentDateTime,
               
            };

            using (CartsEntities db = new CartsEntities())
            {
                db.ProductCommets.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Detail",new {id = id });

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