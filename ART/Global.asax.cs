using ArtHandler.Interface;
using ArtHandler.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.Security;

namespace ART
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string SiteKey;

        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HttpRuntime.Cache.Insert("ClearOutputCache", true);
            
            bool result = SettingsRepository.InitializeApplicationSettings();
            if (result)
            {
                SettingsRepository.InitializePasswordSettings();
                SettingsRepository.InitializeLDAPSettings();
                SettingsRepository.InitializeArtValidationDBContext();
                SettingsRepository.InitializeLExcemptedOus();
            }
            MvcHandler.DisableMvcResponseHeader = true;
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            ArtHandler.Repository.Log.WriteFile("*****************" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString() + "****************" + Environment.NewLine + exception.Message.ToString() + Environment.NewLine + exception.StackTrace.ToString());
            Response.Clear();

            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";

            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        routeData.Values["action"] = "Error404";
                        //routeData.Values.Add("Error", "Error404");
                        break;
                    case 500:
                        // server error
                        routeData.Values["action"] = "Index";
                        //routeData.Values.Add("Error", "Index");
                        break;
                    default:
                        routeData.Values["action"] = "Index";
                        //routeData.Values.Add("Error", "Index");
                        break;
                }
                routeData.Values.Add("error", exception);


            }
            Server.ClearError();

            IController errorsController = new ART.Controllers.ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new System.Web.Routing.RequestContext(wrapper, routeData);
            wrapper.Response.ContentType = "text/html";
            errorsController.Execute(rc);
        }
        //protected void Session_Start(Object sender, EventArgs e)
        //{
        //    //SessionIDManager manager = new SessionIDManager();

        //    //string newID = manager.CreateSessionID(Context);
        //    //bool redirected = false;
        //    //bool isAdded = false;
        //    //manager.SaveSessionID(Context, newID, out redirected, out isAdded);

        //    //Session["init"] = 0;
        //}

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    string guid = Guid.NewGuid().ToString();
        //    Session["AuthToken"] = guid;
        //    Response.Cookies.Add(new HttpCookie("AuthToken", guid));  
        //}

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
        }
        //protected void Application_EndRequest(object sender, EventArgs e)
        //{
        //    if (Response.Cookies.Count > 0)
        //    {
        //        foreach (string s in Response.Cookies.AllKeys)
        //        {
        //            if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid")
        //            {
        //                Response.Cookies[s].Secure = true;
        //            }
        //        }
        //    }
        //}

    }
}
