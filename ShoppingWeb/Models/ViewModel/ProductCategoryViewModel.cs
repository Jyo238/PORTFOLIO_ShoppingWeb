using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
namespace ShoppingWeb.Models.ViewModel
{
    public class ProductCategoryViewModel
    {
        //定義你需要查詢的屬性
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public System.DateTime PublishDate { get; set; }
        public bool Status { get; set; }
        public Nullable<long> DefaultImageId { get; set; }
        public int Quantity { get; set; }
        public string DefaultImageURL { get; set; }
        public string CategoryName { get; set; }

        //public IQueryable<ProductCategoryViewModel> ProductCategoryViewModels { get; set; }
    }
}