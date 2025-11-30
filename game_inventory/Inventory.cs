using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private List<Item> items = new List<Item>();
    private IUpgradeStrategy upgradeStrategy = new BasicUpgradeStrategy();
    
    public void AddItem(Item item) => items.Add(item);
    
    public void RemoveItem(Item item) => items.Remove(item);
    
    public void DisplayInventory()
    {
        Console.WriteLine("\n=== ИНВЕНТАРЬ ===");
        if (!items.Any())
        {
            Console.WriteLine("Инвентарь пуст");
            return;
        }
        
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i].GetDescription()}");
        }
    }
    
    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            var item = items[index];
            if (item is IUsable usable)
            {
                ItemState state;
                if (usable.UsesRemaining > 0)
                {
                    state = new AvailableState();
                }
                else
                {
                    state = new DepletedState();
                }
                
                state.HandleUse(usable);
                
                if (usable.UsesRemaining == 0)
                {
                    Console.WriteLine($"{item.Name} израсходован!");
                }
            }
            else
            {
                Console.WriteLine("Этот предмет нельзя использовать");
            }
        }
    }
    
    public void EquipItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            var item = items[index];
            if (item is IEquipable equipable)
            {
                if (!equipable.IsEquipped)
                {
                    equipable.Equip();
                    Console.WriteLine($"{item.Name} экипировано");
                }
                else
                {
                    equipable.Unequip();
                    Console.WriteLine($"{item.Name} снято");
                }
            }
            else
            {
                Console.WriteLine("Этот предмет нельзя экипировать");
            }
        }
    }
    
    public void UpgradeItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            var item = items[index];
            if (item is IUpgradable upgradable)
            {
                int cost = upgradeStrategy.CalculateUpgradeCost(upgradable);
                Console.WriteLine($"Улучшение {item.Name} будет стоить {cost} золота");
                upgradeStrategy.Upgrade(upgradable);
                Console.WriteLine($"{item.Name} улучшено до уровня {upgradable.Level}");
            }
            else
            {
                Console.WriteLine("Этот предмет нельзя улучшить");
            }
        }
    }
    
    public Item GetItem(int index) => items[index];
    public int ItemCount => items.Count;
}