using ShoppingWeb.Models;
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


        /// <summary>
        /// 平均分數吧?忘了
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       protected static decimal StarGet(int id)
        {
            decimal starsum = 0.0m;
            decimal starcount = 0.0m;
            decimal StarRating = 0.0m;
            using (CartsEntities db = new CartsEntities())
            {

                starcount = (from s in db.ProductCommets where s.ProductId == id select s.Stars).Count();
                if (starcount != 0)
                {
                    starsum = (from s in db.ProductCommets where s.ProductId == id select s.Stars).Sum();
                    StarRating = Math.Round(starsum / starcount, 1);

                }

                return StarRating;

            }
        }


      protected static List<int> StarC(int id)
        {
            var cuttingstar = new List<int>();

            using (CartsEntities db = new CartsEntities())
            {
                for (int i = 5; i >= 0; i--)
                {
                    var star = (from s in db.ProductCommets where s.ProductId == id && s.Stars == i select s.Stars).Count();
                    cuttingstar.Add(star);
                }
            }
            return cuttingstar;
        }


    }
}