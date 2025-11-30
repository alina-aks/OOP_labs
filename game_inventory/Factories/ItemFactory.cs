public static class ItemFactory
{
    public static Weapon CreateWeapon(string name, int damage, int weight = 5, int value = 50)
    {
        return new Weapon(name, weight, value, damage);
    }
    
    public static Armor CreateArmor(string name, int defense, int weight = 10, int value = 40)
    {
        return new Armor(name, weight, value, defense);
    }
    
    public static Potion CreatePotion(string name, string effect, int uses = 1, int weight = 1, int value = 20)
    {
        return new Potion(name, weight, value, effect, uses);
    }
    
    public static QuestItem CreateQuestItem(string name, string description, int weight = 1, int value = 1)
    {
        return new QuestItem(name, weight, value, description);
    }
}