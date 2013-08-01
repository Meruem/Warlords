using System.Diagnostics.Contracts;
using System.Web.Optimization;

namespace Warlords.Server.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            Contract.Requires(bundles != null);

            bundles.Add(new ScriptBundle("~/bundles/warlords").Include(
                        "~/Scripts/app.js",
                        "~/Scripts/Controllers/*.js",
                        "~/Scripts/Services/*.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/3rdParty/angular.js",
                        "~/Scripts/3rdParty/angular-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/3rdParty/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/linq").Include(
                        "~/Scripts/3rdParty/linq*"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/3rdParty/jquery.signalR-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/3rdParty/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}