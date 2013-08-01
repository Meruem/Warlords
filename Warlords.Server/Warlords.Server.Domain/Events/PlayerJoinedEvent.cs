using Warlords.Server.Domain.Infrastructure;

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