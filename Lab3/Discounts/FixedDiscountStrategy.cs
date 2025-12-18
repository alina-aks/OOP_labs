namespace DeliverySystem.Discounts
{
    //фиксированная скидка
    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private decimal discountAmount;

        public FixedDiscountStrategy(decimal discountAmount)
        {
            this.discountAmount = discountAmount;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return amount - discountAmount;
        }
    }
}