using System.Collections.Generic;
using System.Linq;
using POSTerminal.Discount;

namespace POSTerminal
{
    public class Sale
    {
        private readonly List<SaleItem> _sale = new();
        private DiscountCard? _discountCard;

        public void Add(Product product)
        {
            var saleItem = _sale.Find(x => x.Product == product);
            if (saleItem != null)
            {
                saleItem.Increment();
                return;
            }

            _sale.Add(new SaleItem(product));
        }

        public void AddDiscountCard(DiscountCard discountCard) =>
            _discountCard = discountCard;

        public decimal GetTotalPrice()
        {
            return _sale
                .Select(saleItem => saleItem.GetPrice())
                .Select(price => _discountCard?.Apply(price.UnitPrice) + price.VolumePrice ?? price.Total)
                .Sum();
        }

        public decimal Checkout()
        {
            var finalSum = GetTotalPrice();

            if (_discountCard == null) return finalSum;

            _discountCard.AddCurrentSale(GetTotalWithoutDiscounts());
            _discountCard = null;

            return finalSum;
        }

        private decimal GetTotalWithoutDiscounts() =>
            _sale.Sum(x => x.GetPrice().UnitPrice);
    }
}