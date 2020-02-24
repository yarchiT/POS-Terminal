using System;
using System.Collections.Generic;
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

        public IEnumerable<DiscountCondition> SetDiscountConditions()
        {
            List<DiscountCondition> discountConditions = new List<DiscountCondition>();
            var discountConditionsInput = FileReadManager.ReadFromFile(_filePath);

            try
            {
                foreach (var item in discountConditionsInput)
                {
                    var discountConditionLine = item.Split(' ');

                    if (discountConditionLine.Length < 2)
                    {
                        throw new Exception($"Invalid input in DiscountConditions file {_filePath}");
                    }

                    DiscountCondition discountCondition = new DiscountCondition();

                    var discountConditionAmountRange = item.Split("-");
                    discountCondition.AmountFrom = decimal.Parse(discountConditionAmountRange[0]);

                    if (discountConditionAmountRange.Length == 2)
                    {
                        discountCondition.AmountTo = decimal.Parse(discountConditionAmountRange[1]);
                    }

                    discountCondition.Percent = decimal.Parse(discountConditionLine[1]);

                    discountConditions.Add(discountCondition);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while setting discount condition from file", ex);
            }

            return discountConditions;
        }
    }
}
