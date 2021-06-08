namespace POSTerminal.Discount
{
    public class VolumeDiscount
    {
        private readonly decimal _price;
        public readonly int Quantity;

        public VolumeDiscount(decimal price, int quantity)
        {
            _price = price;
            Quantity = quantity;
        }

        public decimal GetPrice(int itemsQuantity, decimal unitPrice) =>
            itemsQuantity / Quantity * _price + itemsQuantity % Quantity * unitPrice;
    }
}