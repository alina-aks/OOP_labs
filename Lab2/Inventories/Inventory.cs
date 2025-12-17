using System;
using System.Collections.Generic;
using GameInventory.Models;

namespace GameInventory.Inventories
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        
        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"Добавлен предмет: {item.Name}");
        }
        
        public bool RemoveItem(Item item)
        {
            return items.Remove(item);
        }
        
        public int GetItemsCount()
        {
            return items.Count;
        }
        
        public List<Item> GetAllItems()
        {
            return new List<Item>(items);
        }
        
        public void PrintItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Пусто");
                return;
            }
            
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"- {items[i].GetInfo()}");
            }
        }
        
        public bool CombineWeapons(Weapon weapon1, Weapon weapon2)
        {
            if (items.Contains(weapon1) && items.Contains(weapon2))
            {
                items.Remove(weapon1);
                items.Remove(weapon2);
                
                string combinedName = $"Комбинированное {weapon1.Name}";
                int combinedDamage = weapon1.Damage + weapon2.Damage;
                
                Weapon combinedWeapon = new Weapon(combinedName, combinedDamage);
                items.Add(combinedWeapon);
                
                Console.WriteLine($"Создано новое оружие: {combinedName} (урон: {combinedDamage})");
                return true;
            }
            return false;
        }
        
        public bool UpgradeItem(IUpgradable item)
        {
            if (item != null)
            {
                item.Upgrade();
                Console.WriteLine($"Предмет улучшен до уровня {item.Level}");
                return true;
            }
            return false;
        }
    }
}