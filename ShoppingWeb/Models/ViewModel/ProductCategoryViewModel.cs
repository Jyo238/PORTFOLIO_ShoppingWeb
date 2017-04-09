using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.ComponentModel.DataAnnotations;
namespace ShoppingWeb.Models.ViewModel
{
    public class ProductCategoryViewModel
    {
        //定義你需要查詢的屬性
        [Display(Name = "編號")]
        public int Id { get; set; }
        [Display(Name = "名稱")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "類別編號")]
        public int CategoryId { get; set; }
        [Display(Name = "價格")]
        [Range(0, 1000000, ErrorMessage = "{0}必須介於{1}到{2}之間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#######}")]
        public decimal Price { get; set; }
        [Display(Name = "發佈日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")] //日期格式化
        public System.DateTime PublishDate { get; set; }
        [Display(Name = "狀態")]
        public bool Status { get; set; }
        [Display(Name = "預設圖片編號")]
        public Nullable<long> DefaultImageId { get; set; }
        [Display(Name = "庫存")]
        public int Quantity { get; set; }
        [Display(Name = "商品圖")]
        public string DefaultImageURL { get; set; }
        [Display(Name = "類別")]
        public string CategoryName { get; set; }

        //public IQueryable<ProductCategoryViewModel> ProductCategoryViewModels { get; set; }
    }
}