using System;
using DeliverySystem.Menu;
using DeliverySystem.Orders;
using DeliverySystem.States;
using DeliverySystem.Discounts;
using DeliverySystem.Services;

namespace DeliverySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--Управление заказами в службе доставки---\n");
            //создаем меню рестика
            RestaurantMenu menu = new RestaurantMenu();
            
            OrderService orderService = OrderService.GetInstance();
            NotificationService notificationService = new NotificationService();
            OrderStateChanger stateChanger = new OrderStateChanger();

            //содаем 1 заказ
            OrderBuilder builder = new OrderBuilder(menu);
            builder.CreateStandardOrder("Иван Иванов", "ул. Ленина, д. 10");
            builder.AddItem("Пицца");
            builder.AddItem("Кофе");
            
            //получаем готовый заказ
            Order order1 = builder.GetOrder();
            orderService.AddOrder(order1);
            notificationService.NotifyOrderCreated(order1);

            //применяем скидку
            IDiscountStrategy discountStrategy = new PercentageDiscountStrategy(10);
            decimal originalTotal = order1.CalculateTotal();
            decimal discountedTotal = discountStrategy.ApplyDiscount(originalTotal);


            //инфа о заказе
            Console.WriteLine($"Заказ #{order1.Id}:");
            Console.WriteLine($"Клиент: {order1.CustomerName}");
            Console.WriteLine($"Сумма заказа: {originalTotal} руб.");
            Console.WriteLine($"Сумма со скидкой 10%: {discountedTotal} руб.");


            //статусы заказа
            order1.PrintStatus();
            stateChanger.ChangeToDelivering(order1);
            order1.PrintStatus();
            notificationService.NotifyOrderStatusChanged(order1);

            //создаем 2 заказ
            builder.CreateExpressOrder("Анна Петрова", "ул. Пушкина, д. 5");
            builder.AddItem("Суши");
            builder.AddItem("Салат");

            //получаем готовый заказ
            Order order2 = builder.GetOrder();
            orderService.AddOrder(order2);
            notificationService.NotifyOrderCreated(order2);

            //инфа о заказе
            Console.WriteLine($"\nЗаказ #{order2.Id} (экспресс):");
            Console.WriteLine($"Клиент: {order2.CustomerName}");
            Console.WriteLine($"Сумма заказа: {order2.CalculateTotal()} руб.");

            //считаем скидку
            IDiscountStrategy fixedDiscount = new FixedDiscountStrategy(100);
            decimal fixedDiscountTotal = fixedDiscount.ApplyDiscount(order2.CalculateTotal());
            Console.WriteLine($"Сумма со скидкой 100 руб.: {fixedDiscountTotal} руб.");

            Console.WriteLine($"\nВсего заказов в системе: {orderService.GetAllOrders().Count}");
        }
    }
}