using System.Collections.Generic;

namespace DeliverySystem.Menu
{
    //меню рестика
    public class RestaurantMenu
    {
        private List<MenuItem> items = new List<MenuItem>();

        public RestaurantMenu()
        {
            items.Add(new MenuItem("Пицца", 500, "Пицца маргарита"));
            items.Add(new MenuItem("Бургер", 300, "Бургер с курицей"));
            items.Add(new MenuItem("Салат", 200, "Овощной салат"));
            items.Add(new MenuItem("Суши", 400, "Суши филадельфия"));
            items.Add(new MenuItem("Кофе", 150, "Капучино"));
        }
        
        //чтобы пользователь не касался "оригинального" меню, не менял его
        public List<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>(items);
        }
        //поиск блюда 
        public MenuItem? GetItemByName(string name)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name == name)
                {
                    return items[i];
                }
            }
            return null;
        }
    }
}