using System.Collections.Generic;
using POSTerminal.Models;
using POSTerminal.Services;
using Xunit;

namespace POSTerminal.Tests
{
    public class ShopTerminalTests
    {
        public ShopTerminalTests()
        {
        }

        [Theory]
        [InlineData("A", 1.25)]
        [InlineData("B", 4.25)]
        [InlineData("C", 1.00)]
        public void CalculateCurrent_SingleProduct_ReturnsSameUnitPrice(string input, decimal expected)
        {
            var terminal = GetBasicTerminal();
            terminal.Scan(input);

            var currentTotal = terminal.CalculateCurrent();

            Assert.Equal(expected, currentTotal);
        }

        [Theory]
        [InlineData("A B C D A B A", 13.25)]
        public void CalculateCurrent_ListOfProducts_ReturnsSumOfVolumePriceAndUnitPrices(string inputList, decimal expected)
        {
            var terminal = GetBasicTerminal();
            foreach (var productCode in inputList.Split(" "))
            {
                terminal.Scan(productCode);
            }
            
            var currentTotal = terminal.CalculateCurrent();

            Assert.Equal(expected, currentTotal);
        }

        [Theory]
        [InlineData("C C C C C C", 5.0)]
        public void CalculateCurrent_ListOfProducts_ReturnsVolumePrice(string inputList, decimal expected)
        {
            var terminal = GetBasicTerminal();
            foreach (var productCode in inputList.Split(" "))
            {
                terminal.Scan(productCode);
            }

            var currentTotal = terminal.CalculateCurrent();

            Assert.Equal(expected, currentTotal);
        }

        [Fact]
        public void CalculateCurrent_CalculateAfterCheckout_ReturnsZero()
        {
            var terminal = GetBasicTerminal();
            terminal.Scan("A");
            terminal.Checkout();

            var currentTotal = terminal.CalculateCurrent();

            Assert.Equal(0, currentTotal);
        }

        [Theory]
        [InlineData("A", 1.2375)]
        public void CheckoutWithDiscounts_CalculateProductOnDiscount_ReturnsPriceOnDiscount(string input, decimal expected)
        {
            var terminal = GetBasicTerminal();
            terminal.Scan(input);
            var discountCard = GetOnePercentDiscountCard();

            var total = terminal.CheckoutWithDiscount(discountCard);

            Assert.Equal(expected, total);
        }

        [Theory]
        [InlineData("B", 2003.25)]
        public void CheckoutWithDiscounts_BuyDiscountProduct_UpdatesCardAmountOfSale(string input, decimal expected)
        {
            var terminal = GetBasicTerminal();
            terminal.Scan(input);
            const decimal MAXIMUM_AMOUNT_TO_ONE_PERCENT = 1999;
            var discountCard = new DiscountCard
            {
                AmountOfSale = MAXIMUM_AMOUNT_TO_ONE_PERCENT
            };

            terminal.CheckoutWithDiscount(discountCard);

            Assert.Equal(expected, discountCard.AmountOfSale);
            Assert.Equal(3, discountCard.DiscountPercent);
        }

        [Theory]
        [InlineData("C C C C C C C", 5.99)]
        public void CheckoutWithDiscounts_BuyProductList_DiscountOnlyForUnitPrice(string inputList, decimal expected)
        {
            var terminal = GetBasicTerminal();
            var discountCard = GetOnePercentDiscountCard();
            foreach (var productCode in inputList.Split(" "))
            {
                terminal.Scan(productCode);
            }

            var total = terminal.CheckoutWithDiscount(discountCard);

            Assert.Equal(expected, total);
        }


        private IEnumerable<Product> GetBasicProductList()
        {
            return new List<Product>
            {
                new Product
                {
                    Code = "A",
                    UnitPrice = (decimal)1.25,
                    NumberForVolume = 3,
                    VolumePrice = (decimal)3.0
                },
                new Product
                {
                    Code = "B",
                    UnitPrice = (decimal)4.25
                },
                new Product
                {
                    Code = "C",
                    UnitPrice = (decimal)1.00,
                    NumberForVolume = 6,
                    VolumePrice = (decimal)5.0
                },
                new Product
                {
                    Code = "D",
                    UnitPrice = (decimal)0.75
                }
            };
        }

        private IEnumerable<DiscountCondition> GetBasicDiscounts()
        {
            return new List<DiscountCondition>
            {
                new DiscountCondition
                {
                    AmountFrom = 1000,
                    AmountTo = 1999,
                    Percent = 1
                },
                new DiscountCondition
                {
                    AmountFrom = 2000,
                    AmountTo = 4999,
                    Percent = 3
                },
                new DiscountCondition
                {
                    AmountFrom = 5000,
                    AmountTo = 9999,
                    Percent = 5
                },
                new DiscountCondition
                {
                    AmountFrom = 9999,
                    Percent = 7
                }
            };
        }

        private DiscountCard GetOnePercentDiscountCard()
        {
            return new DiscountCard
            {
                AmountOfSale = 1500
            };
        }

        private ITerminal GetBasicTerminal()
        {
            var terminal = new ShopTerminal();
            terminal.SetPricing(GetBasicProductList());
            terminal.SetDiscounts(GetBasicDiscounts());

            return terminal;
        }
    }
}
