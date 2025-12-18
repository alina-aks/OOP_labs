using DeliverySystem.Menu;

namespace DeliverySystem.Orders
{
    // паттерн Builder
    // используем чтобы создавать сложные объекты
    public class OrderBuilder
    {
        private Order order;
        private RestaurantMenu restaurantMenu;

        public OrderBuilder(RestaurantMenu menu)
        {
            this.restaurantMenu = menu;
        }

        public void CreateStandardOrder(string customerName, string address)
        {
            order = new StandardOrder(customerName, address);
        }

        public void CreateExpressOrder(string customerName, string address)
        {
            order = new ExpressOrder(customerName, address);
        }

        public void AddItem(string itemName)
        {
            if (order == null) return;
            var menuItem = restaurantMenu.GetItemByName(itemName);
            if (menuItem != null)
            {
                order.AddItem(menuItem);
            }
        }

        public Order GetOrder()
        {
            return order;
        }
    }
}