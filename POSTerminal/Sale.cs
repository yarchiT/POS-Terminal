using System.Collections.Generic;
using System.Linq;
using POSTerminal.Discount;

namespace POSTerminal
{
    public class Sale
    {
        private List<SaleItem> _sale = new();
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
            if (_discountCard != null)
                _sale.Last().ApplyDiscount(_discountCard);
        }

        public void AddDiscountCard(DiscountCard discountCard)
        {
            _discountCard = discountCard;
            _sale.ForEach(x => x.ApplyDiscount(discountCard));
        }

        public decimal GetTotalPrice() =>
            _sale.Sum(x => x.Total());

        public decimal Checkout()
        {
            var finalSum = GetTotalPrice();
            _sale = new List<SaleItem>();
            _discountCard = null;

            return finalSum;
        }
    }
}