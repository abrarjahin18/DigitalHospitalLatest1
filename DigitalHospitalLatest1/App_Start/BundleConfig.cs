using System.Web;
using System.Web.Optimization;

namespace DigitalHospitalLatest1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/StyleSheet1.css"));
            bundles.Add(new ScriptBundle("~/JS").Include(
                        "~/Content/bower_components/jquery/dist/jquery.min.js",
                        "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        "~/Content/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Content/bower_components/fastclick/lib/fastclick.js",
                        "~/Content/dist/js/adminlte.min.js",
                        "~/Content/dist/js/demo.js",
                        //"~/Scripts/free-jqGrid/jquery.jqgrid.min.js",
                        //"~/Scripts/free-jqGrid/i18n/grid.locale-en.js",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Content/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"
                        ));


            //   bundles.Add(new StyleBundle("~/CSS").Include(
            //     "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
            //   "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
            // "~/Content/bower_components/Ionicons/css/ionicons.min.css",
            // "~/Content/dist/css/AdminLTE.min.css",
            //"~/Content/dist/css/skins/_all-skins.min.css",
            //"~/Content/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css"
            //));

            BundleTable.EnableOptimizations = true;
        }
    }
}
