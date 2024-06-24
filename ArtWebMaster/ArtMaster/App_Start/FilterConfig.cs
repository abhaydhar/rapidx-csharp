using ArtMaster.Filters;
using System.Web;
using System.Web.Mvc;

namespace ArtMaster
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
