using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEcommerce_product_api.ViewModels
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
