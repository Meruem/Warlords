using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Messages.Out
{
    public class GameCreatedMessage
    {
        public LobbyGameInfo Game { get; set; }
    }
}