using System.Collections.Generic;
using POSTerminal.Models;

namespace POSTerminal.Services.PricingService
{
    public class FilePricingService : IPricingService
    {
        private readonly string _filePath;

        public FilePricingService(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Product> SetPricing()
        {
            List<Product> products = new List<Product>();
            
            return products;
        }
    }
}
