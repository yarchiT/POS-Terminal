namespace POSTerminal
{
    public class Price
    {
        public readonly decimal UnitPrice;
        public readonly decimal VolumePrice;
        public decimal Total => VolumePrice + UnitPrice;

        public Price(decimal unitPrice, decimal volumePrice = 0)
        {
            UnitPrice = unitPrice;
            VolumePrice = volumePrice;
        }
    }
}