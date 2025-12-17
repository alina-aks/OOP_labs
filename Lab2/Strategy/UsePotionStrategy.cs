using GameInventory.Models;

namespace GameInventory.Strategy
{
    public class UsePotionStrategy : IUseStrategy
    {
        public void Use(Item item)
        {
            Potion? potion = item as Potion;

            if (potion != null)
            {
            }
        }
    }
}
