using System;
using GameInventory.Models;
using GameInventory.Factory;
using GameInventory.Builder;
using GameInventory.Strategy;
using GameInventory.Inventories;

class Program
{
    static void Main(string[] args)
    {
        
        Inventory inventory = new Inventory();
        
        IItemFactory factory = new SimpleItemFactory();
        
        //создаем предметы
        Weapon weapon1 = factory.CreateWeapon();
        Armor armor1 = factory.CreateArmor();
        Potion potion1 = factory.CreatePotion();
        QuestItem questItem = factory.CreateQuestItem();
        
        //добавляем их в инвентарь
        inventory.AddItem(weapon1);
        inventory.AddItem(armor1);
        inventory.AddItem(potion1);
        inventory.AddItem(questItem);

        //делаем свое оружие
        WeaponBuilder builder = new WeaponBuilder();
        builder.SetName("Меч дракона");
        builder.SetDamage(25);
        Weapon customWeapon = builder.Build();
        inventory.AddItem(customWeapon);

        //показываем что есть в инвентаре
        inventory.PrintItems();

        //экипируем предметы
        Console.WriteLine("\nЭкипировка:");
        IUseStrategy weaponStrategy = new UseWeaponStrategy();
        weaponStrategy.Use(weapon1);
        
        IUseStrategy armorStrategy = new UseArmorStrategy();
        armorStrategy.Use(armor1);
        
        IUseStrategy potionStrategy = new UsePotionStrategy();
        potionStrategy.Use(potion1);
        
        //прокачиваем предметы
        Console.WriteLine("\nУлучшение предметов:");
        inventory.UpgradeItem(weapon1 as IUpgradable);
        inventory.UpgradeItem(armor1 as IUpgradable);
        
        //комбинируем
        Console.WriteLine("\nКомбинирование:");
        Weapon weapon2 = new Weapon("Старый кинжал", 5);
        inventory.AddItem(weapon2);
        
        
        Console.WriteLine("\nИнвентарь:");
        inventory.PrintItems();
        
        Console.WriteLine("\nСостояния предметов:");
        foreach (var item in inventory.GetAllItems())
        {
            Console.WriteLine($"{item.Name}: {item.State.GetStateName()}");
        }
    }
}