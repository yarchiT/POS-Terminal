using System;
using System.Collections.Generic;
using System.IO;
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

            var productLines = ReadProductPricesFromFile();

            try
            {
                foreach (var item in productLines)
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

        private string[] ReadProductPricesFromFile()
        {
            string[] productLines;

            if (File.Exists(_filePath))
            {
                try
                {
                    productLines = File.ReadAllLines(_filePath);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Coudn't read product prices from file with path {_filePath}", ex);
                }

            }
            else
            {
                throw new Exception($"File with path {_filePath} doesn't exist in project directory");
            }

            return productLines;
        }
    }
}
