using System;
using System.Collections.Generic;
using System.IO;
using POSTerminal.Models;
using POSTerminal.Util;

namespace POSTerminal.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly string _filePath;

        public DiscountService(string filePath)
        {
            _filePath = filePath;
        }

        public decimal GetDiscountForAmount(decimal amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DiscountCondition> SetDiscountConditions()
        {
            
            List<DiscountCondition> discountConditions = new List<DiscountCondition>();

            var discountConditionsInput = FileReadManager.ReadFromFile(_filePath);

            return discountConditions;
        }
    }
}
