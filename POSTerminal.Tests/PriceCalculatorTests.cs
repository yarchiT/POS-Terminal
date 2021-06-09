using POSTerminal.Discount;
using Xunit;

namespace POSTerminal.Tests
{
    public class PriceCalculatorTests
    {
        [Fact]
        public void Get_ProductWithVolumeDiscount_ReturnsCorrectPrices()
        {
            var product = new Product("A", 10, new VolumeDiscount(20, 3));

            var price = PriceCalculator.Get(product, 3);

            Assert.Equal(20, price.Total);
            Assert.Equal(0, price.UnitPrice);
            Assert.Equal(20, price.VolumePrice);
        }

        [Fact]
        public void Get_ProductWithMoreThanVolumeAmount_ReturnsCorrectPrices()
        {
            var product = new Product("A", 10, new VolumeDiscount(20, 3));

            var price = PriceCalculator.Get(product, 4);

            Assert.Equal(30, price.Total);
            Assert.Equal(10, price.UnitPrice);
            Assert.Equal(20, price.VolumePrice);
        }

        [Fact]
        public void Get_ProductWithoutVolume_ReturnsCorrectPrices()
        {
            var product = new Product("A", 10);

            var price = PriceCalculator.Get(product, 4);

            Assert.Equal(40, price.Total);
            Assert.Equal(40, price.UnitPrice);
            Assert.Equal(0, price.VolumePrice);
        }
    }
}