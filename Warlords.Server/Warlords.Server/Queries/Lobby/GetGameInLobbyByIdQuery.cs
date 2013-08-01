using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Queries
{
    public class GetGameInLobbyByIdQuery : Query<LobbyGameInfo>
    {
        private Lobby _Lobby;

        public string Id { get; set; }

        public GetGameInLobbyByIdQuery(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public GetGameInLobbyByIdQuery WithParameters(string id)
        {
            Id = id;
            return this;
        }

        public override LobbyGameInfo GetResult()
        {
            return String.IsNullOrEmpty(Id) ? null : _Lobby.GetGameById(Id);
        }
    }
}