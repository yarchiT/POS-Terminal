namespace POSTerminal
{
    public class SaleItem
    {
        public readonly Product Product;
        private int _quantity = 1;

        public SaleItem(Product product)
        {
            Product = product;
        }

        public void Increment() => _quantity++;

        public decimal Total() =>
            Product.VolumeDiscount?.GetTotalPrice(_quantity, Product.Price) ?? Product.Price * _quantity;
    }
}