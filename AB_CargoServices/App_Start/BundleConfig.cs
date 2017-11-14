using System.Web;
using System.Web.Optimization;

namespace AB_CargoServices
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/commonJs").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/commonCss").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Admin dashboard js
            bundles.Add(new ScriptBundle("~/bundles/dashboardJs").Include(
                        "~/Scripts/jquery.slimscroll.min.js",
                        "~/Scripts/fastclick.js",
                        "~/Scripts/adminlte.min.js"));

            // Main site js
            bundles.Add(new ScriptBundle("~/bundles/mainSiteJs").Include(
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/mousescroll.js",
                "~/Scripts/smoothscroll.js",
                "~/Scripts/jquery.prettyPhoto.js",
                "~/Scripts/jquery.isotope.min.js",
                "~/Scripts/jquery.inview.min.js",
                "~/Scripts/wow.min.js",
                "~/Scripts/main.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/css/Site.css"));

            // Admin dashboard css
            bundles.Add(new StyleBundle("~/Content/dashboardCss").Include(
                        "~/Content/css/ionicons.min.css",
                        "~/Content/css/AdminLTE.min.css",
                        "~/Content/css/skin-black.min.css",
                        "~/Content/css/skin-green.min.css"));

            // Main site css
            bundles.Add(new StyleBundle("~/Content/mainSiteCss").Include(
                "~/Content/css/animate.min.css",
                "~/Content/css/owl.carousel.css",
                "~/Content/css/owl.transitions.css",
                "~/Content/css/prettyPhoto.css",
                "~/Content/css/main.css",
                "~/Content/css/responsive.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
