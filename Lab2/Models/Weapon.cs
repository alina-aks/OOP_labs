using GameInventory.State; 

namespace GameInventory.Models
{
    public class Weapon : Item, IUpgradable
    {
        public int Damage { get; private set; }
        public int Level { get; private set; }
        
        public Weapon(string name, int damage) : base(name)
        {
            Damage = damage;
            Level = 1;
            IsEquippable = true;
        }

        public void Upgrade()
        {
            Level++;
            Damage += 5;
        }

        public override string GetInfo()
        {
            return $"{Name} (Урон: {Damage}, Уровень: {Level}, Состояние: {State.GetStateName()})";
        }
    }
}