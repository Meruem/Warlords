using System;
using Microsoft.AspNet.SignalR;
using Warlords.Server.Application.Infrastructure;

namespace Warlords.Server.Infrastructure
{
    public class SignalRClientSender : IClientSender
    {
        public void SendToAllClients(string hubName, Action<dynamic> action)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext(hubName);
            var clients = context.Clients.All;
            action.Invoke(clients);
        }

        public void SendToClient(string connectionId, string hubName, Action<dynamic> action)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext(hubName);

            var client = context.Clients.Client(connectionId);
            action.Invoke(client);
        }
    }
}