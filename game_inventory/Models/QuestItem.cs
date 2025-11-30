public class QuestItem : Item
{
    public string QuestDescription { get; private set; }
    
    public QuestItem(string name, int weight, int value, string questDescription) 
        : base(name, weight, value)
    {
        QuestDescription = questDescription;
    }
    
    public override string GetDescription()
    {
        return $"{Name} - {QuestDescription}, Вес: {Weight}";
    }
}