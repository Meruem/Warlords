using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class CreaturePrototype
    {
        public string Name { get; private set; }
        public int Attack { get; private set; }
        public int MaxHp { get; private set; }

        public CreaturePrototype(string name, int attack, int maxHP)
        {
            Name = name;
            MaxHp = maxHP;
            Attack = attack;
        }
    }
}