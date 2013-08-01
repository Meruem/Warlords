using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Infrastructure;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Commands
{
    public class RemoveGameCommand : Command
    {
        private Lobby _Lobby;

        public string GameId { get; set; }

        public RemoveGameCommand WithParameters(string gameId)
        {
            GameId = gameId;
            return this;
        }

        public RemoveGameCommand(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public override void Execute()
        {
            _Lobby.RemoveGame(GameId);
        }
    }
}