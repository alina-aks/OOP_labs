public abstract class Item : IItem
{
    public string Name { get; protected set; }
    public int Weight { get; protected set; }
    public int Value { get; protected set; }
    
    protected Item(string name, int weight, int value)
    {
        Name = name;
        Weight = weight;
        Value = value;
    }
    
    public abstract string GetDescription();
}
