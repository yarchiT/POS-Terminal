using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services
{
    public interface ITerminal
    {
        void SetPricing(IEnumerable<Product> products);

        void Scan(string productCode);

        decimal CalculateCurrent();

        decimal Checkout();

        void SetDiscounts(IEnumerable<DiscountCondition> discountConditions);

        decimal CheckoutWithDiscount(DiscountCard discountCard);
    }
}
