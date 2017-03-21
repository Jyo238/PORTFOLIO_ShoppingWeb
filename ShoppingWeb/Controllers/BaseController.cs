using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWeb.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected bool isPicture(string fileName)
        {
            string extensionName = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var reg = new Regex("^(gif|png|jpg|bmp)$", RegexOptions.IgnoreCase);
            return reg.IsMatch(extensionName);
        }

        protected Image IsImage(HttpPostedFileBase photofile)
        {
            try
            {
                Image img = Image.FromStream(photofile.InputStream);
                return img;
            }
            catch
            {
                return null;
            }
        }
    }
}