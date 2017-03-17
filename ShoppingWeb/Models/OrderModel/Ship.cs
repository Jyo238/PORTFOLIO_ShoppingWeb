using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ShoppingWeb.Models.OrderModel
{
    
    public class Ship
    {
        //收貨人姓名
        [Required]
        [Display(Name = "收件人姓名")]
        [StringLength(30, ErrorMessage = "{0}的字元至少為{2}個字元", MinimumLength = 2)]
        public string RecieverName { get; set; }

        //收貨人電話
        [Required]
        [Display(Name = "收件人電話")]
        [Phone]
        //[StringLength(30, ErrorMessage = "{0}的字元至少為{2}個字元", MinimumLength = 2)]
        public string RecieverPhone { get; set; }

        
        //收貨人店址
        [Required]
        [Display(Name = "收件人地址")]
        [StringLength(30, ErrorMessage = "{0}的字元至少為{2}個字元", MinimumLength = 8)]
        public string RecieverAddress { get; set; }

    }
}