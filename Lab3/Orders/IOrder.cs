using System.Collections.Generic;
using DeliverySystem.Menu;

namespace DeliverySystem.Orders
{
    public interface IOrder
    {
        int Id { get; }
        List<MenuItem> Items { get; }
        decimal CalculateTotal();
        void AddItem(MenuItem item);
        void RemoveItem(MenuItem item);
    }
}