using System.Collections.Generic;

namespace Warlords.Server.Domain.Models.Game
{
    public class FightResult
    {
        public IList<Creature> Army1 { get; set; }
        public IList<Creature> Army2 { get; set; }
    }
}