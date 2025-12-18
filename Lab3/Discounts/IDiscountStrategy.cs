namespace DeliverySystem.Discounts
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal amount);
    }
}