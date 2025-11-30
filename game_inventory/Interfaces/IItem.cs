public interface IItem
{
    string Name { get; }
    int Weight { get; }
    int Value { get; }
    string GetDescription();
}