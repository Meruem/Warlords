using Microsoft.AspNet.SignalR;
using Warlords.Server.Events;
using Warlords.Server.Hubs;
using Warlords.Server.Infrastructure;
using Warlords.Server.Messages.Out;

namespace Warlords.Server.EventHandlers
{
    public class PlayerJoinedEventHandler : IHandles<PlayerJoinedEvent>
    {
        public void Handle(PlayerJoinedEvent message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            context.Clients.All.playerJoined(new PlayerJoinedMessage(message.Name));
        }
    }
}