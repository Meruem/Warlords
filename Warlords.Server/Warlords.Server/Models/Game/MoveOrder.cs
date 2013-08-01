using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class MoveOrder
    {
        public Creature Creature { get; set; }
        public Zone From { get; set; }
        public Zone To { get; set; }
        public string OwnerName { get; set; }

        public void Execute()
        {
            From.GetCreaturesForPlayer(OwnerName).Remove(Creature);
            To.GetCreaturesForPlayer(OwnerName).Add(Creature);
        }
    }
}