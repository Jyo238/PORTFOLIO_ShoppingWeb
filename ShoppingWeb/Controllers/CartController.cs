using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWeb.Controllers
{
    public class CartController : Controller
    {


        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }

        // GET: Cart
        public ActionResult AddToCart(int id)
        {
            var CurrentCart = Models.Operation.GetCurrentCart();
            CurrentCart.AddProduct(id);


            return PartialView("_CartPartial");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var CurrentCart = Models.Operation.GetCurrentCart();
            CurrentCart.RemoveProduct(id);
                return PartialView("_CartPartial");
        }

        public ActionResult ClearCart()
        {
            var CurrentCart = Models.Operation.GetCurrentCart();
            CurrentCart.ClearCart();
            return PartialView("_CartPartial");
        }



    }
}