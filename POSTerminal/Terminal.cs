﻿using System;
using System.Collections.Generic;
using System.Linq;
using POSTerminal.Discount;

namespace POSTerminal
{
    public class Terminal
    {
        private readonly Dictionary<string, Product> _products;
        private List<SaleItem> _sale = new();
        private DiscountCard _discountCard;

        public Terminal(Dictionary<string, Product> products)
        {
            _products = products;
        }

        public void Scan(string code)
        {
            var product = _products.SingleOrDefault(x => x.Key == code).Value;

            if (product == null)
                throw new ArgumentException("Unknown product.");

            var saleItem = _sale.Find(x => x.Product == product);
            if (saleItem == null)
            {
                _sale.Add(new SaleItem(product));
                return;
            }

            saleItem.Increment();
        }

        public void Scan(DiscountCard discountCard)
        {
            _discountCard = discountCard;
            _sale.ForEach(x => x.ApplyDiscount(discountCard));
        }

        public decimal CalculateTotal() =>
            _sale.Sum(x => x.Total());

        public decimal Checkout()
        {
            var finalSum = CalculateTotal();
            _sale = new List<SaleItem>();

            return finalSum;
        }
    }
}