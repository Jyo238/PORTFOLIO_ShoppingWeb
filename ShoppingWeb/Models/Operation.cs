using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ShoppingWeb.Models;


namespace ShoppingWeb.Models
{
    public static class Operation
    {
        [WebMethod(EnableSession=true)]
        public static  Cart GetCurrentCart() // 取得目前Session中的cart物件
        {
            if (HttpContext.Current != null)
            {
                //如果Session["Cart"]不存在則新增一個新的cart物件
                if (HttpContext.Current.Session["Cart"] == null)
                {
                    var order = new Cart();
                    HttpContext.Current.Session["Cart"] = order;
                }
                return (Cart)HttpContext.Current.Session["Cart"];
            }
            else
            {
                throw new InvalidOperationException("Session為空");
            }
        }

    }
}