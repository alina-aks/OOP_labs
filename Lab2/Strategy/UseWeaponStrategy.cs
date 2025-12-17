using GameInventory.Models;
using GameInventory.State;

namespace GameInventory.Strategy
{
    public class UseWeaponStrategy : IUseStrategy
    {
        public void Use(Item item)
        {
            Weapon? weapon = item as Weapon;

            if (weapon != null)
            {
                weapon.ChangeState(new EquippedState());
            }
        }
    }
}
