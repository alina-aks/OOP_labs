using GameInventory.Models;

namespace GameInventory.Builder
{
    public class WeaponBuilder
    {
        private string name = "";
        private int damage;

        public void SetName(string value)
        {
            name = value;
        }

        public void SetDamage(int value)
        {
            damage = value;
        }

        public Weapon Build()
        {
            return new Weapon(name, damage);
        }
    }
}
