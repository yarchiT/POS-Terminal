using System;
using System.Collections.Generic;
using System.IO;
using POSTerminal.Services;
using POSTerminal.Services.PricingService;

namespace ShopClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ITerminal terminal = new ShopTerminal();
            IPricingService pricingService = new FilePricingService(Path.Combine(Directory.GetCurrentDirectory(), "ProductPrice.txt"));
            
            terminal.SetPricing(pricingService.SetPricing());

            var listOfTests = new List<string[]>
            {
                "A B C D A B A".Split(" "),
                "C C C C C C C".Split(" "),
                "A B C D".Split(" ")
            };

            foreach (var test in listOfTests)
            {
                foreach (var item in test)
                {
                    terminal.Scan(item);
                }

                Console.WriteLine("Total: " + string.Format("$ {0:0.00}", terminal.CalculateCurrent()));
                terminal.Checkout();
            }
        }
    }
}
