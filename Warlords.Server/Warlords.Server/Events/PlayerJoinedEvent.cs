using Warlords.Server.Infrastructure;

namespace Warlords.Server.Events
{
    public class PlayerJoinedEvent : Event
    {
        public readonly string Name;
        public readonly string ConnectionId;

        public PlayerJoinedEvent(string name, string connectionId)
        {
            Name = name;
            ConnectionId = connectionId;
        }
    }
}