namespace GameInventory.State
{
    public class EquippedState : IItemState
    {
        public string GetStateName()
        {
            return "Используется";
        }
    }
}