using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Models.Partials
{
     [MetadataType(typeof(ProductCommetMD))]
    public partial class ProductCommet
    {
         public class ProductCommetMD
         {
             public int Id { get; set; }
             public string UserId { get; set; }
             public string Content { get; set; }
             public System.DateTime CreateDate { get; set; }
             public int ProductId { get; set; }
             public int Stars { get; set; }
             public string ImgUrl { get; set; }
         }
    }
}