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

        public Price GetPrice() =>
            PriceCalculator.Get(Product, Quantity);
    }
}