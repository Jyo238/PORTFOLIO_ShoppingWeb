using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWeb.Models;
namespace ShoppingWeb.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: ManageUser
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            ViewBag.DeleteMessage = TempData["DeleteMessage"];
            using (UserEntities db = new UserEntities())

            { //抓取所有AspNetUsers中的資料，並且放入Models.ManageUser模型中
                var result = (from s in db.AspNetUsers
                              select new ManageUser
                                  {
                                      Id = s.Id,
                                      Name = s.UserName,
                                      Email = s.Email
                                  }
                             ).ToList();
                return View(result);
            }     
        }


        [HttpPost] //刪除方法為POST
        public ActionResult Delete(string id)
        {

            using (UserEntities db = new UserEntities())
            {
                //ProductSet內的Id = 輸入的Id 的值
                var result = (from s in db.AspNetUsers                             
                              where s.Id == id
                              select s).FirstOrDefault();

                if (result != default(AspNetUsers)) //如果Result有抓到資料
                {
                    db.AspNetUsers.Remove(result);
                    db.SaveChanges();


                    TempData["DeleteMessage"] = String.Format("會員{0}已刪除", result.UserName);
                    return RedirectToAction("Index"); //回傳Result的View
                }
                else
                {
                    TempData["DeleteMessage"] = "資料有誤，無法刪除，請重新操作";
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult Edit(string id)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (
                    
                    from s in db.AspNetUsers
                              where s.Id == id
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  Name = s.UserName,
                                  Email = s.Email
                              }
                    
                    ).FirstOrDefault();
                if (result != default(Models.ManageUser))
                {
                    return View(result);
                }
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Models.ManageUser postback)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == postback.Id
                              select s).FirstOrDefault();
                if (result != default(Models.AspNetUsers))
                {
                    result.UserName = postback.Name;
                    result.Email = postback.Email;
                    db.SaveChanges();
                    //設定成功訊息
                    TempData["ResultMessage"] = String.Format("使用者[{0}]成功編輯", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", postback.Name);
            return RedirectToAction("Index");
        }




    }
}