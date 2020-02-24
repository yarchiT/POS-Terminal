using System;
using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        public DiscountService()
        {
        }

        public decimal GetDiscountForAmount(decimal amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiscountCondition> SetDiscountConditions()
        {
            throw new NotImplementedException();
        }
    }
}
