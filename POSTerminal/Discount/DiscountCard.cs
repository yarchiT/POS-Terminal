using System.Collections.Generic;
using System.Linq;

namespace POSTerminal.Discount
{
    public class DiscountCard
    {
        private List<DiscountCardCondition> _conditions = new List<DiscountCardCondition>
        {
            new DiscountCardCondition(0, 0, 999),
            new DiscountCardCondition(1, 1000, 1999),
            new DiscountCardCondition(3, 2000, 4999),
            new DiscountCardCondition(5, 5000, 9999),
            new DiscountCardCondition(7, 9999)
        };

        private decimal _amount;

        public DiscountCard()
        {
            _amount = 0;
        }

        public DiscountCard(decimal amount)
        {
            _amount = amount;
        }

        public void AddCurrentSale(decimal amount) =>
            _amount += amount;

        public decimal GetPrice(int itemsQuantity, decimal unitPrice) =>
            unitPrice * itemsQuantity - unitPrice * itemsQuantity * GetCurrentPercent()/100;

        public decimal GetCurrentPercent() =>
            _conditions.Single(c => c.AmountFrom <= _amount && (c.AmountTo == null || _amount < c.AmountTo)).Percent;
    }
}