using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyEcommerce_product_api.Controllers;
using MyEcommerce_product_api.Models;
using MyEcommerce_product_api.Utils;
using MyEcommerce_product_api.ViewModels;
using Sciensoft.Hateoas.Extensions;

namespace MyEcommerce_product_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("ProductDb")); 
            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMappingProfile));

            services.AddLink(policy =>
            {
                policy.AddPolicy<ProductViewModel>(model =>
                {
                    model
                        .AddSelf(m => m.Id, "This is a GET self link.")
                        .AddRoute(m => m.Id, ProductsController.UpdateProductById)
                        .AddRoute(m => m.Id, ProductsController.DeleteProductById)
                        .AddCustomPath(m => m.Id, "Edit", method: HttpMethods.Post, message: "Edits resource");
                });
                /*
                policy.AddPolicy<PaginatedItemsViewModel<ProductViewModel>>(model =>
                {
                    model
                        .AddCustomPath(m => $"/api/[controller]", "All", method: HttpMethods.Get);

                });*/
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Product API",
                    Description = "Product API ¿¬½À",
                    TermsOfService = new Uri("https://localhost:44320"),
                    Contact = new OpenApiContact
                    {
                        Name = "uningsky",
                        Email = "uningsky@gmail.com",
                        Url = new Uri("https://localhost:44320"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "",
                        Url = new Uri("https://localhost:44320"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Api V1");
            }); 

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
