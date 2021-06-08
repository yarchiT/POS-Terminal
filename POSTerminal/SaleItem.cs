using POSTerminal.Discount;

namespace POSTerminal
{
    public class SaleItem
    {
        public readonly Product Product;
        private int _quantity = 1;
        private DiscountCard? Discount { get; set; }

        public SaleItem(Product product)
        {
            Product = product;
        }

        public void ApplyDiscount(DiscountCard discountCard) =>
            Discount = discountCard;

        public void Increment() => _quantity++;

        public decimal Total() =>
            PriceWithDiscount() ?? Product.Price * _quantity;

        private decimal? PriceWithDiscount()
        {
            return Product.VolumeDiscount?.GetTotalPrice(_quantity, Product.Price) ??
                   Discount?.GetPrice(_quantity, Product.Price);
        }
    }
}