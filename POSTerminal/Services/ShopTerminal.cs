using System;
using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services
{
    public class ShopTerminal : ITerminal
    {
        private IEnumerable<Product> _products;

        public void SetPricing(IEnumerable<Product> products)
        {
            _products = products;
        }

        public void Scan(string productCode)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateTotal()
        {
            throw new NotImplementedException();
        }
    }
}
