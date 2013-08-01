using System.Diagnostics.Contracts;
using Microsoft.AspNet.SignalR;
using Warlords.Server.Application.Commands;
using Warlords.Server.Application.Infrastructure;

namespace Warlords.Server.App_Start
{
    public class StartCommands
    {
        public static void PublishStartCommands()
        {
            var hubService = GlobalHost.DependencyResolver.GetService(typeof (IHubService)) as IHubService;
            Contract.Assert(hubService != null);

            hubService.Send(new CreateLobbyMessage());
        }
    }
}