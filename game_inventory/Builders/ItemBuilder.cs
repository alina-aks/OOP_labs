public class ItemBuilder
{
    protected string name = string.Empty;
    protected int weight = 1;
    protected int value = 1;
    
    public ItemBuilder SetName(string name)
    {
        this.name = name;
        return this;
    }
    
    public ItemBuilder SetWeight(int weight)
    {
        this.weight = weight;
        return this;
    }
    
    public ItemBuilder SetValue(int value)
    {
        this.value = value;
        return this;
    }
    
    public virtual Item Build()
    {
        return new QuestItem(name, weight, value, "Квестовый предмет");
    }
}