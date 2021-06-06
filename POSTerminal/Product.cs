using POSTerminal.Discount;

namespace POSTerminal
{
    public class Product
    {
        public readonly string Code;
        public readonly decimal Price;
        public readonly VolumeDiscount? VolumeDiscount;

        public Product(string code, decimal price, VolumeDiscount? volumeDiscount = null)
        {
            Code = code;
            Price = price;
            VolumeDiscount = volumeDiscount;
        }
    }
}