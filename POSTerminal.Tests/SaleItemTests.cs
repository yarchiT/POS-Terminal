using Xunit;

namespace POSTerminal.Tests
{
    public class SaleItemTests
    {
        [Fact]
        public void Total_SingleItem_ReturnsUnitPrice()
        {
            var saleItem = new SaleItem(new Product("A", 1));

            Assert.Equal(1, saleItem.Total());
        }

        [Fact]
        public void Total_AfterIncrement_ReturnsUnitPrice()
        {
            var saleItem = new SaleItem(new Product("A", 1));
            saleItem.Increment();

            Assert.Equal(2, saleItem.Total());
        }

        [Fact]
        public void Total_WithVolumeDiscount_ReturnsVolumePrice()
        {
            var saleItem = new SaleItem(new Product("A", 1, new VolumeDiscount(2, 3)));
            saleItem.Increment();
            saleItem.Increment();

            Assert.Equal(2, saleItem.Total());
        }

        [Fact]
        public void Total_WithVolumeDiscount_ReturnsCombinedPrice()
        {
            var saleItem = new SaleItem(new Product("A", 1, new VolumeDiscount(2, 3)));
            saleItem.Increment();
            saleItem.Increment();
            saleItem.Increment();

            Assert.Equal(3, saleItem.Total());
        }
    }
}