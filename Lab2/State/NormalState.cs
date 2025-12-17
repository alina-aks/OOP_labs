namespace GameInventory.State
{
    public class NormalState : IItemState
    {
        public string GetStateName()
        {
            return "Нормальное";
        }
    }
}