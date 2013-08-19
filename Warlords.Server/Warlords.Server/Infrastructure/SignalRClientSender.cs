using Microsoft.AspNet.SignalR;
using Warlords.Server.Common;

namespace Warlords.Server.Infrastructure
{
    public class SignalRClientSender : IClientSender
    {
        public void SendToAllClients(object message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext("MessageHub");
            context.Clients.All.handleMessage(message.GetType().Name, message);
        }

        public void SendToClient(string connectionId, object message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext("MessageHub");
            context.Clients.Client(connectionId).handleMessage(message.GetType().Name, message);
        }
    }
}