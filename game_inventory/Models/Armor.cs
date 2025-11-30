public class Armor : Item, IEquipable, IUpgradable
{
    public int Defense { get; private set; }
    public bool IsEquipped { get; private set; }
    public int Level { get; private set; }
    
    public Armor(string name, int weight, int value, int defense) 
        : base(name, weight, value)
    {
        Defense = defense;
        Level = 1;
    }
    
    public void Equip() => IsEquipped = true;
    public void Unequip() => IsEquipped = false;
    
    public void Upgrade()
    {
        Level++;
        Defense = (int)(Defense * 1.15);
        Value = (int)(Value * 1.3);
    }
    
    public override string GetDescription()
    {
        return $"{Name} (Уровень {Level}) - Защита: {Defense}, Вес: {Weight}, Ценность: {Value} {(IsEquipped ? "[Экипировано]" : "")}";
    }
}