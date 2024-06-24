using ArtHandler;
using ArtHandler.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ART.Controllers
{
    public class ResourceController : Controller
    {
        //
        // GET: /Resource/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Resource/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Resource/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Resource/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string urlReferrer = GetControllerName();
            string controllerName = urlReferrer.Split('$')[0];
            string actionName = urlReferrer.Split('$')[1];


            string language = collection["localize"].ToString();
            HttpRuntime.Cache.Insert("ClearOutputCache", true);

            HttpCookie Cookie = new HttpCookie("ArtCookie");
            
            Cookie.Values.Add(Constants.ARTUSERLANG, language);
            Cookie.Expires = DateTime.MaxValue; // never expire
            Cookie.Secure = true;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);

            Session[Constants.ARTUSERLANG] = language;

            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language.Split('-')[0]);
            Art.Culture = CultureInfo.GetCultureInfo(language);
            return RedirectToAction(actionName, controllerName);
        }

        public string GetControllerName()
        {
            // Split the url to url + query string
            var fullUrl = Request.UrlReferrer.ToString();
            var questionMarkIndex = fullUrl.IndexOf('?');
            string queryString = null;
            string url = fullUrl;
            if (questionMarkIndex != -1) // There is a QueryString
            {
                url = fullUrl.Substring(0, questionMarkIndex);
                queryString = fullUrl.Substring(questionMarkIndex + 1);
            }

            // Arranges
            var request = new HttpRequest(null, url, queryString);
            var response = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            // Extract the data    
            var values = routeData.Values;
            var controllerName = values["controller"];
            var actionName = values["action"];
            var areaName = values["area"];

            return controllerName + "$" + actionName;
        }

        //
        // GET: /Resource/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Resource/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Resource/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Resource/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
