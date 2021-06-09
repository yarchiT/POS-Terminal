using System.Collections.Generic;
using System.Linq;

namespace POSTerminal.Discount
{
    public class DiscountCard
    {
        private readonly List<DiscountCardCondition> _conditions = new List<DiscountCardCondition>
        {
            new DiscountCardCondition(0, 0, 999),
            new DiscountCardCondition(1, 1000, 1999),
            new DiscountCardCondition(3, 2000, 4999),
            new DiscountCardCondition(5, 5000, 9999),
            new DiscountCardCondition(7, 9999)
        };

        private decimal _balance;

        public DiscountCard()
        {
            _balance = 0;
        }

        public DiscountCard(decimal balance)
        {
            _balance = balance;
        }

        public void AddCurrentSale(decimal amount) =>
            _balance += amount;

        public decimal Apply(decimal quantity) =>
            quantity - quantity * GetCurrentPercent()/100;

        public decimal GetCurrentPercent() =>
            _conditions.Single(c => c.AmountFrom <= _balance && (c.AmountTo == null || _balance < c.AmountTo)).Percent;
    }
}