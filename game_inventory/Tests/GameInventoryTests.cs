using System;
using System.IO;
using Xunit;

public class GameInventoryTests
{
    [Fact]
    public void Inventory_AddRemoveItems_WorksCorrectly()
    {
        var inventory = new Inventory();
        var weapon = ItemFactory.CreateWeapon("Меч", 10);
        var armor = ItemFactory.CreateArmor("Броня", 5);
        
        inventory.AddItem(weapon);
        inventory.AddItem(armor);
        Assert.Equal(2, inventory.ItemCount);
        
        inventory.RemoveItem(weapon);
        Assert.Equal(1, inventory.ItemCount);
    }

    [Fact]
    public void Weapon_CreationAndUpgrade_WorksCorrectly()
    {
        var weapon = new Weapon("Меч", 5, 50, 15);
        
        Assert.Equal("Меч", weapon.Name);
        Assert.Equal(15, weapon.Damage);
        Assert.Equal(1, weapon.Level);
        
        weapon.Upgrade();
        Assert.Equal(2, weapon.Level);
        Assert.Equal(18, weapon.Damage);
    }

    [Fact]
    public void Armor_CreationAndUpgrade_WorksCorrectly()
    {
        var armor = new Armor("Броня", 10, 40, 8);
        
        Assert.Equal("Броня", armor.Name);
        Assert.Equal(8, armor.Defense);
        
        armor.Upgrade();
        Assert.Equal(2, armor.Level);
        Assert.Equal(9, armor.Defense);
    }

    [Fact]
    public void Potion_UseAndState_WorksCorrectly()
    {
        var potion = new Potion("Зелье", 1, 20, "Лечение", 2);
        
        Assert.Equal(2, potion.UsesRemaining);
        
        potion.Use();
        Assert.Equal(1, potion.UsesRemaining);
        
        potion.Use();
        Assert.Equal(0, potion.UsesRemaining);
    }

    [Fact]
    public void ItemFactory_CreateAllTypes_WorksCorrectly()
    {
        var weapon = ItemFactory.CreateWeapon("Меч", 10);
        var armor = ItemFactory.CreateArmor("Броня", 5);
        var potion = ItemFactory.CreatePotion("Зелье", "Лечение", 3);
        var questItem = ItemFactory.CreateQuestItem("Артефакт", "Важный квест");
        
        Assert.IsType<Weapon>(weapon);
        Assert.IsType<Armor>(armor);
        Assert.IsType<Potion>(potion);
        Assert.IsType<QuestItem>(questItem);
    }

    [Fact]
    public void EquipableItems_EquipUnequip_WorksCorrectly()
    {
        var weapon = ItemFactory.CreateWeapon("Меч", 10);
        var armor = ItemFactory.CreateArmor("Броня", 5);
        
        ((IEquipable)weapon).Equip();
        Assert.True(((Weapon)weapon).IsEquipped);
        
        ((IEquipable)weapon).Unequip();
        Assert.False(((Weapon)weapon).IsEquipped);
        
        ((IEquipable)armor).Equip();
        Assert.True(((Armor)armor).IsEquipped);
    }

    [Fact]
    public void Inventory_UseEquipUpgrade_IntegrationTest()
    {
        var inventory = new Inventory();
        var weapon = ItemFactory.CreateWeapon("Меч", 10);
        var potion = ItemFactory.CreatePotion("Зелье", "Лечение", 1);
        
        inventory.AddItem(weapon);
        inventory.AddItem(potion);
        
        inventory.EquipItem(0);
        Assert.True(((Weapon)weapon).IsEquipped);
        
        inventory.UpgradeItem(0);
        Assert.Equal(2, ((Weapon)weapon).Level);
        
        inventory.UseItem(1);
        Assert.Equal(0, ((Potion)potion).UsesRemaining);
    }

    [Fact]
    public void BuilderPattern_WeaponBuilder_CreatesWeapon()
    {
        var weapon = new WeaponBuilder()
            .SetName("Топор")
            .SetDamage(18)
            .SetWeight(8)
            .SetValue(75)
            .Build();
        
        Assert.IsType<Weapon>(weapon);
        Assert.Equal("Топор", weapon.Name);
        Assert.Equal(18, ((Weapon)weapon).Damage);
    }

    [Fact]
    public void StatePattern_PotionUsage_ChangesState()
    {
        var potion = new Potion("Зелье", 1, 20, "Лечение", 1);
        var inventory = new Inventory();
        inventory.AddItem(potion);
        
        inventory.UseItem(0);
        Assert.Equal(0, ((Potion)potion).UsesRemaining);
        
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        inventory.UseItem(0);
        var output = stringWriter.ToString();
        Assert.Contains("израсходован", output);
    }

    [Fact]
    public void StrategyPattern_UpgradeStrategy_CalculatesCost()
    {
        var strategy = new BasicUpgradeStrategy();
        var weapon = ItemFactory.CreateWeapon("Меч", 10);
        
        var cost = strategy.CalculateUpgradeCost((IUpgradable)weapon);
        
        Assert.Equal(100, cost);
    }
}