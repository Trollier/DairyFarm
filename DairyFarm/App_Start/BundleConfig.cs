using System.Web;
using System.Web.Optimization;

namespace DairyFarm
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/bootstrap.js",
                      "~/Content/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/morris-charts").Include(
                      "~/Content/Scripts/plugins/morris/raphael.js",
                      "~/Content/Scripts/plugins/morris/morris.js",
                      "~/Content/Scripts/plugins/morris/morris-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/flot-charts").Include(
                      "~/Content/Scripts/plugins/flot/jquery.flot.js",
                      "~/Content/Scripts/plugins/flot/jquery.flot.tooltip.js",
                      "~/Content/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Content/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Content/Scripts/plugins/flot/flot-data.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/sb-admin.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Content/Scripts/dairy-farm.js",
                "~/Content/Scripts/jquery-ui-1.11.4.js"
                ));
            
        }
    }
}
