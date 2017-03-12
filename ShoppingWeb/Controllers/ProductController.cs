using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWeb.Models;
namespace ShoppingWeb.Controllers
{
    public class ProductController : Controller
    {
        //透過Entity Framework 讀取Carts資料表內新增的三筆資料
        // GET: Product
        public ActionResult Index()
        {
            //List實體化
            List<Models.Product> result = new List<Models.Product>();
            //接收轉至的成功訊息
            ViewBag.ResultMessage = TempData["ResultMessage"];

            using(Models.CartsEntities db = new Models.CartsEntities())
            {
                result = (from s in db.ProductSet select s).ToList();    
            }
            
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }
        //只有使用Post方法才可進入
        [HttpPost]
        public ActionResult Create(Product postback)
        {
            if (this.ModelState.IsValid)
            {
                //DB實體化
                using (CartsEntities db = new CartsEntities())
                {
                    //新增postback到資料
                    db.ProductSet.Add(postback);
                    //存檔
                    db.SaveChanges();
                    //成功訊息
                    TempData["ResultMessage"] = String.Format("商品{0}成功建立", postback.Name);
                    //轉至Product/Index
                    return RedirectToAction("Index");
                }
            }
             //如果資料有誤：失敗訊息
            ViewBag.ResultMessage = "資料有誤，請檢查";

            //停留在Create畫面
            return View(postback);
                 
        }


        public ActionResult Edit(int Id)
        {
            using (CartsEntities db = new CartsEntities())
            {
                //ProductSet內的Id = 輸入的Id 的值
                var result = (from s in db.ProductSet where s.Id == Id select s).FirstOrDefault();
                if (result != default(Product)) //如果Result有抓到資料
                {
                    return View(result); //回傳Result的View
                }
                else
                {
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost] //限定Post方法
        public ActionResult Edit(Product postback)
        {
           if(this.ModelState.IsValid)//判斷使用者資料是否正確
           {
            using(CartsEntities db = new CartsEntities())
            {
                
                //ProductSet內的Id = postback的Id 的值
                var result = (from s in db.ProductSet where s.Id == postback.Id select s).FirstOrDefault();
                //儲存使用者變更資料
                result.Name = postback.Name;
                result.Price =postback.Price;
                result.PublishDate=postback.PublishDate;
                result.Quantity=postback.Quantity;
                result.Status=postback.Status;
                result.CategoryId = postback.CategoryId;
                result.DefaultImageId = postback.DefaultImageId;
                result.Description = postback.Description;
                result.DefaultImageURL = postback.DefaultImageURL;
                //存檔
                db.SaveChanges();
                TempData["resultMessage"] = String.Format("商品{0}已編輯成功",postback.Name);
                return RedirectToAction("Index");
                

            }
           }
           else
           {
            return View(postback);
           }

        }

       



        [HttpPost, ActionName("Delete")] //刪除方法為POST
        public ActionResult Delete(int Id)
        {
                    
            using (CartsEntities db = new CartsEntities())
            {           
                //ProductSet內的Id = 輸入的Id 的值
                var result = (from s in db.ProductSet where s.Id == Id select s).FirstOrDefault();
                if (result != default(Product)) //如果Result有抓到資料
                {
                    db.ProductSet.Remove(result);
                    db.SaveChanges();


                    TempData["deleteMessage"] = String.Format("商品{0}已刪除", result.Name);
                    return RedirectToAction("Index"); //回傳Result的View
                }
                else
                {
                    TempData["deleteMessage"] = "資料有誤，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }
            }

            
        }


        }
            
}
    
