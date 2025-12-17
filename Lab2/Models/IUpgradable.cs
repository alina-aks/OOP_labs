namespace GameInventory.Models
{
    public interface IUpgradable
    {
        void Upgrade();
        int Level { get; }
    }
}