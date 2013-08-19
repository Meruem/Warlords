using System.Diagnostics.Contracts;
using Microsoft.AspNet.SignalR;

namespace Warlords.Server.App_Start
{
    public class HandlerRegistration
    {
        public static void RegisterHandlers()
        {
        //    var handlerFactory = GlobalHost.DependencyResolver.GetService(typeof(IHandlerFactory)) as IHandlerFactory;
        //    Contract.Assert(handlerFactory != null);

        //    handlerFactory.AddHandlersLocatedInAssembly(typeof(IHandles<>).Assembly);
        }
    }
}