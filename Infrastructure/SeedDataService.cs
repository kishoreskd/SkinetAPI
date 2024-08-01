using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SeedDataService
    {
        /// <summary>
        /// Fetching data from the existing json file and store them in to the data base.
        /// </summary>
        /// <param name="context">Db context</param>
        /// <param name="loggerFactory">Logging</param>
        /// <returns></returns>
        public static async Task SeedAsync(SkiNetDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());

                    var brandData = File.ReadAllText("../Infrastructure/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    await context.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Infrastructure/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

                    await context.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText("../Infrastructure/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    await context.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<SeedDataService>();
                logger.LogError(ex.Message);
            }
        }
    }
}
