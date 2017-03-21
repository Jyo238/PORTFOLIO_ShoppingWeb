using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Models
{

    public class Board
    {

        
        public int Stars { get; set; }

        public decimal Amount
        {
            get 
            {
                return this.Stars;
             }
        }
       
        public static decimal StarGet(int id)
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

        public static List<int> StarC(int id)
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