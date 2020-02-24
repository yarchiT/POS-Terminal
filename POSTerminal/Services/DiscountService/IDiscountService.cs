using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services.DiscountService
{
    public interface IDiscountService
    {
        IEnumerable<DiscountCondition> SetDiscountConditions();
    }
}
