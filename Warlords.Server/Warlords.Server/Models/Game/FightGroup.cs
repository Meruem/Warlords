using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class FightGroup
    {
        public IList<Creature> Creatures1 { get; private set; }
        public IList<Creature> Creatures2 { get; private set; }

        public FightGroup()
        {
            Creatures1 = new List<Creature>();
            Creatures2 = new List<Creature>();
        }

        public void Battle()
        {
            foreach (var creature in Creatures1)
            {
                var target = Creatures2.FirstOrDefault(c => c.IsDead() == false);
                if (target != null)
                {
                    creature.AttackCreature(target);
                }
            }

            foreach (var creature in Creatures2)
            {
                var target = Creatures1.FirstOrDefault(c => c.IsDead() == false);
                if (target != null)
                {
                    creature.AttackCreature(target);
                }
            }
        }
    }
}