public class Weapon : Item, IEquipable, IUpgradable
{
    public int Damage { get; private set; }
    public bool IsEquipped { get; private set; }
    public int Level { get; private set; }
    
    public Weapon(string name, int weight, int value, int damage) 
        : base(name, weight, value)
    {
        Damage = damage;
        Level = 1;
    }
    
    public void Equip() => IsEquipped = true;
    public void Unequip() => IsEquipped = false;
    
    public void Upgrade()
    {
        Level++;
        Damage = (int)(Damage * 1.2);
        Value = (int)(Value * 1.5);
    }
    
    public override string GetDescription()
    {
        return $"{Name} (Уровень {Level}) - Урон: {Damage}, Вес: {Weight}, Ценность: {Value} {(IsEquipped ? "[Экипировано]" : "")}";
    }
}