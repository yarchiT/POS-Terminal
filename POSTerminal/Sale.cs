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

        public decimal GetTotalPrice() =>
            _sale.Sum(x => x.Total(_discountCard));

        public decimal Checkout()
        {
            var finalSum = GetTotalPrice();

            if (_discountCard == null) return finalSum;

            _discountCard.AddCurrentSale(GetTotalWithoutDiscounts());
            _discountCard = null;

            return finalSum;
        }

        private decimal GetTotalWithoutDiscounts()
        {
            return _sale
                .TakeWhile(saleItem => saleItem.Product.VolumeDiscount == null)
                .Sum(saleItem => saleItem.Product.Price * saleItem.Quantity);
        }
    }
}