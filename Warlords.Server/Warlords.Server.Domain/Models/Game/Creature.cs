﻿using System.Diagnostics.Contracts;

namespace Warlords.Server.Domain.Models.Game
{
    public class Creature
    {
        public CreaturePrototype Prototype { get; private set; }
        public int Damage { get; private set; }
        public string OwnerName { get; private set; }

        public Creature(CreaturePrototype prototype, string ownerName)
        {
            Damage = 0;
            Prototype = prototype;
            OwnerName = ownerName;
        }

        public bool IsDead()
        {
            return Damage >= Prototype.MaxHp;
        }

        public void TakeDamage(int damage)
        {
            Damage += damage;
        }

        public void AttackCreature(Creature target)
        {
            Contract.Requires(target != null);

            target.TakeDamage(Prototype.Attack);
        }
    }
}