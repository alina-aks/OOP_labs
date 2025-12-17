using GameInventory.Models;

namespace GameInventory.Strategy
{
    public interface IUseStrategy
    {
        void Use(Item item);
    }
}
