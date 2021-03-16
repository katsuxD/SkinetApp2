using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFacory)
        {
            try
            {   
                //checking productBrands in db    
                if (!context.ProductBrands.Any())
                {
                    var brandsData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                //devide data into object each one in each row    
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                //add into our db
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    } 

                    await context.SaveChangesAsync();   
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                //devide data into object each one in each row    
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                //add into our db
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    } 

                    await context.SaveChangesAsync();   
                }

                if (!context.Products.Any())
                {
                    var productsData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                //devide data into object each one in each row    
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                //add into our db
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    } 

                    await context.SaveChangesAsync();   
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFacory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}