using System;
using System.Collections.Generic;
using POSTerminal.Models;
using POSTerminal.Util;

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

            var productsInput = FileReadManager.ReadFromFile(_filePath);

            try
            {
                foreach (var item in productsInput)
                {
                    var productLine = item.Split(' ');

                    Product product = new Product
                    {
                        Code = productLine[0],
                        UnitPrice = decimal.Parse(productLine[1])
                    };

                    if (productLine.Length == 4)
                    {
                        product.NumberForVolume = int.Parse(productLine[2]);
                        product.VolumePrice = decimal.Parse(productLine[3]);
                    }

                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while setting product's price from file", ex);
            }

            return products;
        }
    }
}
