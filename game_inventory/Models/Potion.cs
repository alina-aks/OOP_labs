public class Potion : Item, IUsable
{
    public string Effect { get; private set; }
    public int UsesRemaining { get; private set; }
    
    public Potion(string name, int weight, int value, string effect, int uses) 
        : base(name, weight, value)
    {
        Effect = effect;
        UsesRemaining = uses;
    }
    
    public void Use()
    {
        if (UsesRemaining > 0)
        {
            UsesRemaining--;
            Console.WriteLine($"Использовано {Name}: {Effect}");
        }
    }
    
    public override string GetDescription()
    {
        return $"{Name} - Эффект: {Effect}, Осталось использований: {UsesRemaining}, Вес: {Weight}";
    }
}