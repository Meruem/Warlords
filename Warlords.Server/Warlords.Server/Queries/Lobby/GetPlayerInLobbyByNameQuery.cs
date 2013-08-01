using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Queries
{
    public class GetPlayerInLobbyByNameQuery : Query<Player>
    {
        private Lobby _Lobby;

        public string Name { get; set; }

        public GetPlayerInLobbyByNameQuery(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public GetPlayerInLobbyByNameQuery WithParameters(string name)
        {
            Name = name;
            return this;
        }

        public override Player GetResult()
        {
            return String.IsNullOrEmpty(Name) ? null : _Lobby.GetPlayerByName(Name);
        }
    }
}