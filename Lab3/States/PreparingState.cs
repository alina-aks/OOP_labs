namespace DeliverySystem.States
{
    public class PreparingState : IOrderState
    {
        public string GetStatus()
        {
            return "Готовится";
        }
    }
}