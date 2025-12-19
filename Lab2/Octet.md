## Соблюдение принципов SOLID в лабораторной:
1. S - Single Responsibility (Принцип единственной ответственности)
   
   [Inventory.cs](Lab2/Inventories/Inventory.cs) - отвечает только за хранение и управление предметами
   
   [WeaponBuilder.cs](Lab2/Builders/WeaponBuilder.cs) - отвечает только за построение оружия
   
   [UseWeaponStrategy.cs](Lab2/Strategy/UseWeaponStrategy.cs) - отвечает только за использование оружия

2.  O - Open/Closed (Принцип открытости/закрытости)

       Strategy - можно добавлять новые стратегии, без изменения существующего кода 

3. L - Liskov Substitution (Принцип подстановки Барбары Лисков)
   объекты базового типа должны быть заменяемы объектами его наследников без изменения корректности программы

   Все наследники Item (Weapon, Armor, Potion, QuestItem) 
   

4.  I - Interface Segregation (Принцип разделения интерфейсов)

     [IUpgradable.cs](Lab2/Models/IUpgradable.cs) - отдельный интерфейс только для улучшения

    [IItemFactory.cs](Lab2/Factories/IItemFactory.cs) - отдельный интерфейс только для создания

5. D - Dependency Inversion (Принцип инверсии зависимостей)

   - Item зависит от IItemState, а не от конкретных классов:
     ```
      public IItemState State { get; set; }
      ```
      
      ```
       public void ChangeState(IItemState state)
        {
            State = state; 
        }
       ```

   - Стратегии работают через интерфейс:
     ```
     IUseStrategy strategy = new UseWeaponStrategy(); 
     strategy.Use(weapon); 
     ```
