using DeliverySystem.Orders;

namespace DeliverySystem.States
{
    public class OrderStateChanger
    {
        public void ChangeToPreparing(Order order)
        {
            order.ChangeState(new PreparingState());
        }

        public void ChangeToDelivering(Order order)
        {
            order.ChangeState(new DeliveringState());
        }

        public void ChangeToCompleted(Order order)
        {
            order.ChangeState(new CompletedState());
        }
    }
}