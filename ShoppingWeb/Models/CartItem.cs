using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Models
{


    [Serializable] //序列化
    public class CartItem
    {
        //編號
        public int Id { get; set; }
        //名稱
        public string Name { get; set; }
        //價格
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#######}")]
        public decimal Price { get; set; }
        //數量
        public int Quantity { get; set; }
        //小計
       [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#######}")]
        public decimal Amount
        {
            get 
            {
                return this.Price * this.Quantity;
            }
        }

        public string DefaultImgURL { get; set; }
    }
}