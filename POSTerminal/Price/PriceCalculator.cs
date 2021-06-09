namespace POSTerminal
{
    public static class PriceCalculator
    {
        public static Price Get(Product product, int quantity)
        {
            if (!product.IsVolumeDiscountValidFor(quantity))
                return new Price(product.Price * quantity, 0);

            var volumePrice = quantity / product.VolumeDiscount!.Quantity * product.VolumeDiscount.Price;
            var unitPrice = quantity % product.VolumeDiscount!.Quantity * product.Price;

            return new Price(unitPrice, volumePrice);
        }
    }
}