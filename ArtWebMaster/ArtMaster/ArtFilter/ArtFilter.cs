using ArtHandler;
using ArtHandler.Model;
using ArtHandler.Repository;
using System;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;

namespace ArtMaster.Filters
{
    public class UserCultureFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie userlangCookie = System.Web.HttpContext.Current.Request.Cookies["ArtCookie"];
            if (userlangCookie != null)
            {
                string UserLang = System.Web.HttpContext.Current.Server.HtmlEncode(userlangCookie.Values[Constants.ARTUSERLANG]);
                Art.Culture = CultureInfo.GetCultureInfo(UserLang);
                System.Web.HttpContext.Current.Session[Constants.ARTUSERLANG] = UserLang;
            }
            else
            {
                Art.Culture = CultureInfo.GetCultureInfo(Singleton.Instance.ClientSessionID.Default_Lang);
                System.Web.HttpContext.Current.Session[Constants.ARTUSERLANG] = Singleton.Instance.ClientSessionID.Default_Lang;
            }
            HttpContextBase httpContext = filterContext.HttpContext;
            httpContext.Response.AddCacheItemDependency("ClearOutputCache");

            string CACHE_FILE_DEPENDENCY = System.Web.HttpContext.Current.Server.MapPath("~/Cache.txt");
            System.Web.HttpContext.Current.Response.AddCacheDependency(new CacheDependency(CACHE_FILE_DEPENDENCY));

            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RequestorLogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string hostName = Convert.ToString(System.Web.HttpContext.Current.Request.UserHostName);
            string hostAddress = Convert.ToString(System.Web.HttpContext.Current.Request.UserHostAddress);
            string userAgent = Convert.ToString(System.Web.HttpContext.Current.Request.UserAgent);
            string urlReferer = Convert.ToString(System.Web.HttpContext.Current.Request.UrlReferrer);
            string browser = Convert.ToString(System.Web.HttpContext.Current.Request.Browser);
            string userId = System.Web.HttpContext.Current.Session["UserId"] != null ? System.Web.HttpContext.Current.Session["UserId"].ToString() : " - NA -";
            string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            Log.LogRequestorInfo(userId, sessionId, hostName, hostAddress, userAgent, urlReferer);

            base.OnActionExecuting(filterContext);
        }
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (actionName == "EmployeeSearch" && controllerName == "Reports")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Login"
                    }));
                }
            }
            if (actionName == "Dashboard" && controllerName == "Reports")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Login"
                    }));
                }
            }
            if (actionName == "GSDView" && controllerName == "Reports")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Login"
                    }));
                }
            }
            

            base.OnActionExecuting(filterContext);
        }
    }
    public class HandleAntiforgeryTokenErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { action = "HomePage", controller = "Home" }));
        }
    }


}