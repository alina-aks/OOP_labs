using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine();
            machine.Run();
        }
    }

    class Product //карточка товара
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;

        }
    }

    class VendingMachine //основа
    {
        private List<Product> products; //список всех товаров
        private decimal collectedMoney; //заработанные деньги
        private decimal currentBalance; //баланс покупателя
        private bool isAdmineMode;

        public VendingMachine() //стартовая настройка автомата
        {
            products = new List<Product> //стартовый список продуктов 
            {
                new Product("Сок яблочный",60, 15), //название, цена, кол-во
                new Product("Чипсы",40, 10),
                new Product("Печенье",25, 10),
                new Product("Вода",35, 20),
                new Product("Липтон",60, 10),
            };

            currentBalance = 0;
            collectedMoney = 0;
            isAdmineMode = false;
        }
        public void Run() //запуск автомата
        {
            Console.WriteLine("Добро пожаловать!");

            while (true) // открываем либо пользовательское меню либо для админа
            {
                if (isAdmineMode)
                {
                    AdminMenu();
                }
                else
                {
                    UserMenu();
                }
            }
        }

        private void UserMenu() //меню покупателя
        {
            Console.WriteLine("-----Главная страница-----");
            Console.WriteLine("1. Выбор товаров");
            Console.WriteLine("2. Внести деньги");
            Console.WriteLine("3. Купить товар");
            Console.WriteLine("4. Вернуть внесенные деньги");
            Console.WriteLine("-----------------");
            Console.WriteLine($"Ваш текущий баланс: {currentBalance} руб.");
            Console.WriteLine("-----------------");
            Console.WriteLine("5. Режим администатора");
            Console.WriteLine("-----------------");
            Console.WriteLine("Выберите действие: ");

            string? choice = Console.ReadLine(); //считываем выбор покупателя

            switch (choice) //действие исходя из выбора
            {
                case "1":
                    ShowProducts();
                    break;
                case "2":
                    AddMoney();
                    break;
                case "3":
                    BuyProducts();
                    break;
                case "4":
                    ReturnMoney();
                    break;
                case "5":
                    CheckAdminPassword();
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }

        private void AdminMenu()
        {
            Console.WriteLine("-----Меню администратора-----");
            Console.WriteLine("1. Пополнение товаров");
            Console.WriteLine("2. Собрать деньги");
            Console.WriteLine("3. Режим покупателя");
            Console.WriteLine("-----------------");
            Console.WriteLine($"Собранная сумма: {collectedMoney} руб.");
            Console.WriteLine("-----------------");
            Console.WriteLine("Выберите действие: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProducts();
                    break;
                case "2":
                    CollectMoney();
                    break;
                case "3":
                    isAdmineMode = false;
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }

        }

        private void ShowProducts()
        {
            Console.WriteLine("------Доступные товары------");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} руб.");
                Console.WriteLine($"В наличии - {products[i].Quantity} шт.");
            }
        }

        private void AddMoney()
        {
            Console.WriteLine("Номиналы монет для внесения: 1, 2, 5, 10");
            Console.WriteLine("Введите сумму пополнения и внесите деньги:");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                currentBalance += amount;
                Console.WriteLine($"Ваш баланс пополнен на {amount} руб. Текущий баланс: {currentBalance} руб.");
            }
            else
            {
                Console.WriteLine("Ошибка ввода");
            }
        }

        private void BuyProducts()
        {
            ShowProducts();
            Console.WriteLine("Выберете номер товара, который хотите приобрести:");
            if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= products.Count)
            {
                Product selectedProduct = products[number - 1];

                if (selectedProduct.Quantity == 0)
                {
                    Console.WriteLine("Товар закончился");
                    return;
                }

                if (currentBalance >= selectedProduct.Price)
                {
                    selectedProduct.Quantity--;
                    decimal change = currentBalance - selectedProduct.Price;
                    collectedMoney = selectedProduct.Price;
                    currentBalance = 0;

                    Console.WriteLine($"Вы приобрели {selectedProduct.Name}");
                    Console.WriteLine($"Ваша сдача: {change} руб.");
                }

                else
                {
                    Console.WriteLine($"Недостаточно средств. Пополните баланс на {selectedProduct.Price - currentBalance} руб.");
                }
            }

            else
            {
                Console.WriteLine("Неверно выбран номер товара");
            }
        }

        private void ReturnMoney()
        {
            if (currentBalance > 0)
            {
                Console.WriteLine($"Возращено: {currentBalance} руб.");
                currentBalance = 0;
            }
            else
            {
                Console.WriteLine("Ваша баланс равен 0");
            }
        }

        private void CheckAdminPassword()
        {
            Console.WriteLine("Введите пароль для входа в режим администратора: _ _ _ _");
            string? password = Console.ReadLine();

            if (password == "1111")
            {
                isAdmineMode = true;
                Console.WriteLine("Вы вошли в режим администратора");
            }
            else
            {
                Console.WriteLine("Пароль не верный");
            }
        }

        private void AddProducts()
        {
            ShowProducts();
            Console.WriteLine("Выберите товар для пополнения:");

            if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= products.Count)
            {
                Console.WriteLine("Введите количество добавленного товара:");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    products[number - 1].Quantity += quantity;
                    Console.WriteLine($"Количество товара - {products[number + 1].Name} = {products[number + 1].Quantity} шт.");
                }
                else
                {
                    Console.WriteLine("Неверный ввод");
                }
            }
            else
            {
                Console.WriteLine("Такого товара не существует");
            }
        }

        private void CollectMoney()
        {
            Console.WriteLine($"Текущий баланс автомана: {collectedMoney} руб.");
            Console.WriteLine("Подтвердите снятие денег: 1 - да, 0 - нет");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice == 1)
            {
                Console.WriteLine($"Вы собрали {collectedMoney} руб. Текущий баланс автомана - 0 руб.");
                collectedMoney = 0;
            }
            else
            {
                Console.WriteLine("Отмена снятия.");
                return;
            }
        }
    }
}