using System.Collections.Generic;
using DeliverySystem.Menu;

namespace DeliverySystem.Orders
{
    //экспресс заказы
    public class ExpressOrder : Order
    {
        public decimal ExpressFee { get; set; }
        
        public ExpressOrder(string customerName, string address, decimal expressFee = 200)
            : base(customerName, address)
        {
            ExpressFee = expressFee;
        }
        
        //считаем сумму заказа с учетом стоимости доставки
        public override decimal CalculateTotal()
        {
            decimal itemsTotal = 0;
            foreach (var item in Items)
            {
                itemsTotal += item.Price;
            }
            return itemsTotal + ExpressFee;
        }
    }
}