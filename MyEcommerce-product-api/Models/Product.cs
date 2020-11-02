using System;
using System.ComponentModel.DataAnnotations;

namespace MyEcommerce_product_api.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(minimum: 1, maximum: (double)decimal.MaxValue)]
        public decimal Price { get; set; }
    }
}
