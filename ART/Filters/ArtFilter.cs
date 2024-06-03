using ArtHandler;
using ArtHandler.Model;
using ArtHandler.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;

namespace ART.Filters
{
    public class UserCultureFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie userlangCookie = System.Web.HttpContext.Current.Request.Cookies["ArtCookie"];
            if (userlangCookie != null)
            {
                string UserLang = System.Web.HttpContext.Current.Server.HtmlEncode(userlangCookie.Values[Constants.ARTUSERLANG]);
                userlangCookie.Secure = true;
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

            string isMobile = "No";
            //Check if browser is Mobile Browser or not
            if (System.Web.HttpContext.Current.Request.Browser.IsMobileDevice)
            {
                isMobile = "Yes";
            }

            Log.LogRequestorInfo(userId, sessionId, hostName, hostAddress, userAgent, urlReferer, isMobile);

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

            if (actionName == "Register" && controllerName == "User" )
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null || System.Web.HttpContext.Current.Session["UserRegStp2Complete"] == null || Convert.ToString(System.Web.HttpContext.Current.Session["UserRegStp2Complete"]) == "No")
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Login",
                    };
                }
            }

            if (actionName == "Reset" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null || System.Web.HttpContext.Current.Session["IsValidAnswer"] == null || Convert.ToBoolean(System.Web.HttpContext.Current.Session["IsValidAnswer"]) == false)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "ResetPassword"
                    }));
                }
            }
            if (actionName == "ResetPasswordOTP" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "ResetPassword"
                    }));
                }
            }
            if (actionName == "ResetPasswordAuthType" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "ResetPassword"
                    }));
                }
            }
            if (actionName == "ResetPasswordQuestions" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "ResetPassword"
                    }));
                }
            }
            if (actionName == "RegisterUserInfo" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Login",
                    };
                }
            }
            if (actionName == "AccountUnlockQuestions" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "AccountUnlock"
                    }));
                }
            }
            if (actionName == "AccountUnlockOTP" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "AccountUnlock"
                    }));
                }
            }
            if (actionName == "AccountUnlockAuthType" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "AccountUnlock"
                    }));
                }
            }
            if (actionName == "AccountUnlockQuestions" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "AccountUnlock"
                    }));
                }
            }
            if (actionName == "AccountUnlockOTP" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null || System.Web.HttpContext.Current.Session["UserAccUnlockStep2Comp"] == null || Convert.ToString(System.Web.HttpContext.Current.Session["UserAccUnlockStep2Comp"]) == "No")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "AccountUnlock"
                    }));
                }
            }

            if (actionName == "UnlockAccount" && controllerName == "User")
            {
                if (System.Web.HttpContext.Current.Session["UserId"] == null || System.Web.HttpContext.Current.Session["UserAccUnlockStep2Comp"] == null || Convert.ToString(System.Web.HttpContext.Current.Session["UserAccUnlockStep2Comp"]) == "No")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "AccountUnlock"
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