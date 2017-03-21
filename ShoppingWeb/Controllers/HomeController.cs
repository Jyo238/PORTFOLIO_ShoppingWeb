using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWeb.Models;
using PagedList;
using PagedList.Mvc;

namespace ShoppingWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string q, int page = 1, int pagesize = 6)
        {

            ////List實體化
            //List<Models.Product> result = new List<Models.Product>();


            //using (Models.CartsEntities db = new Models.CartsEntities())
            //{
            //    //db.Product和db.Category做inner join，select new Models.ViewModel.ProductCategoryViewModel
            //    result = (from s in db.ProductSet select s).ToList();
            //}
            //return View(result);

           // 接收轉至的成功訊息
            ViewBag.ResultMessage = q;
            if (string.IsNullOrWhiteSpace(q))
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {

                   var result = (from s in db.ProductSet orderby s.Id select s);
                     return View(result.ToPagedList(page, pagesize));
                }            
            }
            else
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {

                    var result = (from s in db.ProductSet
                                  where s.Name.Contains(q)
                                  orderby s.Id
                                  select s);

                     return View(result.ToPagedList(page, pagesize));
                }
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
        public ActionResult AddComment(int id, string Content,int rating)
        { 
            //取得使用者Id
            var UserId = HttpContext.User.Identity.Name;
            
            var currentDateTime = DateTime.Now;
            using (UserEntities Userdb = new UserEntities())
            {
                var NickName = (from s in Userdb.AspNetUsers where s.UserName == UserId select s.Name).FirstOrDefault();
                var ImgUrl = (from s in Userdb.AspNetUsers where s.UserName == UserId select s.ImgUrl).FirstOrDefault();
            

            var comment = new ProductCommet()
            {

                ProductId = id,
                UserId = NickName,
                Content = Content,
                CreateDate = currentDateTime,
                ImgUrl = ImgUrl,
                Stars = rating

            };
                using (CartsEntities db = new CartsEntities())
                {
                    db.ProductCommets.Add(comment);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Detail",new {id = id });

        }
/*
        public ActionResult Search(string q)
        {

            //List實體化
            List<Models.Product> result = new List<Models.Product>();

            //接收轉至的成功訊息
            ViewBag.ResultMessage = q;
            if (string.IsNullOrWhiteSpace(q))
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    //db.Product和db.Category做inner join，select new Models.ViewModel.ProductCategoryViewModel
                    result = (from s in db.ProductSet select s).ToList();
                }
            }
            else
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    //db.Product和db.Category做inner join，select new Models.ViewModel.ProductCategoryViewModel
                    result = (from s in db.ProductSet
                              where s.Name.Contains(q)
                              select s).ToList();
                }
            }           
            return View(result);
        }
*/
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