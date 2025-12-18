namespace DeliverySystem.Discounts
{
    public class NoDiscountStrategy : IDiscountStrategy
    {
        //та же сумма без изменений
        public decimal ApplyDiscount(decimal amount)
        {
            return amount;
        }
    }
}