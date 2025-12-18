using DeliverySystem.Orders;

namespace DeliverySystem.Services
{
    //паттерн Observer 
    // убираем жесткое связывание, автоматическое обновление всех обьектов, уведомления
    public class NotificationService
    {
        public void NotifyOrderCreated(Order order)
        {
            System.Console.WriteLine($"Уведомление: Создан новый заказ #{order.Id} для {order.CustomerName}");
        }

        public void NotifyOrderStatusChanged(Order order)
        {
            System.Console.WriteLine($"Уведомление: Статус заказа #{order.Id} изменен");
        }
    }
}