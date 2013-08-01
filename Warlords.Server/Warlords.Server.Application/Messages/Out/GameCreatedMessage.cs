using Warlords.Server.Domain.Models.Lobby;

namespace Warlords.Server.Application.Messages.Out
{
    public class GameCreatedMessage
    {
        public LobbyGameInfo Game { get; set; }
    }
}