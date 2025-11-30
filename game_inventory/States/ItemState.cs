public abstract class ItemState
{
    public abstract void HandleUse(IUsable item);
    public abstract bool CanUse();
}

public class AvailableState : ItemState
{
    public override void HandleUse(IUsable item)
    {
        if (CanUse())
        {
            item.Use();
        }
    }
    
    public override bool CanUse() => true;
}

public class DepletedState : ItemState
{
    public override void HandleUse(IUsable item)
    {
        Console.WriteLine("Предмет израсходован!");
    }
    
    public override bool CanUse() => false;
}