using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Queries
{
    public class GetAllGamesInLobbyQuery : Query<IEnumerable<LobbyGameInfo>>
    {
        private Lobby _Lobby;

        public GetAllGamesInLobbyQuery(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public override IEnumerable<LobbyGameInfo> GetResult()
        {
            return _Lobby.Games;
        }
    }
}