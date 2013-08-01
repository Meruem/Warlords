using System.Collections.Generic;
using System.Linq;

namespace Warlords.Server.Domain.Models.Game
{
    public class Building
    {
        public BuildingPrototype Prototype { get; private set; }
        public string OwnerName { get; private set; }
        public int Damage { get; private set; }

        public Building(BuildingPrototype prototype, string ownerName)
        {
            Damage = 0;
            Prototype = prototype;
            OwnerName = ownerName;
        }

        public IList<Creature> SpawnCreatures()
        {
            return Prototype.Spawns.Select(cp => new Creature(cp, OwnerName)).ToList();
        }
    }
}