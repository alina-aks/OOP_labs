namespace DeliverySystem.States
{
    public class DeliveringState : IOrderState
    {
        public string GetStatus()
        {
            return "В доставке";
        }
    }
}