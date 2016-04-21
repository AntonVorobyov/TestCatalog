using System.Web;
using System.Web.Optimization;

namespace TestCatalog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Content/js/vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Content/js/vendor/respond.js",
                "~/Content/js/vendor/jquery-2.2.2.js",
                "~/Content/js/vendor/selectize.js",
                "~/Content/js/vendor/jquery.inputmask.bundle.js",
                "~/Content/js/vendor/angular.js",
                "~/Content/js/vendor/angular-route.js",
                "~/Content/js/widget/selectpicker.js",
                "~/Content/js/app/app.js",
                "~/Content/js/app/service/*.js",
                "~/Content/js/app/directive/*.js",
                "~/Content/js/app/controller/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/selectize.bootstrap3.css",
                "~/Content/css/app.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
