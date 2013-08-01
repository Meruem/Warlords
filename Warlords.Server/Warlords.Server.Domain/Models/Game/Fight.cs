using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Warlords.Server.Domain.Models.Game
{
    public class Fight
    {
        private IList<Creature> _army1;
        private IList<Creature> _army2;

        private const int _BattleRounds = 3;

        public Fight(IList<Creature> army1, IList<Creature> army2)
        {
            _army1 = army1;
            _army2 = army2;
        }

        public FightResult ResolveBattle()
        {
             FightResult result = null;
             for (int i = 0; i < _BattleRounds; i++)
             {
                  var groups = LineUp();
                  foreach (var group in groups) { group.Battle(); }
                  result = GetBattleResult(groups);
                  _army1 = result.Army1;
                  _army2 = result.Army2;
             }
            
             return result;
        }

        private IList<FightGroup> LineUp()
        {
            var maxCreatures = Math.Max(_army1.Count, _army2.Count);
            var groupNumber = Math.Min(_army1.Count, _army2.Count);

            var groups = new List<FightGroup>();

            for (int i = 0; i < maxCreatures; i++)
            {
                // creatures available in both armies
                if (i < groupNumber)
                {
                    var group = new FightGroup();
                    group.Creatures1.Add(_army1[i]);
                    group.Creatures2.Add(_army2[i]);
                    groups.Add(group);
                }
                else
                {
                    if (_army1.Count > _army2.Count)
                    {
                        groups[(i - groupNumber) % groupNumber].Creatures1.Add(_army1[i]);
                    }
                    else
                    {
                        groups[(i - groupNumber) % groupNumber].Creatures2.Add(_army2[i]);
                    }
                }
            }

            return groups;
        }

        private FightResult GetBattleResult(IEnumerable<FightGroup> groups)
        {
            Contract.Requires(groups != null);

            var result = new FightResult();

            foreach (var group in groups)
            {
                foreach (var creature in group.Creatures1)
                {
                    if (creature.IsDead() == false)
                    {
                        result.Army1.Add(creature);
                    }
                }

                foreach (var creature in group.Creatures2)
                {
                    if (creature.IsDead() == false)
                    {
                        result.Army2.Add(creature);
                    }
                }
            }

            return result;
        }
    }
}