using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace Warlords.Server.Domain.Models.Game
{
    public class BuildingPrototype
    {
        public string Name { get; private set; }
        public int Cost { get; private set; }
        public int Attack { get; private set; }
        public int MaxHp { get; private set; }
        public IReadOnlyCollection<CreaturePrototype> Spawns { get; private set; }

        public BuildingPrototype(string name, int cost, int attack, int maxHp, IList<CreaturePrototype> spawns)
        {
            Contract.Requires(spawns != null);

            Name = name;
            Cost = cost;
            MaxHp = maxHp;
            Attack = attack;
            Spawns = new ReadOnlyCollection<CreaturePrototype>(spawns);
        }
    }
}