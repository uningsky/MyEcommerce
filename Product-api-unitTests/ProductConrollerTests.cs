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
    public class ProductsControllerTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture _fixture;

        private readonly IMapper _mapper;

        public ProductsControllerTests(DatabaseFixture fixture)
        {
            _fixture = fixture; 

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


            var controller = new ProductsController(_fixture.DbContext, _mapper);
            var actionResult = await controller.GetProducts(pageSize, pageIndex);

            //Assert
            Assert.IsType<ActionResult<PaginatedViewModel<ProductDto>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedViewModel<ProductDto>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }

        [Fact]
        public async Task Get_ProductById_ReturnsNotFound()
        {

            var controller = new ProductsController(_fixture.DbContext, _mapper);
            var actionResult = await controller.GetProduct(99);

            Assert.IsType<ActionResult<ProductDto>>(actionResult);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task Get_ProductById_ReturnsSuccess()
        {
            var controller = new ProductsController(_fixture.DbContext, _mapper);
            var actionResult = await controller.GetProduct(3);

            Assert.IsType<ActionResult<ProductDto>>(actionResult);
            var value = Assert.IsAssignableFrom<ProductDto>(actionResult.Value);
            Assert.Equal(3, value.Id);
            Assert.Equal("fakeItemC", value.Name);
            Assert.Equal(300, value.Price);
        }

    }

}
