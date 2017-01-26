using System.Web;
using System.Web.Optimization;

namespace SMETA.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/uboldjs").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/detect.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/waves.js",
                      "~/Scripts/wow.min.js"));

            bundles.Add(new StyleBundle("~/bundles/ubold").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/components.css",
                      "~/Content/core.css",
                      "~/Content/responsive.css",
                      "~/Content/pages.css",
                      "~/Content/icons.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/fontawesome").Include(
                    "~/Content/font-awesome.css"));
        }
    }
}
