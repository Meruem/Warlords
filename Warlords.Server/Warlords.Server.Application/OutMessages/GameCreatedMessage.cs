using Warlords.Server.Domain.Models.Lobby;

namespace Warlords.Server.Application.OutMessages
{
    public class GameCreatedMessage
    {
        public LobbyGameInfo Game { get; set; }
    }
}