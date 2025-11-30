using System;

class Program
{
    static void Main(string[] args)
    {
        var inventory = new Inventory();
        
        // Добавляем несколько начальных предметов для демонстрации
        var sword = ItemFactory.CreateWeapon("Стальной меч", 15);
        var armor = ItemFactory.CreateArmor("Кожаный доспех", 8);
        var healthPotion = ItemFactory.CreatePotion("Зелье здоровья", "Восстанавливает 50 HP", 3);
        var questItem = ItemFactory.CreateQuestItem("Древний артефакт", "Принесите его мудрецу");
        
        inventory.AddItem(sword);
        inventory.AddItem(armor);
        inventory.AddItem(healthPotion);
        inventory.AddItem(questItem);
        
        // Создаем дополнительное оружие через фабрику
        var axe = ItemFactory.CreateWeapon("Боевой топор", 18);
        inventory.AddItem(axe);
        
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== СИСТЕМА УПРАВЛЕНИЯ ИНВЕНТАРЕМ ===");
            Console.WriteLine("1. Показать инвентарь");
            Console.WriteLine("2. Использовать предмет");
            Console.WriteLine("3. Экипировать/снять предмет");
            Console.WriteLine("4. Улучшить предмет");
            Console.WriteLine("5. Добавить новый предмет");
            Console.WriteLine("6. Выйти");
            Console.Write("Выберите действие: ");
            
            string? choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    inventory.DisplayInventory();
                    break;
                    
                case "2":
                    UseItemMenu(inventory);
                    break;
                    
                case "3":
                    EquipItemMenu(inventory);
                    break;
                    
                case "4":
                    UpgradeItemMenu(inventory);
                    break;
                    
                case "5":
                    AddItemMenu(inventory);
                    break;
                    
                case "6":
                    running = false;
                    break;
                    
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
    
    static void UseItemMenu(Inventory inventory)
    {
        inventory.DisplayInventory();
        if (inventory.ItemCount > 0)
        {
            Console.Write("Введите номер предмета для использования: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                inventory.UseItem(index - 1);
        }
    }
    
    static void EquipItemMenu(Inventory inventory)
    {
        inventory.DisplayInventory();
        if (inventory.ItemCount > 0)
        {
            Console.Write("Введите номер предмета для экипировки/снятия: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                inventory.EquipItem(index - 1);
        }
    }
    
    static void UpgradeItemMenu(Inventory inventory)
    {
        inventory.DisplayInventory();
        if (inventory.ItemCount > 0)
        {
            Console.Write("Введите номер предмета для улучшения: ");
            if (int.TryParse(Console.ReadLine(), out int index))
                inventory.UpgradeItem(index - 1);
        }
    }
    
    static void AddItemMenu(Inventory inventory)
    {
        Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОГО ПРЕДМЕТА ===");
        Console.WriteLine("1. Оружие");
        Console.WriteLine("2. Броня");
        Console.WriteLine("3. Зелье");
        Console.WriteLine("4. Квестовый предмет");
        Console.Write("Выберите тип предмета: ");
        
        string? typeChoice = Console.ReadLine();
        
        switch (typeChoice)
        {
            case "1": // Оружие
                AddWeaponMenu(inventory);
                break;
                
            case "2": // Броня
                AddArmorMenu(inventory);
                break;
                
            case "3": // Зелье
                AddPotionMenu(inventory);
                break;
                
            case "4": // Квестовый предмет
                AddQuestItemMenu(inventory);
                break;
                
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }
    
    static void AddWeaponMenu(Inventory inventory)
    {
        Console.Write("Введите название оружия: ");
        string? name = Console.ReadLine();
        Console.Write("Введите урон оружия: ");
        if (int.TryParse(Console.ReadLine(), out int damage))
        {
            Console.Write("Введите вес оружия (по умолчанию 5): ");
            int weight = int.TryParse(Console.ReadLine(), out int w) ? w : 5;
            Console.Write("Введите ценность оружия (по умолчанию 50): ");
            int value = int.TryParse(Console.ReadLine(), out int v) ? v : 50;
            
            var weapon = ItemFactory.CreateWeapon(name ?? "Безымянное оружие", damage, weight, value);
            inventory.AddItem(weapon);
            Console.WriteLine($"Оружие '{weapon.Name}' добавлено в инвентарь!");
        }
    }
    
    static void AddArmorMenu(Inventory inventory)
    {
        Console.Write("Введите название брони: ");
        string? name = Console.ReadLine();
        Console.Write("Введите защиту брони: ");
        if (int.TryParse(Console.ReadLine(), out int defense))
        {
            Console.Write("Введите вес брони (по умолчанию 10): ");
            int weight = int.TryParse(Console.ReadLine(), out int w) ? w : 10;
            Console.Write("Введите ценность брони (по умолчанию 40): ");
            int value = int.TryParse(Console.ReadLine(), out int v) ? v : 40;
            
            var armor = ItemFactory.CreateArmor(name ?? "Безымянная броня", defense, weight, value);
            inventory.AddItem(armor);
            Console.WriteLine($"Броня '{armor.Name}' добавлена в инвентарь!");
        }
    }
    
    static void AddPotionMenu(Inventory inventory)
    {
        Console.Write("Введите название зелья: ");
        string? name = Console.ReadLine();
        Console.Write("Введите эффект зелья: ");
        string? effect = Console.ReadLine();
        Console.Write("Введите количество использований (по умолчанию 1): ");
        int uses = int.TryParse(Console.ReadLine(), out int u) ? u : 1;
        Console.Write("Введите вес зелья (по умолчанию 1): ");
        int weight = int.TryParse(Console.ReadLine(), out int w) ? w : 1;
        Console.Write("Введите ценность зелья (по умолчанию 20): ");
        int value = int.TryParse(Console.ReadLine(), out int v) ? v : 20;
        
        var potion = ItemFactory.CreatePotion(
            name ?? "Безымянное зелье", 
            effect ?? "Неизвестный эффект", 
            uses, weight, value);
        inventory.AddItem(potion);
        Console.WriteLine($"Зелье '{potion.Name}' добавлено в инвентарь!");
    }
    
    static void AddQuestItemMenu(Inventory inventory)
    {
        Console.Write("Введите название квестового предмета: ");
        string? name = Console.ReadLine();
        Console.Write("Введите описание квеста: ");
        string? description = Console.ReadLine();
        Console.Write("Введите вес предмета (по умолчанию 1): ");
        int weight = int.TryParse(Console.ReadLine(), out int w) ? w : 1;
        Console.Write("Введите ценность предмета (по умолчанию 1): ");
        int value = int.TryParse(Console.ReadLine(), out int v) ? v : 1;
        
        var questItem = ItemFactory.CreateQuestItem(
            name ?? "Безымянный предмет", 
            description ?? "Неизвестное назначение", 
            weight, value);
        inventory.AddItem(questItem);
        Console.WriteLine($"Квестовый предмет '{questItem.Name}' добавлен в инвентарь!");
    }
}