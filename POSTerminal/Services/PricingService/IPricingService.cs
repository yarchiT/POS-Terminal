using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services.PricingService
{
    public interface IPricingService
    {
        IEnumerable<Product> SetPricing();
    }
}
