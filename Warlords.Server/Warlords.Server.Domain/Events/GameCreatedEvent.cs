using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Domain.Events
{
    public class GameCreatedEvent : Event
    {
        public string GameId { get; set; }
        public string OwnerName { get; set; }

        public GameCreatedEvent(string gameId, string ownerName)
        {
            GameId = gameId;
            OwnerName = ownerName;
        }
    }
}