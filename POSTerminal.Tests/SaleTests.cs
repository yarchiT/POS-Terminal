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

        [Fact]
        public void Checkout_WithDiscountCard_AmountOfSaleIsAddedToDiscountCard()
        {
            var sale = new Sale();
            var discountCard = new DiscountCard(1000);

            sale.Add(new Product("A", 2000));
            sale.AddDiscountCard(discountCard);

            Assert.Equal(1, discountCard.GetCurrentPercent());
            Assert.Equal(1980, sale.Checkout());
            Assert.Equal(3, discountCard.GetCurrentPercent());
        }

        [Fact]
        public void Checkout_WithDiscountCard_DiscountCardAppliesNewPercentToNewSale()
        {
            var sale1 = new Sale();
            var discountCard = new DiscountCard(1000);
            var product = new Product("A", 2000);

            sale1.Add(product);
            sale1.AddDiscountCard(discountCard);
            sale1.Checkout();
            var sale2 = new Sale();
            sale2.Add(product);
            sale2.AddDiscountCard(discountCard);

            Assert.Equal(3, discountCard.GetCurrentPercent());
            Assert.Equal(1940, sale2.Checkout());
            Assert.Equal(5, discountCard.GetCurrentPercent());
        }
    }
}