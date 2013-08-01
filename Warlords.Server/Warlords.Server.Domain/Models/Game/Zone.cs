using System.Collections.Generic;
using System.Linq;

namespace Warlords.Server.Domain.Models.Game
{
    public class Zone
    {
        public string OwnerName { get; private set; }

        public IList<Building> Buildings { get; private set; }

        // list of creatures separated by player name
        private readonly IDictionary<string, IList<Creature>> _creatures;

        public ZoneTypeEnum Type { get; private set; }

        public Zone(ZoneTypeEnum type)
        {
            Buildings = new List<Building>();
            _creatures = new Dictionary<string, IList<Creature>>();
            Type = type;
        }

        public Zone(string ownerName, ZoneTypeEnum type)
            : this(type)
        {
            OwnerName = ownerName;
        }

        public IList<Creature> GetCreaturesForPlayer(string playerName)
        {
            return _creatures[playerName];
        }

        public IList<Creature> AllCreatures
        {
            get
            {
                return _creatures.Values.SelectMany(s => s).ToList();
            }
        }

        public void AddBuilding(BuildingPrototype prototype)
        {
            Buildings.Add(new Building(prototype, OwnerName));
        }

        public void SpawnCreatures()
        {
            if (!_creatures.ContainsKey(OwnerName))
            {
                _creatures[OwnerName] = new List<Creature>();
            }

            foreach (var building in Buildings)
            {
                _creatures[OwnerName] = _creatures[OwnerName].Concat(building.SpawnCreatures()).ToList();
            }
        }

        public void Fight()
        {
            var player1 = _creatures.Keys.ElementAt(0);
            var player2 = _creatures.Keys.ElementAt(1);

            var fight = new Fight(_creatures[player1], _creatures[player2]);
            var result = fight.ResolveBattle();
            _creatures[player1] = result.Army1;
            _creatures[player2] = result.Army2;
        }

        public string GetWinner()
        {
            var player1 = _creatures.Keys.ElementAt(0);
            var player2 = _creatures.Keys.ElementAt(1);

            if (_creatures[player1].Count > 0)
            {
                if (_creatures[player2].Count > 0)
                {
                    return null;
                }

                return player1;
            }
            if (_creatures[player2].Count > 0)
            {
                return player2;
            }

            return null;
        }

        public int GetWinnerArmyStrength()
        {
            var winnerName = GetWinner();
            if (winnerName != null)
            {
                return _creatures[winnerName].Count;
            }

            return 0;
        }
    }
}