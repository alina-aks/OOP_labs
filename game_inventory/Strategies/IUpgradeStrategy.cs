public interface IUpgradeStrategy
{
    void Upgrade(IUpgradable item);
    int CalculateUpgradeCost(IUpgradable item);
}