using System;
using Xunit;
using DeliverySystem.Menu;
using DeliverySystem.Orders;
using DeliverySystem.States;
using DeliverySystem.Discounts;
using DeliverySystem.Services;

namespace DeliverySystem.Tests
{
    public class MenuTests
    {
        [Fact]
        public void MenuItem_Creation_ShouldSetProperties()
        {
            // Arrange & Act
            var item = new MenuItem("Пицца", 500, "Итальянская пицца");
            
            // Assert
            Assert.Equal("Пицца", item.Name);
            Assert.Equal(500, item.Price);
            Assert.Equal("Итальянская пицца", item.Description);
        }
        
        [Fact]
        public void RestaurantMenu_GetItemByName_ShouldReturnCorrectItem()
        {
            // Arrange
            var menu = new RestaurantMenu();
            
            // Act
            var item = menu.GetItemByName("Пицца");
            
            // Assert
            Assert.NotNull(item);
            Assert.Equal("Пицца", item.Name);
            Assert.Equal(500, item.Price);
        }
        
        [Fact]
        public void RestaurantMenu_GetItemByName_ShouldReturnNullForNonExistent()
        {
            // Arrange
            var menu = new RestaurantMenu();
            
            // Act
            var item = menu.GetItemByName("Несуществующее блюдо");
            
            // Assert
            Assert.Null(item);
        }
        
        [Fact]
        public void RestaurantMenu_GetMenuItems_ShouldReturnCopy()
        {
            // Arrange
            var menu = new RestaurantMenu();
            
            // Act
            var items = menu.GetMenuItems();
            items.Clear(); // Очищаем копию
            
            // Assert
            Assert.Empty(items);
            // Оригинальный список не должен измениться
            Assert.NotEmpty(menu.GetMenuItems());
        }
    }
    
    public class OrderTests
    {
        [Fact]
        public void Order_Creation_ShouldGenerateUniqueIds()
        {
            // Arrange & Act
            var order1 = new StandardOrder("Иван", "ул. Ленина");
            var order2 = new StandardOrder("Петр", "ул. Пушкина");
            var order3 = new ExpressOrder("Анна", "ул. Гагарина");
            
            // Assert
            Assert.Equal(1, order1.Id);
            Assert.Equal(2, order2.Id);
            Assert.Equal(3, order3.Id);
        }
        
        [Fact]
        public void Order_AddItem_ShouldAddItemToOrder()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина");
            var menuItem = new MenuItem("Пицца", 500);
            
            // Act
            order.AddItem(menuItem);
            
            // Assert
            Assert.Single(order.Items);
            Assert.Equal("Пицца", order.Items[0].Name);
        }
        
        [Fact]
        public void Order_RemoveItem_ShouldRemoveItemFromOrder()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина");
            var menuItem = new MenuItem("Пицца", 500);
            order.AddItem(menuItem);
            
            // Act
            order.RemoveItem(menuItem);
            
            // Assert
            Assert.Empty(order.Items);
        }
        
        [Fact]
        public void Order_CalculateTotal_ShouldReturnCorrectSum()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина");
            order.AddItem(new MenuItem("Пицца", 500));
            order.AddItem(new MenuItem("Кофе", 150));
            
            // Act
            var total = order.CalculateTotal();
            
            // Assert
            Assert.Equal(650, total); // 500 + 150 (без доставки в базовом классе)
        }
        
        [Fact]
        public void StandardOrder_CalculateTotal_ShouldIncludeDeliveryFee()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина", 100);
            order.AddItem(new MenuItem("Пицца", 500));
            order.AddItem(new MenuItem("Кофе", 150));
            
            // Act
            var total = order.CalculateTotal();
            
            // Assert
            Assert.Equal(750, total); // 500 + 150 + 100
        }
        
        [Fact]
        public void ExpressOrder_CalculateTotal_ShouldIncludeExpressFee()
        {
            // Arrange
            var order = new ExpressOrder("Иван", "ул. Ленина", 200);
            order.AddItem(new MenuItem("Суши", 400));
            order.AddItem(new MenuItem("Салат", 200));
            
            // Act
            var total = order.CalculateTotal();
            
            // Assert
            Assert.Equal(800, total); // 400 + 200 + 200
        }
        
        [Fact]
        public void Order_ChangeState_ShouldUpdateState()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина");
            
            // Act
            order.ChangeState(new DeliveringState());
            
            // Assert
            Assert.IsType<DeliveringState>(order.State);
            Assert.Equal("В доставке", order.State.GetStatus());
        }
    }
    
    public class OrderBuilderTests
    {
        [Fact]
        public void OrderBuilder_CreateStandardOrder_ShouldCreateStandardOrder()
        {
            // Arrange
            var menu = new RestaurantMenu();
            var builder = new OrderBuilder(menu);
            
            // Act
            builder.CreateStandardOrder("Иван", "ул. Ленина");
            var order = builder.GetOrder();
            
            // Assert
            Assert.NotNull(order);
            Assert.IsType<StandardOrder>(order);
            Assert.Equal("Иван", order.CustomerName);
        }
        
        [Fact]
        public void OrderBuilder_CreateExpressOrder_ShouldCreateExpressOrder()
        {
            // Arrange
            var menu = new RestaurantMenu();
            var builder = new OrderBuilder(menu);
            
            // Act
            builder.CreateExpressOrder("Анна", "ул. Пушкина");
            var order = builder.GetOrder();
            
            // Assert
            Assert.NotNull(order);
            Assert.IsType<ExpressOrder>(order);
            Assert.Equal("Анна", order.CustomerName);
        }
        
        [Fact]
        public void OrderBuilder_AddItem_ShouldAddExistingMenuItem()
        {
            // Arrange
            var menu = new RestaurantMenu();
            var builder = new OrderBuilder(menu);
            builder.CreateStandardOrder("Иван", "ул. Ленина");
            
            // Act
            builder.AddItem("Пицца");
            var order = builder.GetOrder();
            
            // Assert
            Assert.NotNull(order);
            Assert.Single(order.Items);
            Assert.Equal("Пицца", order.Items[0].Name);
        }
        
        [Fact]
        public void OrderBuilder_AddItem_ShouldNotAddNonExistentItem()
        {
            // Arrange
            var menu = new RestaurantMenu();
            var builder = new OrderBuilder(menu);
            builder.CreateStandardOrder("Иван", "ул. Ленина");
            
            // Act
            builder.AddItem("Несуществующее блюдо");
            var order = builder.GetOrder();
            
            // Assert
            Assert.NotNull(order);
            Assert.Empty(order.Items);
        }
        
        [Fact]
        public void OrderBuilder_AddItem_WithoutCreatingOrder_ShouldNotCrash()
        {
            // Arrange
            var menu = new RestaurantMenu();
            var builder = new OrderBuilder(menu);
            // Не вызываем CreateStandardOrder!
            
            // Act & Assert (не должно быть исключения)
            var exception = Record.Exception(() => builder.AddItem("Пицца"));
            Assert.Null(exception);
        }
    }
    
    public class StateTests
    {
        [Fact]
        public void PreparingState_GetStatus_ShouldReturnCorrectText()
        {
            // Arrange
            var state = new PreparingState();
            
            // Act
            var status = state.GetStatus();
            
            // Assert
            Assert.Equal("В подготовке", status);
        }
        
        [Fact]
        public void DeliveringState_GetStatus_ShouldReturnCorrectText()
        {
            // Arrange
            var state = new DeliveringState();
            
            // Act
            var status = state.GetStatus();
            
            // Assert
            Assert.Equal("В доставке", status);
        }
        
        [Fact]
        public void CompletedState_GetStatus_ShouldReturnCorrectText()
        {
            // Arrange
            var state = new CompletedState();
            
            // Act
            var status = state.GetStatus();
            
            // Assert
            Assert.Equal("Выполнен", status);
        }
        
        [Fact]
        public void OrderStateChanger_ShouldChangeStatesCorrectly()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина");
            var changer = new OrderStateChanger();
            
            // Act & Assert 1
            changer.ChangeToDelivering(order);
            Assert.IsType<DeliveringState>(order.State);
            Assert.Equal("В доставке", order.State.GetStatus());
            
            // Act & Assert 2
            changer.ChangeToCompleted(order);
            Assert.IsType<CompletedState>(order.State);
            Assert.Equal("Выполнен", order.State.GetStatus());
            
            // Act & Assert 3
            changer.ChangeToPreparing(order);
            Assert.IsType<PreparingState>(order.State);
            Assert.Equal("В подготовке", order.State.GetStatus());
        }
    }
    
    public class DiscountTests
    {
        [Fact]
        public void NoDiscountStrategy_ShouldReturnSameAmount()
        {
            // Arrange
            var strategy = new NoDiscountStrategy();
            decimal amount = 1000m;
            
            // Act
            var result = strategy.ApplyDiscount(amount);
            
            // Assert
            Assert.Equal(amount, result);
        }
        
        [Fact]
        public void PercentageDiscountStrategy_ShouldApplyCorrectDiscount()
        {
            // Arrange
            var strategy = new PercentageDiscountStrategy(10); // 10%
            decimal amount = 1000m;
            
            // Act
            var result = strategy.ApplyDiscount(amount);
            
            // Assert
            Assert.Equal(900m, result); // 1000 * 0.9 = 900
        }
        
        [Fact]
        public void PercentageDiscountStrategy_WithZeroPercent_ShouldReturnSameAmount()
        {
            // Arrange
            var strategy = new PercentageDiscountStrategy(0); // 0%
            decimal amount = 1000m;
            
            // Act
            var result = strategy.ApplyDiscount(amount);
            
            // Assert
            Assert.Equal(amount, result);
        }
        
        [Fact]
        public void PercentageDiscountStrategy_With100Percent_ShouldReturnZero()
        {
            // Arrange
            var strategy = new PercentageDiscountStrategy(100); // 100%
            decimal amount = 1000m;
            
            // Act
            var result = strategy.ApplyDiscount(amount);
            
            // Assert
            Assert.Equal(0m, result); // 1000 * 0 = 0
        }
        
        [Fact]
        public void FixedDiscountStrategy_ShouldApplyCorrectDiscount()
        {
            // Arrange
            var strategy = new FixedDiscountStrategy(100);
            decimal amount = 1000m;
            
            // Act
            var result = strategy.ApplyDiscount(amount);
            
            // Assert
            Assert.Equal(900m, result); // 1000 - 100 = 900
        }
        
        [Fact]
        public void FixedDiscountStrategy_WithDiscountLargerThanAmount_ShouldReturnNegative()
        {
            // Arrange
            var strategy = new FixedDiscountStrategy(150);
            decimal amount = 100m;
            
            // Act
            var result = strategy.ApplyDiscount(amount);
            
            // Assert
            Assert.Equal(-50m, result); // 100 - 150 = -50
        }
        
        [Fact]
        public void FixedDiscountStrategy_WithZeroDiscount_ShouldReturnSameAmount()
        {
            // Arrange
            var strategy = new FixedDiscountStrategy(0);
            decimal amount = 1000m;
            
            // Act
            var result = strategy.ApplyDiscount(amount);
            
            // Assert
            Assert.Equal(amount, result);
        }
    }
    
    public class ServiceTests
    {
        [Fact]
        public void OrderService_Singleton_ShouldReturnSameInstance()
        {
            // Arrange & Act
            var instance1 = OrderService.GetInstance();
            var instance2 = OrderService.GetInstance();
            var instance3 = OrderService.GetInstance();
            
            // Assert
            Assert.Same(instance1, instance2);
            Assert.Same(instance2, instance3);
            Assert.Same(instance1, instance3);
        }
        
        [Fact]
        public void OrderService_AddOrder_ShouldStoreOrder()
        {
            // Arrange
            var service = OrderService.GetInstance();
            var order = new StandardOrder("Иван", "ул. Ленина");
            
            // Act
            service.AddOrder(order);
            var allOrders = service.GetAllOrders();
            
            // Assert
            Assert.Contains(order, allOrders);
        }
        
        [Fact]
        public void OrderService_GetAllOrders_ShouldReturnCopy()
        {
            // Arrange
            var service = OrderService.GetInstance();
            var order = new StandardOrder("Иван", "ул. Ленина");
            service.AddOrder(order);
            
            // Act
            var ordersCopy = service.GetAllOrders();
            ordersCopy.Clear(); // Очищаем копию
            
            // Assert
            Assert.Empty(ordersCopy);
            // Оригинальный список не должен измениться
            Assert.NotEmpty(service.GetAllOrders());
        }
        
        [Fact]
        public void OrderService_GetOrderById_ShouldReturnCorrectOrder()
        {
            // Arrange
            var service = OrderService.GetInstance();
            var order1 = new StandardOrder("Иван", "ул. Ленина");
            var order2 = new StandardOrder("Петр", "ул. Пушкина");
            service.AddOrder(order1);
            service.AddOrder(order2);
            
            // Act
            var foundOrder = service.GetOrderById(order2.Id);
            
            // Assert
            Assert.NotNull(foundOrder);
            Assert.Equal(order2.Id, foundOrder.Id);
            Assert.Equal("Петр", foundOrder.CustomerName);
        }
        
        [Fact]
        public void OrderService_GetOrderById_ShouldReturnNullForNonExistentId()
        {
            // Arrange
            var service = OrderService.GetInstance();
            
            // Act
            var foundOrder = service.GetOrderById(999); // Несуществующий ID
            
            // Assert
            Assert.Null(foundOrder);
        }
        
        [Fact]
        public void NotificationService_NotifyOrderCreated_ShouldNotThrow()
        {
            // Arrange
            var service = new NotificationService();
            var order = new StandardOrder("Иван", "ул. Ленина");
            
            // Act & Assert (не должно быть исключения)
            var exception = Record.Exception(() => service.NotifyOrderCreated(order));
            Assert.Null(exception);
        }
        
        [Fact]
        public void NotificationService_NotifyOrderStatusChanged_ShouldNotThrow()
        {
            // Arrange
            var service = new NotificationService();
            var order = new StandardOrder("Иван", "ул. Ленина");
            
            // Act & Assert (не должно быть исключения)
            var exception = Record.Exception(() => service.NotifyOrderStatusChanged(order));
            Assert.Null(exception);
        }
    }
    
    public class IntegrationTests
    {
        [Fact]
        public void CompleteOrderFlow_ShouldWorkCorrectly()
        {
            // Arrange
            var menu = new RestaurantMenu();
            var builder = new OrderBuilder(menu);
            var orderService = OrderService.GetInstance();
            var stateChanger = new OrderStateChanger();
            var discountStrategy = new PercentageDiscountStrategy(10);
            
            // Act - создаем заказ
            builder.CreateExpressOrder("Иван", "ул. Ленина");
            builder.AddItem("Пицца");
            builder.AddItem("Кофе");
            var order = builder.GetOrder();
            
            // Assert 1 - проверяем создание
            Assert.NotNull(order);
            Assert.IsType<ExpressOrder>(order);
            Assert.Equal("Иван", order.CustomerName);
            Assert.Equal(2, order.Items.Count);
            
            // Act - добавляем в сервис
            orderService.AddOrder(order);
            
            // Assert 2 - проверяем хранение
            var storedOrder = orderService.GetOrderById(order.Id);
            Assert.NotNull(storedOrder);
            Assert.Same(order, storedOrder);
            
            // Act - применяем скидку
            var originalTotal = order.CalculateTotal();
            var discountedTotal = discountStrategy.ApplyDiscount(originalTotal);
            
            // Assert 3 - проверяем расчеты
            // Пицца(500) + Кофе(150) + Экспресс(200) = 850
            // 10% скидка = 850 * 0.9 = 765
            Assert.Equal(850, originalTotal);
            Assert.Equal(765, discountedTotal);
            
            // Act - меняем состояния
            Assert.IsType<PreparingState>(order.State);
            stateChanger.ChangeToDelivering(order);
            
            // Assert 4 - проверяем состояния
            Assert.IsType<DeliveringState>(order.State);
            Assert.Equal("В доставке", order.State.GetStatus());
        }
        
        [Fact]
        public void MultipleOrders_ShouldHaveUniqueIds()
        {
            // Arrange
            var order1 = new StandardOrder("Иван", "ул. Ленина");
            var order2 = new ExpressOrder("Петр", "ул. Пушкина");
            var order3 = new StandardOrder("Анна", "ул. Гагарина");
            var order4 = new ExpressOrder("Мария", "ул. Лермонтова");
            
            // Assert
            Assert.Equal(1, order1.Id);
            Assert.Equal(2, order2.Id);
            Assert.Equal(3, order3.Id);
            Assert.Equal(4, order4.Id);
            Assert.NotEqual(order1.Id, order2.Id);
            Assert.NotEqual(order2.Id, order3.Id);
            Assert.NotEqual(order3.Id, order4.Id);
        }
        
        [Fact]
        public void OrderWithDifferentDiscountStrategies_ShouldCalculateCorrectly()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина", 100);
            order.AddItem(new MenuItem("Пицца", 500));
            order.AddItem(new MenuItem("Кофе", 150));
            
            // Original: 500 + 150 + 100 = 750
            
            // Act & Assert 1 - без скидки
            var noDiscount = new NoDiscountStrategy();
            Assert.Equal(750, noDiscount.ApplyDiscount(750));
            
            // Act & Assert 2 - 10% скидка
            var percentageDiscount = new PercentageDiscountStrategy(10);
            Assert.Equal(675, percentageDiscount.ApplyDiscount(750)); // 750 * 0.9 = 675
            
            // Act & Assert 3 - фиксированная скидка 100
            var fixedDiscount = new FixedDiscountStrategy(100);
            Assert.Equal(650, fixedDiscount.ApplyDiscount(750)); // 750 - 100 = 650
            
            // Act & Assert 4 - 50% скидка
            var halfDiscount = new PercentageDiscountStrategy(50);
            Assert.Equal(375, halfDiscount.ApplyDiscount(750)); // 750 * 0.5 = 375
        }
    }
    
    public class EdgeCaseTests
    {
        [Fact]
        public void EmptyOrder_ShouldCalculateCorrectly()
        {
            // Arrange
            var standardOrder = new StandardOrder("Иван", "ул. Ленина", 100);
            var expressOrder = new ExpressOrder("Петр", "ул. Пушкина", 200);
            
            // Act & Assert - стандартный заказ без блюд
            Assert.Equal(100, standardOrder.CalculateTotal()); // только доставка
            
            // Act & Assert - экспресс заказ без блюд
            Assert.Equal(200, expressOrder.CalculateTotal()); // только экспресс-доставка
        }
        
        [Fact]
        public void OrderWithManyItems_ShouldCalculateCorrectly()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина", 100);
            
            // Добавляем 10 одинаковых блюд
            for (int i = 0; i < 10; i++)
            {
                order.AddItem(new MenuItem("Кофе", 150));
            }
            
            // Act
            var total = order.CalculateTotal();
            
            // Assert
            Assert.Equal(1600, total); // (150 * 10) + 100 = 1600
        }
        
        [Fact]
        public void OrderState_ShouldSurviveSerialChanges()
        {
            // Arrange
            var order = new StandardOrder("Иван", "ул. Ленина");
            var changer = new OrderStateChanger();
            
            // Act - много раз меняем состояния
            changer.ChangeToDelivering(order);
            changer.ChangeToCompleted(order);
            changer.ChangeToPreparing(order);
            changer.ChangeToDelivering(order);
            changer.ChangeToCompleted(order);
            
            // Assert
            Assert.IsType<CompletedState>(order.State);
            Assert.Equal("Выполнен", order.State.GetStatus());
        }
        
        [Fact]
        public void OrderService_WithManyOrders_ShouldFindCorrectly()
        {
            // Arrange
            var service = OrderService.GetInstance();
            
            // Создаем 5 заказов
            for (int i = 0; i < 5; i++)
            {
                var order = new StandardOrder($"Клиент{i}", $"Адрес{i}");
                service.AddOrder(order);
            }
            
            // Act & Assert - ищем каждый заказ
            for (int i = 1; i <= 5; i++)
            {
                var foundOrder = service.GetOrderById(i);
                Assert.NotNull(foundOrder);
                Assert.Equal(i, foundOrder.Id);
                Assert.Equal($"Клиент{i-1}", foundOrder.CustomerName);
            }
            
            // Act & Assert - несуществующий заказ
            var nonExistent = service.GetOrderById(999);
            Assert.Null(nonExistent);
        }
    }
}