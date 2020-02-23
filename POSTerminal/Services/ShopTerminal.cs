using System;
using System.Collections.Generic;
using System.Linq;
using POSTerminal.Models;

namespace POSTerminal.Services
{
    public class ShopTerminal : ITerminal
    {
        private IEnumerable<Product> _products;
        private Dictionary<string, int> _scannedProducts;

        public void SetPricing(IEnumerable<Product> products)
        {
            _products = products;
        }

        public void Scan(string productCode)
        {
            if (_products == null)
            {
                throw new Exception($"Product pricing is not yet stored in the system");
            }

            if (!_products.Any(p => p.Code == productCode))
            {
                throw new Exception($"Product {productCode} is not stored in the system");
            }

            if (!_scannedProducts.ContainsKey(productCode))
            {
                _scannedProducts[productCode] = 1;
            }
            else
            {
                _scannedProducts[productCode] += 1;
            }
        }

        public decimal CalculateCurrent()
        {
            decimal currentTotal = 0;

            foreach (var (productKey, numberOfProducts) in _scannedProducts)
            {
                var product = _products.Single(p => p.Code == productKey);
                decimal productTotalPrice = 0;

                if (product.NumberForVolume != null && numberOfProducts >= product.NumberForVolume.Value)
                {
                    var amountOfVolumes = numberOfProducts / product.NumberForVolume.Value;
                    productTotalPrice = amountOfVolumes * product.VolumePrice.Value
                        + (numberOfProducts - amountOfVolumes * product.NumberForVolume.Value) * product.UnitPrice;
                }
                else
                {
                    productTotalPrice = numberOfProducts * product.UnitPrice;
                }

                currentTotal += productTotalPrice;
            }

            _scannedProducts.Clear();

            return currentTotal;
        }

        public decimal Checkout()
        {
            var total = CalculateCurrent();
            _scannedProducts.Clear();

            return total;
        }
    }
}
