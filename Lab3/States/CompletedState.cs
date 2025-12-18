namespace DeliverySystem.States
{
    public class CompletedState : IOrderState
    {
        public string GetStatus()
        {
            return "Выполнен";
        }
    }
}