using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShoppingWeb.Models
{
    [MetadataType(typeof(ProductMD))]
    public partial class Product
    {
        public class ProductMD
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }
            [Display(Name="產品名稱")]
            [Required]
            [StringLength(50)]
            public string Name { get; set; }
             [Display(Name = "描述")]
             [Required]
            public string Description { get; set; }
             [Display(Name = "類別")]
             [Required]
            public int CategoryId { get; set; }           
             [Display(Name = "價格")]
             [Required]
             [Range(0, 10000, ErrorMessage = "{0}必須介於{1}到{2}之間")]
            public decimal Price { get; set; }
             [Display(Name = "上傳日期")]
             [Required]
             [DataType(DataType.DateTime)]
             [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")] //日期格式化
            public System.DateTime PublishDate { get; set; }
             [Display(Name = "狀態")]
             [Required]
            public bool Status { get; set; }
             [Display(Name = "預設圖片編號")]
             [Required]
            public Nullable<long> DefaultImageId { get; set; }
             [Display(Name = "數量")]
             [Required]
             [Range(0, 10000, ErrorMessage = "{0}必須介於{1}到{2}之間")]
            public int Quantity { get; set; }

            


        }
        public virtual Category Category { get; set; }

    }
}