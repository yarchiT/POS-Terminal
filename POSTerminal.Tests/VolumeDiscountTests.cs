using POSTerminal.Discount;
using Xunit;

namespace POSTerminal.Tests
{
    public class VolumeDiscountTests
    {
        [Fact]
        public void GetTotalPrice_WithVolumeQuantity_ReturnsVolumePrice()
        {
            var volumeDiscount = new VolumeDiscount(5, 6);

            Assert.Equal(5, volumeDiscount.GetTotalPrice(6, 1));
        }

        [Fact]
        public void GetTotalPrice_MoreThanVolumeQuantity_ReturnsCombinedPrice()
        {
            var volumeDiscount = new VolumeDiscount(5, 6);

            Assert.Equal(6, volumeDiscount.GetTotalPrice(7, 1));
        }

        [Fact]
        public void GetTotalPrice_LessThanVolumeQuantity_ReturnsUnitPrice()
        {
            var volumeDiscount = new VolumeDiscount(5, 6);

            Assert.Equal(4, volumeDiscount.GetTotalPrice(4, 1));
        }
    }
}