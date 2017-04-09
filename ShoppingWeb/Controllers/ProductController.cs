using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWeb.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;
using ShoppingWeb.Models.ViewModel;
namespace ShoppingWeb.Controllers
{
    public class ProductController : Controller
    {
        
        // GET: Product
        public ActionResult Index(string q,int page = 1,int pagesize= 6)
        {
            //List實體化
           //List<Models.ViewModel.ProductCategoryViewModel> result = new List<Models.ViewModel.ProductCategoryViewModel>();

            //接收轉至的成功訊息
            ViewBag.ResultMessage = TempData["ResultMessage"];
            //搜尋欄是否為空或空白
            if (string.IsNullOrWhiteSpace(q))
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {

                    //db.Product和db.Category做inner join，select new Models.ViewModel.ProductCategoryViewModel
                   var result = (from s in db.ProductSet
                              join o in db.CategorySet on s.CategoryId equals o.Id
                              orderby s.Id
                              select new  ProductCategoryViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        CategoryId = o.Id,
                        CategoryName = o.Name,
                        DefaultImageId = s.DefaultImageId,
                        DefaultImageURL = s.DefaultImageURL,
                        Status = s.Status,
                        Description = s.Description,
                        Price = s.Price,
                        PublishDate = s.PublishDate,
                        Quantity = s.Quantity
                    });

                   return View(result.ToPagedList(page, pagesize));
                }

            }
            else
            {
                using (Models.CartsEntities db = new Models.CartsEntities())
                {
                    //db.Product和db.Category做inner join，select new Models.ViewModel.ProductCategoryViewModel
                 var result = (from s in db.ProductSet
                               join o in db.CategorySet on s.CategoryId equals o.Id
                               orderby s.Id
                              where s.Name.Contains(q)
                              select new Models.ViewModel.ProductCategoryViewModel
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  CategoryId = o.Id,
                                  CategoryName = o.Name,
                                  DefaultImageId = s.DefaultImageId,
                                  DefaultImageURL = s.DefaultImageURL,
                                  Status = s.Status,
                                  Description = s.Description,
                                  Price = s.Price,
                                  PublishDate = s.PublishDate,
                                  Quantity = s.Quantity
                              }); 

                   return View(result.ToPagedList(page, pagesize));
                }
                
            }
      
        }


        public ActionResult Browse(int Category,int page = 1,int pagesize= 6)
        {

            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.ProductSet where s.CategoryId == Category orderby s.Id  select s);

                return View(result.ToPagedList(page, pagesize));
            }


        }




        public ActionResult Create()
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
            var CN = (from o in db.CategorySet select o).ToList();
            ViewBag.CategoryId = new SelectList(CN, "Id", "Name");
                }
           
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
                    var CN = (from o in db.CategorySet select o).ToList();
                    ViewBag.CategoryId = new SelectList(CN, "Id", "Name");
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
                //抓Category的資料傳到Viewbag做DropDownList
                int CId = (from s in db.ProductSet where s.Id == Id select s.CategoryId).FirstOrDefault();
                var CN = (from o in db.CategorySet select o).ToList();
                ViewBag.CategoryId = new SelectList(CN, "Id", "Name", CId);

                if (result != default(Product)) //如果Result有抓到資料
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

        [HttpPost] //限定Post方法
        public ActionResult Edit(Product postback)
        {
           if(this.ModelState.IsValid)//判斷使用者資料是否正確
           {
            using(CartsEntities db = new CartsEntities())
            {

                //抓Category的資料傳到Viewbag做DropDownList
                int CId = (from s in db.ProductSet where s.Id == postback.Id select s.CategoryId).FirstOrDefault();
                var CN = (from o in db.CategorySet select o).ToList();
                ViewBag.CategoryId = new SelectList(CN, "Id", "Name", CId);
                //ProductSet內的Id = postback的Id 的值
                var result = (from s in db.ProductSet where s.Id == postback.Id select s).FirstOrDefault();
                //儲存使用者變更資料
                result.Name = postback.Name;
                result.Price = Math.Round(postback.Price,0);
                result.PublishDate=postback.PublishDate;
                result.Quantity=postback.Quantity;
                result.Status=postback.Status;
                result.CategoryId =postback.CategoryId ;
                result.DefaultImageId = postback.DefaultImageId;
                result.Description = postback.Description;
                result.DefaultImageURL = postback.DefaultImageURL;
                //ViewBag.CategoryId = new SelectList(db.CategorySet, "Id", "Name", (db.ProductSet.Find(postback.Id).CategoryId));
                //存檔
                db.SaveChanges();
                TempData["ResultMessage"] = String.Format("商品{0}已編輯成功",postback.Name);
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

        //留言管理
        public ActionResult Comments(int Id, int page = 1, int pagesize = 6)
        {
            ViewBag.ResultMessage = TempData["deleteMessage"];
            using (Models.CartsEntities db = new Models.CartsEntities())
            {

                var result = (from s in db.ProductCommets
                              where s.ProductId == Id
                              orderby s.Id
                              select s);

                return View(result.ToPagedList(page, pagesize));
            }

        }


        [HttpPost] //刪除方法為POST
        public ActionResult DeleteComments(int CommentsId)
        {

            using (CartsEntities db = new CartsEntities())
            {
                //ProductSet內的Id = 輸入的Id 的值
                var result = (from s in db.ProductCommets
                              where s.Id == CommentsId
                              select s).FirstOrDefault();

                if (result != default(ProductCommet)) //如果Result有抓到資料
                {
                    db.ProductCommets.Remove(result);
                    db.SaveChanges();


                    TempData["deleteMessage"] = String.Format("留言已刪除");
                    return RedirectToAction("Comments", new { Id = result.ProductId}); //回傳Result的View
                }
                else
                {
                    TempData["deleteMessage"] = "資料有誤，無法刪除，請重新操作";
                    return RedirectToAction("Comments", new { Id = result.ProductId});
                }
            }


        }


    }
            
}
    
