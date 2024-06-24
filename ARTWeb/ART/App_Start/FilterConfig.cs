using ART.Filters;
using System.Web;
using System.Web.Mvc;

namespace ART
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new HandleAntiforgeryTokenErrorAttribute() { ExceptionType = typeof(HttpAntiForgeryException) });
            //filters.Add(new HandleAntiforgeryTokenErrorAttribute());
            //filters.Add(new UserCultureFilter());
            //filters.Add(new SessionFilter());
        }
    }
}
