using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Infrastructure;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Commands
{
    public class CreateGameCommand : Command<LobbyGameInfo>
    {
        private Lobby _Lobby;

        public Player Owner { get; set; }

        public CreateGameCommand(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public CreateGameCommand WithParameters(Player owner)
        {
            Owner = owner;
            return this;
        }

        public override LobbyGameInfo Execute()
        {
            return _Lobby.CreateGame(Owner);
        }
    }
}