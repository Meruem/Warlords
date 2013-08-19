using Warlords.Server.Domain.Infrastructure;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Domain.Events
{
    public class PlayerJoinedEvent : Event
    {
        public PlayerJoinedEvent(string name, string connectionId)
        {
            UserName = name;
            ConnectionId = connectionId;
        }
    }
}