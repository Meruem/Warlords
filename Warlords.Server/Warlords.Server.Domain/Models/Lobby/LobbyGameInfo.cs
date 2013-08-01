using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Warlords.Server.Domain.Models.Lobby
{
    public class LobbyGameInfo
    {
        public LobbyGameInfo(string owner, string id)
        {
            Contract.Requires(owner != null);

            OwnerName = owner;
            Players = new List<string>();
            Id = id;
            JoinPlayer(owner);
            IsStarted = false;
        }

        private const int _MinNumberOfPlayers = 2;

        private const int _MaxNumberOfPlayers = 2;

        public bool IsStarted { get; private set; }

        public string Id { get; private set; }

        public string OwnerName { get; private set; }

        public IList<string> Players { get; private set; }

        public void JoinPlayer(string player)
        {
            Contract.Requires(player != null);
            Contract.Assert(Players.All(p => p != player), "Player already in the game.");

            Players.Add(player);
        }

        public void LeavePlayer(string playerName)
        {
            var inGamePlayer = Players.FirstOrDefault(p => p == playerName);
            Contract.Assert(inGamePlayer != null, "Player is not in game.");

            Players.Remove(inGamePlayer);
        }

        public void StartGame()
        {
            Contract.Assert(!IsStarted, "Game already started.");
            Contract.Assert(Players.Count() >= _MinNumberOfPlayers, "Not enaugh players to start the game");

            IsStarted = true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Players.Count() <= _MaxNumberOfPlayers);
        }
    }
}