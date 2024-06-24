using ArtHandler.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace ArtMaster
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HttpRuntime.Cache.Insert("ClearOutputCache", true);

            bool result = SettingsRepository.InitializeApplicationSettings();
            if (result)
            {
                SettingsRepository.InitializePasswordSettings();
                SettingsRepository.InitializeLDAPSettings();
                SettingsRepository.InitializeArtValidationDBContext();
            }
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

            IController errorsController = new ArtMaster.Controllers.ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new System.Web.Routing.RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }
        /// <summary>
        /// For Enabling Session in REST API - Not Recommended
        /// </summary>
        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        /// <summary>
        /// For Enabling Session in REST API - Not Recommended
        /// </summary>
        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(
                SessionStateBehavior.Required);
        }
    }
}
