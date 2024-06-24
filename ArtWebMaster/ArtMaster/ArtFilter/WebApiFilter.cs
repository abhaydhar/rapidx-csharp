using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ArtHandler.Repository;
using ArtHandler.Model;
using ArtHandler;

namespace ArtMaster.ArtFilter
{
    public class WebApiFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
            {                
                Log.LogTrace(new CustomTrace(actionContext.ActionArguments["userId"].ToString (), Constants.Session_User, Constants.New_Session ));
                var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Redirect, new Exception("No auth"));
                response.Headers.Add("NOAUTH", "0");
                actionContext.Response = response;
            }
        }

        //public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //{
        //    var objectContent = actionExecutedContext.Response.Content as ObjectContent;
        //    if (objectContent != null)
        //    {
        //        var type = objectContent.ObjectType; //type of the returned object
        //        var value = objectContent.Value; //holding the returned value
        //    }

        //    Debug.WriteLine("ACTION 1 DEBUG  OnActionExecuted Response " + actionExecutedContext.Response.StatusCode.ToString());
        //}
    }
}