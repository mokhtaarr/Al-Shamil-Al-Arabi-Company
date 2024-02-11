using System.Web;
using System.Web.Optimization;

namespace Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //////////////////////////// New Style For Portal //////////////////////////////
            bundles.Add(new StyleBundle("~/Content/PortalStyle").Include(
                      "~/Content/Portal/assets/css/fonts.css",
                      "~/Content/Portal/assets/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/Portal/assets/vendor/icofont/icofont.min.css",
                      "~/Content/Portal/assets/vendor/boxicons/css/boxicons.min.css",
                      "~/Content/Portal/assets/vendor/owl.carousel/assets/owl.carousel.min.css",
                      "~/Content/Portal/assets/vendor/venobox/venobox.css",
                      "~/Content/Portal/assets/vendor/aos/aos.css",
                      "~/Content/Portal/assets/css/style.css",
                      "~/Content/Site.css"
                      ));

            bundles.Add(new ScriptBundle("~/Content/PortalScript").Include(
                      "~/Content/Portal/assets/vendor/jquery/jquery.min.js",
                      "~/Scripts/jquery.cookie-1.4.1.min.js",
                      "~/Content/Portal/assets/vendor/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Content/Portal/assets/vendor/jquery.easing/jquery.easing.min.js",
                      "~/Content/Portal/assets/vendor/jquery/jquery.min.js",
                      "~/Content/Portal/assets/vendor/php-email-form/validate.js",
                      "~/Content/Portal/assets/vendor/isotope-layout/isotope.pkgd.min.js",
                      "~/Content/Portal/assets/vendor/owl.carousel/owl.carousel.min.js",
                      "~/Content/Portal/assets/vendor/venobox/venobox.min.js",
                      "~/Content/Portal/assets/vendor/aos/aos.js",
                      "~/Content/Portal/assets/js/main.js"
                      ));


            ///////////////////////////// Admin Panel /////////////////////////////
            bundles.Add(new ScriptBundle("~/bundles/AdminPanel").Include(
                     "~/Content/Admin/AdminPanel/graindashboard.js",
                     "~/Content/Admin/AdminPanel/graindashboard.vendor.js",
                     "~/Content/Admin/AdminPanel/resizeSensor.js",
                     "~/Content/Admin/AdminPanel/chartist.js",
                     "~/Content/Admin/AdminPanel/chartist-plugin-tooltip.js",
                     "~/Content/Admin/AdminPanel/gd.chartist-area.js",
                     "~/Content/Admin/AdminPanel/gd.chartist-bar.js",
                     "~/Content/Admin/AdminPanel/gd.chartist-donut.js",
                     "~/Content/Admin/toastr/toastr.min.js",
                     "~/Content/Admin/dataTable/jquery.dataTables.min.js",
                     "~/Content/Admin/dataTable/dataTables.bootstrap4.min.js"));

            bundles.Add(new StyleBundle("~/Content/AdminPanel/css").Include(
                      "~/Content/Admin/AdminPanel/chartist.css",
                      "~/Content/Admin/AdminPanel/chartist-plugin-tooltip.css",
                      "~/Content/Admin/AdminPanel/graindashboard.css",
                      "~/Content/all.css",
                      "~/Content/Admin/toastr/toastr.css",
                      "~/Content/Admin/dataTable/dataTables.bootstrap4.min.css",
                      "~/Content/Admin/AdminPanel/CustomAP.css"));
        }
    }
}
