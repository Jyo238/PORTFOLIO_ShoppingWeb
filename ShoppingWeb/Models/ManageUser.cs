using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ShoppingWeb.Models
{
    public class ManageUser
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "{0}至少要{2}個字元", MinimumLength = 1)]
        [Display(Name="暱稱")]
        public string NickName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
        [Display(Name = "大頭照")]
        public string ImgUrl { get; set; }
    
    }
}