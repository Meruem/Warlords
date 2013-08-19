using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Domain.Events;
using Warlords.Server.Domain.Infrastructure;
using Warlords.Server.DomainF.AggregateRoot;
using Warlords.Server.DomainF.Events;
using LobbyCreatedEvent = Warlords.Server.Domain.Events.LobbyCreatedEvent;
using PlayerJoinedEvent = Warlords.Server.Domain.Events.PlayerJoinedEvent;

namespace Warlords.Server.Domain.Models.Lobby
{
    public class Lobby : AggregateRoot
    {
        private readonly IDictionary<string, LobbyGameInfo> _games = new Dictionary<string, LobbyGameInfo>();
        private readonly IList<string> _players = new List<string>();

        public IList<string> Players { get { return _players; } }
        public IList<LobbyGameInfo> Games { get { return _games.Values.ToList(); } }

        // Required for creation through Repository<Lobby>
        public Lobby()
            :base(null)
        {
        }

        public Lobby(Guid id)
            : base(id)
        {
            ApplyChange(new LobbyCreatedEvent { LobbyGuid = id});
        }

        public void Apply(LobbyCreatedEvent @event)
        {
            Contract.Requires(@event != null);
            Id = @event.LobbyGuid;
        }

        [Pure]
        public bool IsPlayerJoined(string name)
        {
            return _players.Contains(name);
        }

        public void JoinPlayer(string name, string connectionId)
        {
            Contract.Requires(string.IsNullOrEmpty(name) == false, "name should not be empty");
            Contract.Assert(!IsPlayerJoined(name), "player already joined");
            ApplyChange(new PlayerJoinedEvent(name, connectionId));  
        }

        private void Apply(PlayerJoinedEvent e)
        {
            Contract.Requires(e != null);
            _players.Add(e.UserName);
        }

        private bool IsPlayerInAnyGame(string playerName)
        {
            return _games.Values.Any(g => g.Players.Any(p => p == playerName));
        }

        public void CreateGame(string owner)
        {
            Contract.Requires(owner != null);
            Contract.Assert(!IsPlayerInAnyGame(owner), "Owner already in game");

            ApplyChange(new GameCreatedEvent(Guid.NewGuid().ToString(), owner));
        }

        private void Apply(GameCreatedEvent e)
        {
            Contract.Requires(e != null);

            var game = new LobbyGameInfo(e.OwnerName, e.GameId);
            _games[e.GameId] = game;
        }

        public void RemoveGame(string gameId)
        {
            Contract.Assert(_games.ContainsKey(gameId), "No game with Id {0} exists.");
            var game = _games[gameId];
            Contract.Assert(!game.Players.Any(), "Game still contains players");

            _games.Remove(gameId);
        }

        public void JoinGame(LobbyGameInfo game, string player)
        {
            Contract.Requires(game != null);
            Contract.Requires(player != null);
            Contract.Assert(!IsPlayerInAnyGame(player), "Already in another game");
            Contract.Assert(game.Players.All(p => p != player), "Already in this game");

            game.JoinPlayer(player);
        }

        [Pure]
        public LobbyGameInfo GetGameById(string id)
        {
            return _games.ContainsKey(id) ? _games[id] : null;
        }

        [Pure]
        public LobbyGameInfo GetGamePlayedByPlayer(string playerName)
        {
            return _games.Values.FirstOrDefault(g => g.Players.Any(p => p == playerName));
        }

        public override void Apply(Event obj0)
        {
            throw new NotImplementedException();
        }
    }
}