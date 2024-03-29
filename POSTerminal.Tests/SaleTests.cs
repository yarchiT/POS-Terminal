﻿using POSTerminal.Discount;
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

            Assert.Equal(1, discountCard.CurrentPercent());
            Assert.Equal(1980, sale.Checkout());
            Assert.Equal(3, discountCard.CurrentPercent());
        }

        [Fact]
        public void Checkout_WithDiscountCard_DiscountAppliesToProductIfVolumeIsNotReached()
        {
            var sale1 = new Sale();
            var discountCard = new DiscountCard(1000);
            var product = new Product("A", 2000, new VolumeDiscount(3000, 2));

            sale1.Add(product);
            sale1.AddDiscountCard(discountCard);

            Assert.Equal(1, discountCard.CurrentPercent());
            Assert.Equal(1980, sale1.Checkout());
            Assert.Equal(3, discountCard.CurrentPercent());
        }

        [Fact]
        public void Checkout_WithDiscountCard_NoDiscountForVolume()
        {
            var sale1 = new Sale();
            var product = new Product("A", 2000, new VolumeDiscount(3000, 2));
            var discountCard = new DiscountCard(1000);

            sale1.Add(product);
            sale1.Add(product);
            sale1.AddDiscountCard(discountCard);

            Assert.Equal(1, discountCard.CurrentPercent());
            Assert.Equal(3000, sale1.Checkout());
            Assert.Equal(1, discountCard.CurrentPercent());
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

            Assert.Equal(3, discountCard.CurrentPercent());
            Assert.Equal(1940, sale2.Checkout());
            Assert.Equal(5, discountCard.CurrentPercent());
        }

        [Fact]
        public void Checkout_WithoutDiscountCard_DiscountCardDoesntApplyToNewSale()
        {
            var sale1 = new Sale();
            var discountCard = new DiscountCard(1000);
            var product = new Product("A", 2000);

            sale1.Add(product);
            sale1.AddDiscountCard(discountCard);
            sale1.Checkout();
            var sale2 = new Sale();
            sale2.Add(product);

            Assert.Equal(3, discountCard.CurrentPercent());
            Assert.Equal(2000, sale2.Checkout());
            Assert.Equal(3, discountCard.CurrentPercent());
        }

        [Fact]
        public void Checkout_WithDiscountCard_DiscountCardAddsOnlyUnitPriceToBalance()
        {
            var sale = new Sale();
            var discountCard = new DiscountCard(1000);
            var product = new Product("A", 1000, new VolumeDiscount(2000, 3));

            sale.Add(product);
            sale.Add(product);
            sale.Add(product);
            sale.Add(product);

            sale.AddDiscountCard(discountCard);

            Assert.Equal(1000, discountCard.Balance);
            Assert.Equal(2990, sale.Checkout());
            Assert.Equal(2000, discountCard.Balance);
        }
    }
}