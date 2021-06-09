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

        public decimal Balance { get; private set; }

        public DiscountCard()
        {
            Balance = 0;
        }

        public DiscountCard(decimal balance)
        {
            Balance = balance;
        }

        public void AddCurrentSale(decimal amount) =>
            Balance += amount;

        public decimal Apply(decimal quantity) =>
            quantity - quantity * CurrentPercent()/100;

        public decimal CurrentPercent() =>
            _conditions.Single(c => c.AmountFrom <= Balance && (c.AmountTo == null || Balance < c.AmountTo)).Percent;
    }
}