using Ninject;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Application.OutMessages;
using Warlords.Server.Common.Aspects;
using Warlords.Server.Domain.Events;

namespace Warlords.Server.Application.EventHandlers.ClientInteraction
{
    public class PlayerJoinedEventHandler : IHandles<PlayerJoinedEvent>
    {
        [Inject]
        public IClientSender ClientSender { get; set; }
        
        [Log]
        public void Handle(PlayerJoinedEvent message)
        {
            ClientSender.SendToAllClients("MessageHub", d => d.playerJoined(new PlayerJoinedMessage(message.UserName)));
        }
    }
}