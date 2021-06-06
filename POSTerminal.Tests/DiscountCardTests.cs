using POSTerminal.Discount;
using Xunit;

namespace POSTerminal.Tests
{
    public class DiscountCardTests
    {
        [Fact]
        public void GetTotalPrice_WithoutDiscountPercent_ReturnsUnitPrice()
        {
            var discountCard = new DiscountCard();

            Assert.Equal(10, discountCard.GetTotalPrice(5, 2));
        }

        [Fact]
        public void GetTotalPrice_WithDiscountPercent_ReturnsReducedPrice()
        {
            var discountCard = new DiscountCard(1500);

            Assert.Equal(990, discountCard.GetTotalPrice(100, 10));
        }

        [Fact]
        public void AddCurrentSale_AfterAddingLastSale_AppliesNewDiscountPercent()
        {
            var discountCard = new DiscountCard(1500);
            var totalPriceWithOnePercentDiscount = discountCard.GetTotalPrice(100, 10);

            discountCard.AddCurrentSale(1000);

            Assert.Equal(990, totalPriceWithOnePercentDiscount);
            Assert.Equal(970, discountCard.GetTotalPrice(100, 10));
        }
    }
}