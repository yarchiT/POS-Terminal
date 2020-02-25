using System;
using System.Collections.Generic;
using System.IO;
using POSTerminal.Models;
using POSTerminal.Services;
using POSTerminal.Services.DiscountService;
using POSTerminal.Services.PricingService;

namespace ShopClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ITerminal terminal = new ShopTerminal();
            IPricingService pricingService = new FilePricingService(Path.Combine(Directory.GetCurrentDirectory(), "ProductPrice.txt"));
            IDiscountService discountService = new DiscountService(Path.Combine(Directory.GetCurrentDirectory(), "DiscountConditions.txt"));
            
            terminal.SetPricing(pricingService.SetPricing());
            terminal.SetDiscounts(discountService.SetDiscountConditions());

            var listOfTests = new List<string[]>
            {
                "A B C D A B A".Split(" "),
                "C C C C C C C".Split(" "),
                "A B C D".Split(" ")
            };

            DiscountCard discountCard = new DiscountCard
            {
                AmountOfSale = 1500
            };

            foreach (var test in listOfTests)
            {
                foreach (var item in test)
                {
                    terminal.Scan(item);
                }

                Console.WriteLine("CurrentTotal: " + string.Format("$ {0:0.00}", terminal.CalculateCurrent()));
                Console.WriteLine($"Checkout with Discount: ${terminal.CheckoutWithDiscount(discountCard)}");
            }
        }
    }
}
