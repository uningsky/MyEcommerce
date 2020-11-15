using AutoMapper;
using MyEcommerce_product_api.Models;
using MyEcommerce_product_api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce_product_api.Utils
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Product, ProductDto>(); 
        }
    }
}
