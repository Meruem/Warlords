using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Queries
{
    public class IsPlayerJoinedInLobbyQuery : Query<bool>
    {
        private Lobby _Lobby;

        public string Name { get; set; }

        public IsPlayerJoinedInLobbyQuery WithParameters(string name)
        {
            Name = name;
            return this;
        }

        public IsPlayerJoinedInLobbyQuery(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public override bool GetResult()
        {
            return String.IsNullOrEmpty(Name) ? false : _Lobby.IsPlayerJoined(Name);
        }
    }
}