using Ninject;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Common.Aspects;
using Warlords.Server.Domain.Events;

namespace Warlords.Server.Application.EventHandlers.ClientInteraction
{
    public class GameCreatedEventHandler : IHandles<GameCreatedEvent>
    {
        [Inject]
        public IClientSender ClientSender { get; set; }

        [Log]
        public void Handle(GameCreatedEvent message)
        {
            ClientSender.SendToAllClients(
                "MessageHub",
                d => d.gameCreated(new { Game = new { OwnerName = message.OwnerName} }));
        }
    }
}