using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Moq;
using MyEcommerce_product_api;
using MyEcommerce_product_api.Controllers;
using MyEcommerce_product_api.Models;
using MyEcommerce_product_api.Utils;
using MyEcommerce_product_api.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Product_api_unitTests
{
    public class ProductsControllerTests
    {
        private readonly DbContextOptions<ProductContext> _dbOptions;
        private readonly IMapper _mapper;

        public ProductsControllerTests()
        {
            _dbOptions = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new ProductContext(_dbOptions))
            {
                dbContext.AddRange(GetFakeProduct());
                dbContext.SaveChanges();
            }

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task Get_Products_success()
        {
            var pageSize = 4;
            var pageIndex = 1;

            var expectedItemsInPage = 2;
            var expectedTotalItems = 6;

            var productContext = new ProductContext(_dbOptions);


            //Act
            var orderController = new ProductsController(productContext, _mapper);
            var actionResult = await orderController.GetProducts(pageSize, pageIndex);

            //Assert
            Assert.IsType<ActionResult<PaginatedViewModel<ProductViewModel>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedViewModel<ProductViewModel>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }

        private List<Product> GetFakeProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "fakeItemA",
                },
                new Product()
                {
                    Id = 2,
                    Name = "fakeItemB",
                },
                new Product()
                {
                    Id = 3,
                    Name = "fakeItemC",
                },
                new Product()
                {
                    Id = 4,
                    Name = "fakeItemD",
                },
                new Product()
                {
                    Id = 5,
                    Name = "fakeItemE",
                },
                new Product()
                {
                    Id = 6,
                    Name = "fakeItemF",
                }
            };
        }
    }

}
