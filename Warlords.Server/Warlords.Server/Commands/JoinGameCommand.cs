using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Infrastructure;
using Warlords.Server.Models;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.Commands
{
    public class JoinGameCommand : Command
    {
        private Lobby _Lobby;

        public Player Player { get; set; }
        public LobbyGameInfo GameToJoin { get; set; }

        public JoinGameCommand WithParameters(Player player, LobbyGameInfo gameToJoin)
        {
            Player = player;
            GameToJoin = gameToJoin;
            return this;
        }

        public JoinGameCommand(Lobby lobby)
        {
            _Lobby = lobby;
        }

        public override void Execute()
        {
            _Lobby.JoinGame(GameToJoin, Player);
        }
    }
}