using System.Web;
using System.Web.Optimization;

namespace ART
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.UseCdn = true;

            StyleBundle styleBundle = new StyleBundle("~/bundles/artstyle");
            styleBundle.Include("~/Css/bootstrap.css",
                                //"~/Css/bootstrap-theme.css",
                                "~/Css/customstyle.css",
                                "~/Css/responsive-style.css");

            bundles.Add(styleBundle);

            //StyleBundle styleFontBundle1 = new StyleBundle("~/bundles/artstylebootstrap", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css");
            //bundles.Add(styleFontBundle1);
            //styleFontBundle1.CdnPath = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css";

            StyleBundle styleFontBundle = new StyleBundle("~/bundles/font", "https://netdna.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css");
            bundles.Add(styleFontBundle);

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/ARTScript/corerest.js",
                      "~/Scripts/ARTScript/tether.js",
                      "~/Scripts/ARTScript/bootstrap.js",
                      "~/Scripts/ARTScript/custom.js",
                      "~/Scripts/ARTScript/layout.js"));
            
            var fontCdnPath = "https://fonts.googleapis.com/css?family=Roboto:400,900,700,500,300,100";
            bundles.Add(new StyleBundle("~/bundles/goofonts", fontCdnPath));

            BundleTable.EnableOptimizations = true;
        }
    }
}
