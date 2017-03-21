using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Models
{
    [MetadataType(typeof(CategoryMD))]
    public partial class Category
    {
        public class CategoryMD
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }
            [Display(Name = "產品名稱")]
            [Required]
            [StringLength(50)]
            public string Name { get; set; }

            //public List<Product> ProductList { get; set; }

        }



        public static string CategoryName { get; set; }
    }
}