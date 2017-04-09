using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWeb.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult General(Exception exception)
        {
            return View("~/Shared/Error.cshtml");
        }

        public ActionResult PageNotFound()
        {
            return View("~/Shared/Error.cshtml");
        }

        public ActionResult Http403()
        {
            return View("~/Shared/Error.cshtml");
        }
    }
}