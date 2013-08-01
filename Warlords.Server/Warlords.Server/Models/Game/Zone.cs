using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class Zone
    {
        public string OwnerName { get; private set; }

        public IList<Building> Buildings { get; private set; }

        // list of creatures separated by player name
        private IDictionary<string, IList<Creature>> _Creatures;

        public ZoneTypeEnum Type { get; private set; }

        public Zone(ZoneTypeEnum type)
        {
            Buildings = new List<Building>();
            _Creatures = new Dictionary<string, IList<Creature>>();
            Type = type;
        }

        public Zone(string ownerName, ZoneTypeEnum type)
            : this(type)
        {
            OwnerName = ownerName;
        }

        public IList<Creature> GetCreaturesForPlayer(string playerName)
        {
            return _Creatures[playerName];
        }

        public IList<Creature> AllCreatures
        {
            get
            {
                return _Creatures.Values.SelectMany(s => s).ToList();
            }
        }

        public void AddBuilding(BuildingPrototype prototype)
        {
            Buildings.Add(new Building(prototype, OwnerName));
        }

        public void SpawnCreatures()
        {
            if (!_Creatures.ContainsKey(OwnerName))
            {
                _Creatures[OwnerName] = new List<Creature>();
            }

            foreach (var building in Buildings)
            {
                _Creatures[OwnerName] = _Creatures[OwnerName].Concat(building.SpawnCreatures()).ToList();
            }
        }

        public void Fight()
        {
            var player1 = _Creatures.Keys.ElementAt(0);
            var player2 = _Creatures.Keys.ElementAt(1);

            var fight = new Fight(_Creatures[player1], _Creatures[player2]);
            var result = fight.ResolveBattle();
            _Creatures[player1] = result.Army1;
            _Creatures[player2] = result.Army2;
        }

        public string GetWinner()
        {
            var player1 = _Creatures.Keys.ElementAt(0);
            var player2 = _Creatures.Keys.ElementAt(1);

            if (_Creatures[player1].Count > 0)
            {
                if (_Creatures[player2].Count > 0)
                {
                    return null;
                }

                return player1;
            }
            else if (_Creatures[player2].Count > 0)
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
                return _Creatures[winnerName].Count;
            }

            return 0;
        }
    }
}