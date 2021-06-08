using POSTerminal.Discount;

namespace POSTerminal
{
    public class SaleItem
    {
        public readonly Product Product;
        public int Quantity { get; private set; } = 1;

        public SaleItem(Product product)
        {
            Product = product;
        }

        public void Increment() => Quantity++;

        public decimal Total(DiscountCard? discountCard = null) =>
            PriceWithDiscount(discountCard) ?? Product.Price * Quantity;

        private decimal? PriceWithDiscount(DiscountCard? discountCard)
        {
            return Product.IsVolumeDiscountValidFor(Quantity)
                ? Product.VolumeDiscount?.GetPrice(Quantity, Product.Price)
                : discountCard?.GetPrice(Quantity, Product.Price);
        }
    }
}