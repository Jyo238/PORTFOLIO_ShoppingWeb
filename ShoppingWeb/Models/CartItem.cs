using System;
using System.Collections.Generic;
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
        public decimal Price { get; set; }
        //數量
        public int Quantity { get; set; }
        //小計
        public decimal Amount
        {
            get 
            {
                return this.Price * this.Quantity;
            }
        }
    }
}