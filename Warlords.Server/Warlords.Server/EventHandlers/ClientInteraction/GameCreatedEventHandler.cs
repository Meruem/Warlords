using Microsoft.AspNet.SignalR;
using Warlords.Server.Events;
using Warlords.Server.Hubs;
using Warlords.Server.Infrastructure;

namespace Warlords.Server.EventHandlers
{
    public class GameCreatedEventHandler : IHandles<GameCreatedEvent>
    {
        public void Handle(GameCreatedEvent message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();

            context.Clients.All.gameCreated(new { Game = new { OwnerName = message.OwnerName} });
        }
    }
}