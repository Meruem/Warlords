using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Infrastructure;
using Warlords.Server.Events;

namespace Warlords.Server.Models.Lobby
{
    public class Lobby : AggregateRoot
    {
        private readonly Guid _id;
        public override Guid Id { get { return _id;} }

        private readonly IDictionary<string, LobbyGameInfo> _games = new Dictionary<string, LobbyGameInfo>();
        private readonly IDictionary<string, Player> _players = new Dictionary<string, Player>();

        public IList<Player> Players { get { return _players.Values.ToList(); } }
        public IList<LobbyGameInfo> Games { get { return _games.Values.ToList(); } }

        public Lobby()
        {
            _id = new Guid();
        }

        public void RefreshPlayerConnectionId(string name, string id)
        {
            var player = GetPlayerByName(name);
            Contract.Assert(player != null, "No player exists with given name");
            player.RefreshConnection(id);
        }

        [Pure]
        public bool IsPlayerJoined(string name)
        {
            return _players.ContainsKey(name);
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

            var newPlayer = new Player(e.ConnectionId, e.Name);
            _players[e.Name] = newPlayer;
        }

        public void CreateGame(Player owner)
        {
            Contract.Requires(owner != null);
            Contract.Requires(!owner.IsInGame, "Owner already in game");

            ApplyChange(new GameCreatedEvent(Guid.NewGuid().ToString(), owner.Name));
        }

        public void Apply(GameCreatedEvent e)
        {
            Contract.Requires(e != null);
            Contract.Assert(_players.ContainsKey(e.OwnerName));

            var player = GetPlayerByName(e.OwnerName);
            var game = new LobbyGameInfo(player, e.GameId);
            _games[e.GameId] = game;
        }

        public void RemoveGame(string gameId)
        {
            Contract.Assert(_games.ContainsKey(gameId), "No game with Id {0} exists.");
            var game = _games[gameId];
            Contract.Assert(!game.Players.Any(), "Game still contains players");

            _games.Remove(gameId);
        }

        public void JoinGame(LobbyGameInfo game, Player player)
        {
            Contract.Requires(game != null);
            Contract.Requires(player != null);
            Contract.Requires(!player.IsInGame, "Already in this game");
            Contract.Requires(game.Players.All(p => p.Name != player.Name), "Already in this game");

            game.JoinPlayer(player);
        }

        [Pure]
        public Player GetPlayerByName(string name)
        {
            return _players.ContainsKey(name) ? _players[name] : null;
        }

        [Pure]
        public LobbyGameInfo GetGameById(string id)
        {
            return _games.ContainsKey(id) ? _games[id] : null;
        }

        [Pure]
        public LobbyGameInfo GetGamePlayedByPlayer(string playerName)
        {
            return _games.Values.FirstOrDefault(g => g.Players.Any(p => p.Name == playerName));
        }
    }
}