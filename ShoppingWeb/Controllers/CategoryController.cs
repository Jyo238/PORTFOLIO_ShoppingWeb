using ShoppingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingWeb.Controllers
{
    
    public class CategoryController : Controller
    {
        // GET: Category
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List <Models.Category> result = new List<Models.Category>();
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using(CartsEntities db = new CartsEntities())
            {
                result = (from s in db.CategorySet select s).ToList();
            }

            return View(result);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        //只有使用Post方法才可進入
        [HttpPost]
        public ActionResult Create(Category postback)
        {
            if (this.ModelState.IsValid)
            {
                //DB實體化
                using (CartsEntities db = new CartsEntities())
                {
                    //新增postback到資料
                    db.CategorySet.Add(postback);
                    //存檔
                    db.SaveChanges();
                    //成功訊息
                    TempData["ResultMessage"] = String.Format("類別{0}成功建立", postback.Name);
                    //轉至Product/Index
                    return RedirectToAction("Index");
                }
            }
            //如果資料有誤：失敗訊息
            ViewBag.ResultMessage = "資料有誤，請檢查";

            //停留在Create畫面
            return View(postback);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            using (CartsEntities db = new CartsEntities())
            {
                //ProductSet內的Id = 輸入的Id 的值
                var result = (from s in db.CategorySet where s.Id == Id select s).FirstOrDefault();
                if (result != default(Category)) //如果Result有抓到資料
                {
                    return View(result); //回傳Result的View
                }
                else
                {
                    TempData["ResultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost] //限定Post方法
        public ActionResult Edit(Category postback)
        {
            if (this.ModelState.IsValid)//判斷使用者資料是否正確
            {
                using (CartsEntities db = new CartsEntities())
                {

                    //ProductSet內的Id = postback的Id 的值
                    var result = (from s in db.CategorySet where s.Id == postback.Id select s).FirstOrDefault();
                    //儲存使用者變更資料
                    result.Name = postback.Name;
                    
                    //存檔
                    db.SaveChanges();
                    TempData["ResultMessage"] = String.Format("類別{0}已編輯成功", postback.Name);
                    return RedirectToAction("Index");


                }
            }
            else
            {
                return View(postback);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost] //刪除方法為POST
        public ActionResult Delete(int Id)
        {

            using (CartsEntities db = new CartsEntities())
            {
                //ProductSet內的Id = 輸入的Id 的值
                var result = (from s in db.CategorySet where s.Id == Id select s).FirstOrDefault();
                if (result != default(Category)) //如果Result有抓到資料
                {
                    db.CategorySet.Remove(result);
                    db.SaveChanges();


                    TempData["ResultMessage"] = String.Format("類別{0}已刪除", result.Name);
                    return RedirectToAction("Index"); //回傳Result的View
                }
                else
                {
                    TempData["ResultMessage"] = "資料有誤，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }
            }


        }


        [ChildActionOnly] //只能由子要求存取
        public ActionResult _CategoryMenu()
        {
            List<Models.Category> result = new List<Models.Category>();

            using (CartsEntities db = new CartsEntities())
            {
                result = (from s in db.CategorySet select s).ToList();
                
               
            }
            return PartialView("_CategoryMenu",result);
            //把DB資料撈出來傳到_CategoryMenu
        }



        


    }
}