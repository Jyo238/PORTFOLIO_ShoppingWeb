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
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Index(Models.OrderModel.Ship postback)
        {

            

            if (this.ModelState.IsValid)
            {
                var CurrentCart = Models.Operation.GetCurrentCart();
                var userId = HttpContext.User.Identity.Name;

                using (CartsEntities db = new CartsEntities())
                {

                    //建立Order物件
                    var order = new Order()
                    {

                        UserId = userId,
                        RecieverName = postback.RecieverName,
                        RecieverPhone = postback.RecieverPhone,
                        RecieverAddress = postback.RecieverAddress
                    };

                    //加入Orders資料表後儲存變更
                    db.OrderSet.Add(order);
                    db.SaveChanges();
                    //取得購物車中的OrderDetail物件
                    var OrderDetail  =  CurrentCart.ToOrderDetailList(order.Id);
                    //加入OrderDetail資料表後儲存變更
                    db.OrderDetailSet.AddRange(OrderDetail);
                    db.SaveChanges();
                }
                TempData["ResultMessage"] = String.Format("{0}你好，您的訂單已訂購成功", postback.RecieverName);
                return View("ThanksForBuy");
            }


            return View();
        }

        public ActionResult ThanksForBuy()
        {
            return View();
        }

        public ActionResult MyOrder(int page = 1, int pagesize = 10)
        {
            var UserId = HttpContext.User.Identity.Name;
            using (CartsEntities db = new CartsEntities())
            {
                var result = (from s in db.OrderSet orderby s.Id where s.UserId == UserId select s);
                return View(result.ToPagedList(page, pagesize));
                
            }
            
        }


        public ActionResult MyOrderDetails(int id)
        {
            //var UserId = HttpContext.User.Identity.Name;
            using (CartsEntities db = new CartsEntities())
            {
                var result = (from s in db.OrderDetailSet where s.OrderId == id select s).ToList();

                if (result.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                { 
                return View(result);
                }
            }

        }











        
    }
}