using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Queries
{
    public class GetAllPlayersInLobbyQuery : Query<IEnumerable<Player>>
    {
        private Lobby _Lobby;

        public GetAllPlayersInLobbyQuery(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public override IEnumerable<Player> GetResult()
        {
            return _Lobby.Players;
        }
    }
}