using POSTerminal.Discount;

namespace POSTerminal
{
    public class SaleItem
    {
        public readonly Product Product;
        public int Quantity { get; private set; } = 1;
        private DiscountCard? Discount { get; set; }

        public SaleItem(Product product)
        {
            Product = product;
        }

        public void ApplyDiscount(DiscountCard discountCard) =>
            Discount = discountCard;

        public void Increment() => Quantity++;

        public decimal Total() =>
            PriceWithDiscount() ?? Product.Price * Quantity;

        private decimal? PriceWithDiscount()
        {
            return Product.VolumeDiscount?.GetPrice(Quantity, Product.Price) ??
                   Discount?.GetPrice(Quantity, Product.Price);
        }
    }
}