using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class BuildingPrototype
    {
        public string Name { get; private set; }
        public int Cost { get; private set; }
        public int Attack { get; private set; }
        public int MaxHp { get; private set; }
        public IReadOnlyCollection<CreaturePrototype> Spawns { get; private set; }

        public BuildingPrototype(string name, int cost, int attack, int maxHP, IList<CreaturePrototype> spawns)
        {
            Contract.Requires(spawns != null);

            Name = name;
            Cost = cost;
            MaxHp = maxHP;
            Attack = attack;
            Spawns = new ReadOnlyCollection<CreaturePrototype>(spawns);
        }
    }
}