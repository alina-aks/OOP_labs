using System;
using System.Collections.Generic;
using DeliverySystem.Menu;
using DeliverySystem.States;

namespace DeliverySystem.Orders
{
    //логика всех заказов
    public abstract class Order : IOrder 
    {
        public int Id { get; }
        public List<MenuItem> Items { get; }
        public IOrderState State { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }

        private static int nextId = 1;

        public Order(string customerName, string address)
        {
            Id = nextId++;
            Items = new List<MenuItem>();
            CustomerName = customerName;
            Address = address;
            State = new PreparingState();
        }

        public void AddItem(MenuItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(MenuItem item)
        {
            Items.Remove(item);
        }

        public virtual decimal CalculateTotal()
        {
            decimal total = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                total += Items[i].Price;
            }
            return total;
        }

        public void ChangeState(IOrderState newState)
        {
            State = newState;
        }

        public void PrintStatus()
        {
            Console.WriteLine($"Заказ #{Id}: {State.GetStatus()}");
        }
    }
}