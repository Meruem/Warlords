using Warlords.Server.App_Start;
using log4net;
using log4net.Config;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Warlords.Server
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("Application startup");
            logger.Info("---");
            logger.Info("Application Started");
            logger.Info("---");

            logger.Debug("Mapping Hubs started");
            RouteTable.Routes.MapHubs();
            logger.Debug("Mapping Hubs ended");

            logger.Debug("Area registration started");
            AreaRegistration.RegisterAllAreas();
            logger.Debug("Area registration ended");

            logger.Debug("WebApi configuration started");
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            logger.Debug("WebApi configuration ended");

            logger.Debug("Filters configuration started");
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            logger.Debug("Filters configuration ended");

            logger.Debug("Route configuration started");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            logger.Debug("Route configuration ended");

            logger.Debug("Bundles configuration started");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            logger.Debug("Bundles configuration ended");

            logger.Debug("Started executing startup commands");

            logger.Debug("Finished executing startup commands");
        }
    }
}