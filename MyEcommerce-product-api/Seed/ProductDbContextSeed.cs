using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyEcommerce_product_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce_product_api.Seed
{
    public class ProductDbContextSeed
    {
        public async Task SeedAsync(ProductContext context, ILogger<ProductDbContextSeed> logger)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "아이폰 12",
                        Price = 700000
                    },
                    new Product
                    {
                        Name = "아이폰 12 프로",
                        Price = 990000
                    },
                    new Product
                    {
                        Name = "아이폰 12 맥스",
                        Price = 1290000
                    },
                    new Product
                    {
                        Name = "아이패드",
                        Price = 990000
                    },
                    new Product
                    {
                        Name = "맥",
                        Price = 1200000
                    }
                );

                await context.SaveChangesAsync();
            }
        }

    }
}
