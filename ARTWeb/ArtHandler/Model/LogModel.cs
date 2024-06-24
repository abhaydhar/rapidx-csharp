using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ArtHandler.Model
{
    public class CustomException
    {
        public CustomException(string User, string Exmsg, string Stackmsg, string methodName)
        {
            if (User == null)
            {
                if (System.Web.HttpContext.Current.Session != null)
                    User = System.Web.HttpContext.Current.Session.SessionID;
            }

            if (System.Web.HttpContext.Current.Session != null)
                this.UserId = System.Web.HttpContext.Current.Session["UserId"] != null ? System.Web.HttpContext.Current.Session["UserId"].ToString() : User;
            else
                this.UserId = "- NA -";

            this.ExceptionMessage = Exmsg;
            this.StackTrace = Stackmsg;
            this.MethodName = methodName;
        }
        public string UserId { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string MethodName { get; set; }
    }
    public class CustomTrace
    {
        public CustomTrace(string user, string message, string statmsg)
        {
            this.UserId = user;
            this.Message = message;
            this.Status = statmsg;
            this.SessionId = HttpContext.Current.Session.SessionID;
        }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string SessionId { get; set; }
    }

}
