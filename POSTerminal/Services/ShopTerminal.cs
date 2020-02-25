using System;
using System.Collections.Generic;
using System.Linq;
using POSTerminal.Models;

namespace POSTerminal.Services
{
    public class ShopTerminal : ITerminal
    {
        private IEnumerable<Product> _products;
        private IEnumerable<DiscountCondition> _discountConditions;
        private Dictionary<string, int> _scannedProducts;

        public ShopTerminal()
        {
            _scannedProducts = new Dictionary<string, int>();
        }

        public void SetPricing(IEnumerable<Product> products)
        {
            _products = products;
        }

        public void SetDiscounts(IEnumerable<DiscountCondition> discountConditions)
        {
            _discountConditions = discountConditions;
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

            return currentTotal;
        }

        public decimal Checkout()
        {
            var total = CalculateCurrent();
            _scannedProducts.Clear();

            return total;
        }

        public decimal CheckoutWithDiscount(DiscountCard discountCard)
        {
            if (_discountConditions == null)
            {
                throw new Exception("Discounts are not set up");
            }

            if (discountCard.DiscountPercent == 0)
            {
                discountCard.DiscountPercent = getUpdatedDiscountCardPercent(discountCard.AmountOfSale);
            }

            var (volumePriceTotal, unitPriceTotal) = getVolumeAndUnitPricesForDiscountCheck();

            var unitDiscount = discountCard.DiscountPercent == 0 ? 0 : unitPriceTotal * discountCard.DiscountPercent / 100;

            discountCard.AmountOfSale += unitPriceTotal;
            discountCard.DiscountPercent = getUpdatedDiscountCardPercent(discountCard.AmountOfSale);
            
            _scannedProducts.Clear();

            return volumePriceTotal + unitPriceTotal - unitDiscount;
        }

        private (decimal volumePriceTotal, decimal unitPriceTotal) getVolumeAndUnitPricesForDiscountCheck()
        {
            decimal volumePriceTotal = 0;
            decimal unitPriceTotal = 0;

            foreach (var (productKey, numberOfProducts) in _scannedProducts)
            {
                var product = _products.Single(p => p.Code == productKey);

                if (product.NumberForVolume != null && numberOfProducts >= product.NumberForVolume.Value)
                {
                    var amountOfVolumes = numberOfProducts / product.NumberForVolume.Value;
                    volumePriceTotal += amountOfVolumes * product.VolumePrice.Value;
                    unitPriceTotal += (numberOfProducts - amountOfVolumes * product.NumberForVolume.Value) * product.UnitPrice;
                }
                else
                {
                    unitPriceTotal += numberOfProducts * product.UnitPrice;
                }
            }

            return (volumePriceTotal, unitPriceTotal);
        }

        private decimal getUpdatedDiscountCardPercent(decimal discountCardAmountOfSale)
        {
            var discountCondition = _discountConditions
                .FirstOrDefault(dc => discountCardAmountOfSale >= dc.AmountFrom
                && (dc.AmountTo == null || dc.AmountTo.Value > discountCardAmountOfSale));

            return discountCondition == null ? 0 : discountCondition.Percent;
        }

        
    }
}
