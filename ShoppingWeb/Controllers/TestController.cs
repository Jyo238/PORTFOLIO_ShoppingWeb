using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWeb.Models;
namespace ShoppingWeb.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetCart()
        {
            //取得目前購物車
            var cart = Models.Operation.GetCurrentCart();
            
            cart.AddProduct(1);
            //回傳購物車的商品總價
            return Content(string.Format("目前購物車共{0}元", cart.TotoalAmount));
        }


    }
}