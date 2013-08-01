namespace Warlords.Server.Domain.Models.Game
{
    public class CreaturePrototype
    {
        public string Name { get; private set; }
        public int Attack { get; private set; }
        public int MaxHp { get; private set; }

        public CreaturePrototype(string name, int attack, int maxHp)
        {
            Name = name;
            MaxHp = maxHp;
            Attack = attack;
        }
    }
}