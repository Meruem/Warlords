using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Warlords.Server.Domain.Models.Lobby;

namespace Warlords.Server.Domain.Models.Game
{
    public class Game
    {
        public string Id { get; private set; }
        private readonly string _player1Name;
        private readonly string _player2Name;

        public Battlefield Battlefield { get; private set; }

        public Game(LobbyGameInfo gameInfo)
        {
            Contract.Requires(gameInfo != null, "game info");
            _player1Name = gameInfo.Players[0];
            _player2Name = gameInfo.Players[1];
            Id = gameInfo.Id;

            Contract.Assert(!string.IsNullOrEmpty(_player1Name), "player 1");
            Contract.Assert(!string.IsNullOrEmpty(_player2Name), "player 2");
            Contract.Assert(!string.IsNullOrEmpty(Id), "game id");

            Battlefield = new Battlefield(_player1Name, _player2Name);
        }

        public void InitializeGame()
        {
            // test data
            var orc = new CreaturePrototype("Orc", 1, 3);
            var startingPtototype = new BuildingPrototype("Breeding pit", 10, 10, 100, new List<CreaturePrototype> { orc });

            Battlefield.AddBuilding(startingPtototype, _player1Name, ZoneTypeEnum.Home);
            Battlefield.AddBuilding(startingPtototype, _player2Name, ZoneTypeEnum.Home);
        }

        public void ExecuteRound()
        {
            Battlefield.Fight();
            Battlefield.MoveCreatures();
            Battlefield.SpawnCreatures();
        }
    }
}