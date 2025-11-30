public interface IEquipable
{
    void Equip();
    void Unequip();
    bool IsEquipped { get; }
}