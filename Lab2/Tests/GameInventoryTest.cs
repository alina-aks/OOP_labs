using NUnit.Framework;
using System;
using System.IO;
using GameInventory.Models;
using GameInventory.Factory;
using GameInventory.Builder;
using GameInventory.Strategy;
using GameInventory.Inventories;
using GameInventory.State;

namespace GameInventory.Tests
{
    [TestFixture]
    public class InventorySystemTest
    {
        private StringWriter _output;
        private TextWriter _originalOutput;

        [SetUp]
        public void Setup()
        {
            _output = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_output);
        }

        [TearDown]
        public void Cleanup()
        {
            Console.SetOut(_originalOutput);
            _output.Dispose();
        }

        [Test]
        public void TestCompleteInventorySystem()
        {
            var inventory = new Inventory();

            var factory = new SimpleItemFactory();
            var weapon = factory.CreateWeapon();
            var armor = factory.CreateArmor();
            var potion = factory.CreatePotion();
            var questItem = factory.CreateQuestItem();

            inventory.AddItem(weapon);
            inventory.AddItem(armor);
            inventory.AddItem(potion);
            inventory.AddItem(questItem);

            Assert.That(inventory.GetItemsCount(), Is.EqualTo(4));

            var builder = new WeaponBuilder();
            builder.SetName("Драконий меч");
            builder.SetDamage(30);
            var customWeapon = builder.Build();
            inventory.AddItem(customWeapon);

            Assert.That(inventory.GetItemsCount(), Is.EqualTo(5));

            var weaponStrategy = new UseWeaponStrategy();
            weaponStrategy.Use(weapon);
            Assert.That(weapon.State.GetStateName(), Is.EqualTo("Equipped"));

            var armorStrategy = new UseArmorStrategy();
            armorStrategy.Use(armor);
            Assert.That(armor.State.GetStateName(), Is.EqualTo("Equipped"));

            var potionStrategy = new UsePotionStrategy();
            potionStrategy.Use(potion);

            var upgradeResult1 = inventory.UpgradeItem(weapon as IUpgradable);
            var upgradeResult2 = inventory.UpgradeItem(armor as IUpgradable);

            Assert.That(upgradeResult1, Is.True);
            Assert.That(upgradeResult2, Is.True);
            Assert.That(weapon.Level, Is.EqualTo(2));
            Assert.That(armor.Level, Is.EqualTo(2));

            var weaponForCombine = new Weapon("Старый клинок", 8);
            inventory.AddItem(weaponForCombine);

            var combineResult = inventory.CombineWeapons(weaponForCombine, customWeapon);
            Assert.That(combineResult, Is.True);
            Assert.That(inventory.GetItemsCount(), Is.EqualTo(5));

            foreach (var item in inventory.GetAllItems())
            {
                Assert.That(item.Name, Is.Not.Empty);
                Assert.That(item.State, Is.Not.Null);
            }

            inventory.PrintItems();
            var consoleOutput = _output.ToString();
            Assert.That(consoleOutput, Contains.Substring("ИНВЕНТАРЬ") | Contains.Substring("Драконий") | Contains.Substring("Простой"));
        }
    }
}