using GameInventory.State; 

namespace GameInventory.Models
{
    public class Armor : Item, IUpgradable //наследует всю базу с item и улучшение в IUpgradable
    {
        public int Defense { get; private set; }
        public int Level { get; private set; }
        
        public Armor(string name, int defense) : base(name)
        {
            Defense = defense;
            Level = 1;
            IsEquippable = true; //можем одеть поэтому переопределяем
        }

        public void Upgrade()
        {
            Level++;
            Defense += 3;
        }

        public override string GetInfo()
        {
            return $"{Name} (Защита: {Defense}, Уровень: {Level}, Состояние: {State.GetStateName()})";
        }
    }
}