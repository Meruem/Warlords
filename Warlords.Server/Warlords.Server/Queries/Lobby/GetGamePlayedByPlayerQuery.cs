using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Queries
{
    public class GetGamePlayedByPlayerQuery : Query<LobbyGameInfo>
    {
        private Lobby _Lobby;

        public string Name { get; set; }

        public GetGamePlayedByPlayerQuery(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public GetGamePlayedByPlayerQuery WithParameters(string name)
        {
            Name = name;
            return this;
        }

        public override LobbyGameInfo GetResult()
        {
            return String.IsNullOrEmpty(Name) ? null : _Lobby.GetGamePlayedByPlayer(Name);
        }
    }
}