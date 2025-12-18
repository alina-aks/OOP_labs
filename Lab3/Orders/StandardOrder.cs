using System.Collections.Generic;
using DeliverySystem.Menu;

namespace DeliverySystem.Orders
{
    //стандартная доставка
    public class StandardOrder : Order
    {
        public decimal DeliveryFee { get; set; }

        public StandardOrder(string customerName, string address, decimal deliveryFee = 100)
            : base(customerName, address)
        {
            DeliveryFee = deliveryFee;
        }

        //считаем сумму заказа с учетом стоимости доставки
        public override decimal CalculateTotal()
        {
            decimal itemsTotal = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                itemsTotal += Items[i].Price;
            }
            return itemsTotal + DeliveryFee;
        }
    }
}