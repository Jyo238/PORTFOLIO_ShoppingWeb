using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWeb.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.Caching;
namespace ShoppingWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUserController : BaseController
    {
        // GET: ManageUser
        public ActionResult Index(string q, int page = 1, int pagesize = 10)
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            ViewBag.DeleteMessage = TempData["DeleteMessage"];

            if (string.IsNullOrWhiteSpace(q))
            {

                using (UserEntities db = new UserEntities())
                { //抓取所有AspNetUsers中的資料，並且放入Models.ManageUser模型中
                    var result = (from s in db.AspNetUsers
                                  orderby s.Id
                                  select new ManageUser
                                      {
                                          Id = s.Id,
                                          NickName = s.Name,
                                          Email = s.Email,
                                          ImgUrl = s.ImgUrl
                                      }
                                 );
                    return View(result.ToPagedList(page, pagesize));
                }
            }
            else
            {
                using (UserEntities db = new UserEntities())
                { //抓取所有AspNetUsers中的資料，並且放入Models.ManageUser模型中
                    var result = (from s in db.AspNetUsers
                                  where s.Name.Contains(q)
                                  orderby s.Id
                                  select new ManageUser
                                  {
                                      Id = s.Id,
                                      NickName = s.Name,
                                      Email = s.Email,
                                      ImgUrl = s.ImgUrl
                                  }
                                 );
                    return View(result.ToPagedList(page, pagesize));
                }
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
                                  NickName = s.Name,
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
        public ActionResult Edit(Models.ManageUser postback, HttpPostedFileBase photoFile)
        {
            ViewBag.path = TempData["id"];
            //檔案名
            var fileName = "photo.jpg";
            //路徑
            var path = Path.Combine(Server.MapPath("~/FileUploads/" + postback.Id));
            //路徑加檔案名
            var pathName = Path.Combine(Server.MapPath("~/FileUploads/" + postback.Id), fileName);

            using (Models.UserEntities db = new Models.UserEntities())
            {
                TempData["id"] = postback.Id;

                if (photoFile != null)
                {
                    if (!isPicture(photoFile.FileName))
                    {
                      TempData["ErrorMessage"] = "您所上傳的檔案類型並不是圖片";
                      return RedirectToAction("Edit");
                    }

                    if (IsImage(photoFile) == null)
                    {
                       
                      TempData["ErrorMessage"] = "您所上傳的檔案內容並不是圖片";
                      return RedirectToAction("Edit");
                    }
                    if (photoFile.ContentLength > 0)
                    {
                        //資料夾不存在的話創一個
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        //有此檔名的話把他刪了
                        if (System.IO.File.Exists(pathName))
                        {
                            System.IO.File.Delete(pathName);
                        }

                        Image photo = Image.FromStream(photoFile.InputStream);
                        photo.Save(pathName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //photo.Save(@"D:\Newproject\ASP_Identity\ASP_Identity\FileUploads\" + id + @"\photo.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
                var result = (from s in db.AspNetUsers where s.Id == postback.Id select s).FirstOrDefault();
                if (result != default(Models.AspNetUsers))
                {
                    result.Name = postback.NickName;
                    result.Email = postback.Email;
                    result.ImgUrl = "~/FileUploads/" + postback.Id + "/photo.jpg";
                    db.SaveChanges();
                    //設定成功訊息

                   

                    TempData["ResultMessage"] = String.Format("使用者[{0}]成功編輯", postback.NickName);
                    return RedirectToAction("Index");
                }
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", postback.NickName);
            return RedirectToAction("Index");
        }



    }


}