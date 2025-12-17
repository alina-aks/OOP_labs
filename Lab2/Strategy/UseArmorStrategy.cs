using GameInventory.Models;
using GameInventory.State;

namespace GameInventory.Strategy
{
    public class UseArmorStrategy : IUseStrategy
    {
        public void Use(Item item)
        {
            Armor? armor = item as Armor;
            
            if (armor != null && armor.IsEquippable)
            {
                armor.ChangeState(new EquippedState());
                Console.WriteLine($"Броня '{armor.Name}' экипирована");
            }
        }
    }
}