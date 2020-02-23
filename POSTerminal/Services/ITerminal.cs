using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services
{
    public interface ITerminal
    {
        void SetPricing(IEnumerable<Product> products);

        void Scan(string productCode);

        decimal CalculateTotal();
    }
}
