using POSTerminal.Discount;
using Xunit;

namespace POSTerminal.Tests
{
    public class SaleTests
    {
        [Fact]
        public void Add_DistinctProducts_ReturnsSumOfUnitPrices()
        {
            var sale = new Sale();

            sale.Add(new Product("A", 1));
            sale.Add(new Product("B", 2));
            sale.Add(new Product("C", 3));

            Assert.Equal(6, sale.GetTotalPrice());
        }

        [Fact]
        public void Add_MixOfVolumeAndSingleProducts_ReturnsCorrectTotalPrice()
        {
            var sale = new Sale();
            var volumeProduct = new Product("A", 1, new VolumeDiscount(2, 3));

            sale.Add(volumeProduct);
            sale.Add(volumeProduct);
            sale.Add(volumeProduct);
            sale.Add(new Product("B", 5));

            Assert.Equal(7, sale.GetTotalPrice());
        }
    }
}