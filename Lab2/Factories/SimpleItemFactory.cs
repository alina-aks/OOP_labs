using GameInventory.Models;

namespace GameInventory.Factory
{
    public class SimpleItemFactory : IItemFactory
    {
        public Weapon CreateWeapon()
        {
            return new Weapon("Простой меч", 10);
        }
        
        public Armor CreateArmor()
        {
            return new Armor("Простая броня", 5);
        }
        
        public Potion CreatePotion()
        {
            return new Potion("Зелье здоровья", 25);
        }
        
        public QuestItem CreateQuestItem()
        {
            return new QuestItem("Ключ от подземелья");
        }
    }
}