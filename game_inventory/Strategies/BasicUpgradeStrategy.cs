public class BasicUpgradeStrategy : IUpgradeStrategy
{
    public void Upgrade(IUpgradable item)
    {
        item.Upgrade();
    }
    
    public int CalculateUpgradeCost(IUpgradable item)
    {
        return item.Level * 100;
    }
}