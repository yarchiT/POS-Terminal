namespace POSTerminal.Discount
{
    public class VolumeDiscount
    {
        private readonly decimal Price;
        private readonly int Quantity;

        public VolumeDiscount(decimal price, int quantity)
        {
            Price = price;
            Quantity = quantity;
        }

        public decimal GetPrice(int itemsQuantity, decimal unitPrice) =>
            itemsQuantity / Quantity * Price + itemsQuantity % Quantity * unitPrice;
    }
}