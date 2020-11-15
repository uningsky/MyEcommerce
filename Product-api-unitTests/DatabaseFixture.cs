using Microsoft.EntityFrameworkCore;
using MyEcommerce_product_api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product_api_unitTests
{
    public class DatabaseFixture : IDisposable
    {
        public readonly DbContextOptions<ProductContext> _dbOptions;
        public ProductContext DbContext { get; private set; }

        public DatabaseFixture()
        {
            _dbOptions = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            DbContext = new ProductContext(_dbOptions);
            DbContext.AddRange(GetFakeProduct());
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            //DbContext.Database.EnsureDeleted();
        }

        private List<Product> GetFakeProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "fakeItemA",
                    Price = 100
                },
                new Product()
                {
                    Id = 2,
                    Name = "fakeItemB",
                    Price = 200
                },
                new Product()
                {
                    Id = 3,
                    Name = "fakeItemC",
                    Price = 300
                },
                new Product()
                {
                    Id = 4,
                    Name = "fakeItemD",
                    Price = 400
                },
                new Product()
                {
                    Id = 5,
                    Name = "fakeItemE",
                    Price = 500
                },
                new Product()
                {
                    Id = 6,
                    Name = "fakeItemF",
                    Price = 600
                }
            };
        }


    }
}
