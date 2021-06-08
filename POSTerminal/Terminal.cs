using System;
using System.Collections.Generic;
using System.Linq;
using POSTerminal.Discount;

namespace POSTerminal
{
    public class Terminal
    {
        private readonly Dictionary<string, Product> _products;
        private Sale _sale = new();

        public Terminal(Dictionary<string, Product> products)
        {
            _products = products;
        }

        public void Scan(string code)
        {
            _products.TryGetValue(code, out var product);

            if (product == null)
                throw new ArgumentException("Unknown product.");

            _sale.Add(product);
        }

        public void Scan(DiscountCard discountCard) =>
            _sale.AddDiscountCard(discountCard);

        public decimal CalculateTotal() =>
            _sale.GetTotalPrice();

        public decimal Checkout()
        {
            var total = _sale.Checkout();
            _sale = new Sale();

            return total;
        }
    }
}