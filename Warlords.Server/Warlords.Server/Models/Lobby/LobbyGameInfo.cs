using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Warlords.Server.Models.Lobby
{
    public class LobbyGameInfo
    {
        public LobbyGameInfo(Player owner, string id)
        {
            Contract.Requires(owner != null);

            Owner = owner;
            Players = new List<Player>();
            Id = id;
            JoinPlayer(owner);
            IsStarted = false;
        }

        private const int MinNumberOfPlayers = 2;

        private const int MaxNumberOfPlayers = 2;

        public bool IsStarted { get; private set; }

        public string Id { get; private set; }

        public Player Owner { get; private set; }

        public string OwnerName { get { return Owner.Name; } }

        public IList<Player> Players { get; private set; }

        public void JoinPlayer(Player player)
        {
            Contract.Requires(player != null);
            Contract.Requires(!player.IsInGame, "Player has already open game.");
            Contract.Requires(Players.All(p => p.GetConnectionId() != player.GetConnectionId()), "Player already in the game.");

            Players.Add(player);
            player.IsInGame = true;
        }

        public void LeavePlayer(string playerName)
        {
            var inGamePlayer = Players.FirstOrDefault(p => p.Name == playerName);
            Contract.Assert(inGamePlayer != null, "Player is not in game.");

            Players.Remove(inGamePlayer);
            inGamePlayer.IsInGame = false;
        }

        public void StartGame()
        {
            Contract.Assert(!IsStarted, "Game already started.");
            Contract.Assert(Players.Count() >= MinNumberOfPlayers, "Not enaugh players to start the game");

            IsStarted = true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Players.Count() <= MaxNumberOfPlayers);
        }
    }
}