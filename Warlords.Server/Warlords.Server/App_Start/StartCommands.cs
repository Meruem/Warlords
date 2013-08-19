using System;
using System.Diagnostics.Contracts;
using Microsoft.AspNet.SignalR;
using Raven.Client;
using Warlords.Server.ApplicationF;

namespace Warlords.Server.App_Start
{
    public class StartCommands
    {
        public static void PublishStartCommands()
        {
            var service = GlobalHost.DependencyResolver.GetService(typeof (IHubService));
            var hubService = service as IHubService;
            Contract.Assert(hubService != null);

            hubService.Send(new CreateLobbyCommand());
        }
    }
}