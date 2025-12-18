namespace DeliverySystem.Discounts
{
    //скидка по процентам
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private decimal percentage;

        public PercentageDiscountStrategy(decimal percentage)
        {
            this.percentage = percentage;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return amount * (1 - percentage / 100);
        }
    }
}