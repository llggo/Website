using System.Web.Optimization;

namespace Company
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/signalR").Include(
                        "~/Scripts/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugin/smart-chat-ui").Include(
                        "~/Extends/Content/Plugin/smart-chat-ui/smart.chat.ui.js",
                        "~/Extends/Content/Plugin/smart-chat-ui/smart.chat.manager.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/template/smart-admin").Include(
                "~/Extends/Content/Template/SmartAdmin/js/bootstrap/bootstrap.js",
                      "~/Extends/Content/Template/SmartAdmin/js/app.config.seed.js",
                      "~/Extends/Content/Template/SmartAdmin/js/app.seed.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/template/smart-admin/datatable").Include(
                      "~/Extends/Content/Template/SmartAdmin/js/datatables/jquery.dataTables.js",
                      "~/Extends/Content/Template/SmartAdmin/js/datatables/dataTables.colVis.js",
                      "~/Extends/Content/Template/SmartAdmin/js/datatables/dataTables.tableTools.js",
                      "~/Extends/Content/Template/SmartAdmin/js/datatables/dataTables.bootstrap.js",
                      "~/Extends/Content/Template/SmartAdmin/js/datatable-responsive/datatables.responsive.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/template/smart-admin/smartwidgets").Include(
                      "~/Extends/Content/Template/SmartAdmin/js/smartwidgets/jarvis.widget.js"
                      ));



            bundles.Add(new StyleBundle("~/manage/plugin").Include(
                      //"~/Extends/Content/Plugin/bootstrap-3.3.7/css/bootstrap.css",
                      "~/Extends/Content/Plugin/font-awesome-4.7.0/css/font-awesome.css"));

            bundles.Add(new StyleBundle("~/manage/template/smart-admin").Include(
                      "~/Extends/Content/Template/SmartAdmin/css/bootstrap.css",
                      "~/Extends/Content/Template/SmartAdmin/css/smartadmin-production.css",
                      "~/Extends/Content/Template/SmartAdmin/css/smartadmin-production-plugins.css",
                      "~/Extends/Content/Template/SmartAdmin/css/smartadmin-skins.css",
                      "~/Extends/Content/Template/SmartAdmin/css/smartadmin-rtl.css"
                ));


            BundleTable.EnableOptimizations = true;
        }
    }
}
