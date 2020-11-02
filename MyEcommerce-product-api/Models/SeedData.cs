using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce_product_api.Models
{
    public static class SeedData
    {
        public static void Initialize(ProductContext context)
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

                context.SaveChanges();
            }
        }
    }
}
