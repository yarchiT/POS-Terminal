using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services.DiscountService
{
    public interface IDiscountService
    {
        IEnumerable<DiscountCondition> SetDiscountCondition();

        decimal GetDiscountForAmount(decimal amount);
    }
}
