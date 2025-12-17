using GameInventory.Models;

namespace GameInventory.Factory
{
    public interface IItemFactory
    {
        Weapon CreateWeapon();
        Armor CreateArmor();
        Potion CreatePotion();
        QuestItem CreateQuestItem();
    }
}