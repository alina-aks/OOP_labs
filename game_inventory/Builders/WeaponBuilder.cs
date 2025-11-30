public class WeaponBuilder : ItemBuilder
{
    private int damage = 10;
    
    public WeaponBuilder SetDamage(int damage)
    {
        this.damage = damage;
        return this;
    }
    
    public override Item Build()
    {
        return new Weapon(name, weight, value, damage);
    }
}