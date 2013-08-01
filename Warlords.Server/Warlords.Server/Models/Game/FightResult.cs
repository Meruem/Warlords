using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class FightResult
    {
        public IList<Creature> Army1 { get; set; }
        public IList<Creature> Army2 { get; set; }
    }
}