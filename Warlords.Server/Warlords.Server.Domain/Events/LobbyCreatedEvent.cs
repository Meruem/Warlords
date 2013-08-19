using System;
using Warlords.Server.Domain.Infrastructure;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Domain.Events
{
    public class LobbyCreatedEvent : Event
    {
        public Guid LobbyGuid { get; set; }
    }
}
