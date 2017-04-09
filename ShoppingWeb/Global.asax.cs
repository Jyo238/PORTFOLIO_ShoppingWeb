using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShoppingWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ////準備一個HandleErrorAttribute
            //var handleError = new HandleErrorAttribute();
            //// 指定HandleError導向的頁面，要指定值  但會吃config的頁面
            //handleError.View = "/Home";
            //// 設定HandleError啟用
            //handleError.Order = 0;
            ////加到全域的filter中
            //GlobalFilters.Filters.Add(handleError);

        }
        protected void Application_Error()
    {
        var exception = Server.GetLastError();
        var httpException = exception as HttpException;
        Response.Clear();
        Server.ClearError();
        var routeData = new RouteData();
        routeData.Values["controller"] = "Error";
        routeData.Values["action"] = "Error";
       routeData.Values["exception"] = exception;
       Response.ContentType = "text/html";
       Response.StatusCode = 500;
       if (httpException != null)
       {
           Response.StatusCode = httpException.GetHttpCode();
           switch (Response.StatusCode)
           {
               case 404:
                   routeData.Values["action"] = "PageNotFound";
                   break;
               case 403:
                   routeData.Values["action"] = "Http403";
                   break;
           }
       }

       IController errorsController = new ShoppingWeb.Controllers.ErrorsController();
       var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
       errorsController.Execute(rc);
   }


        //void Application_Error(object sender, EventArgs e)
        //{
        //    // 發生未處理錯誤時執行的程式碼

        //    var error = Server.GetLastError();
        //    //LogHelper.WriteLog(error);
        //    Server.Transfer("~/Shared/Error.cshtml");
        //}
    }
}
