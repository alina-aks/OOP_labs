namespace GameInventory.Models
{
    public class Potion : Item
    {
        public int HealAmount { get; private set; }
        
        public Potion(string name, int heal) : base(name)
        {
            HealAmount = heal;
            IsUsable = true;
        }

        public override string GetInfo()
        {
            return $"{Name} (Восстановление: {HealAmount} HP)";
        }
    }
}