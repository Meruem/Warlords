using System.Web.Http;
using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Diagnostics.Contracts;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Warlords.Server.ApplicationF;
using Warlords.Server.Common;
using Warlords.Server.DB;
using Warlords.Server.Infrastructure;


[assembly: WebActivator.PreApplicationStartMethod(typeof(Warlords.Server.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Warlords.Server.App_Start.NinjectWebCommon), "Stop")]

namespace Warlords.Server.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper _bootstrapper = new Bootstrapper();

        private static readonly ILog _logger = LogManager.GetLogger(typeof (NinjectWebCommon));

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            _logger.Debug("Ninject Start begin.");
            //DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            _bootstrapper.Initialize(CreateKernel);
            _logger.Debug("Ninject Start ended.");
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            _logger.Debug("Ninject Stop begin.");
            _bootstrapper.ShutDown();
            _logger.Debug("Ninject Stop ended.");
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {
            _logger.Debug("Ninject kernel creation started.");
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
            GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);

            RegisterServices(kernel);
            _logger.Debug("Ninject kernel creation ended.");
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            Contract.Requires(kernel != null);

            kernel.Bind<IHubService>().To<HubService>().InSingletonScope();
            kernel.Bind<IClientSender>().To<SignalRClientSender>();
            kernel.Bind<IDocumentStore>()
                .ToMethod(_ =>
                {
                    var store = new DocumentStore {ConnectionStringName = "WarlordsDB"};
                    store.Initialize();
                    return store;
                })
                .InSingletonScope();
        }        
    }
}
