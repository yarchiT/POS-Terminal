using System;
using System.Collections.Generic;
using POSTerminal.Discount;
using POSTerminal.Tests.Extensions;
using Xunit;

namespace POSTerminal.Tests
{
    public class TerminalTests
    {
        [Fact]
        public void Scan_NotExistingProducts_ThrowsException()
        {
            var terminal = new Terminal(GetCatalog());

            Assert.Throws<ArgumentException>(() => terminal.Scan("X"));
        }

        [Theory]
        [InlineData("ABCDABA", 13.25)]
        [InlineData("CCCCCCC", 6.00)]
        [InlineData("ABCD", 7.25)]
        public void CalculateTotal_ExistingProducts_ReturnsTotalPrice(string productSequence, decimal totalPrice)
        {
            var terminal = new Terminal(GetCatalog());
            terminal.ScanAll(productSequence);

            Assert.Equal(totalPrice, terminal.CalculateTotal());
        }

        [Fact]
        public void CalculateTotal_WithVolumeDiscount_ReturnsTotalPrice()
        {
            var terminal = new Terminal(GetCatalog());
            terminal.ScanAll("AAA");

            Assert.Equal(3, terminal.CalculateTotal());
        }

        [Fact]
        public void Checkout_ExistingProducts_FinishesSale()
        {
            var terminal = new Terminal(GetCatalog());
            terminal.ScanAll("ABA");

            Assert.Equal((decimal)6.75, terminal.Checkout());
            Assert.Equal(0, terminal.CalculateTotal());
        }

        private Dictionary<string, Product> GetCatalog() =>
            new Dictionary<string, Product>
            {
                { "A", new Product("A", (decimal) 1.25, new VolumeDiscount((decimal) 3.00, 3)) },
                { "B", new Product("B", (decimal) 4.25) },
                { "C", new Product("C", (decimal) 1, new VolumeDiscount((decimal) 5, 6)) },
                { "D", new Product("D", (decimal) 0.75) }
            };
    }
}