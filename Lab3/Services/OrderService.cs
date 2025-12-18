using System.Collections.Generic;
using DeliverySystem.Orders;

namespace DeliverySystem.Services
{
    //хранилище всех заказов
    public class OrderService
    {
        private List<Order> orders = new List<Order>();
        private static OrderService? instance;

        // Singleton Pattern
        // чтобы во всей системе было только одно хранилище заказов
        private OrderService() { }

        public static OrderService GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderService();
            }
            return instance;
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public List<Order> GetAllOrders()
        {
            return new List<Order>(orders);
        }

        public Order? GetOrderById(int id)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].Id == id)
                {
                    return orders[i];
                }
            }
            return null;
        }
    }
}