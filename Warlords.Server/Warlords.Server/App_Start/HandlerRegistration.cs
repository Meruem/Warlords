using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using log4net;
using Microsoft.AspNet.SignalR;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Application.ViewModels.Handlers;

namespace Warlords.Server.App_Start
{
    public class HandlerRegistration
    {
        public static void RegisterHandlers()
        {
            var handlerFactory = GlobalHost.DependencyResolver.GetService(typeof(IHandlerFactory)) as IHandlerFactory;
            Contract.Assert(handlerFactory != null);

            handlerFactory.AddHandlersLocatedInAssembly(typeof(IHandles<>).Assembly);
        }
    }
}