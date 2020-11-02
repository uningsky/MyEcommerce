using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MyEcommerce_product_api.Models;
using MyEcommerce_product_api.ViewModels;

namespace MyEcommerce_product_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public const string UpdateProductById = nameof(UpdateProductById);
        public const string DeleteProductById = nameof(DeleteProductById);

        private readonly ProductContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// select Products
        /// </summary>
        /// <returns> Produtcs </returns>
        [HttpGet(Name = "GetAllModelsRoute")]
        //[ProducesResponseType(typeof(PaginatedItemsViewModel<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedViewModel<ProductViewModel>>> GetProducts([FromQuery] int pageSize = 2, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _context.Products.LongCountAsync();

            var itemsOnPage = await _context.Products
                .OrderBy(c => c.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var vm = _mapper.Map<List<ProductViewModel>>(itemsOnPage);
            
            var result = new PaginatedViewModel<ProductViewModel>(pageIndex, pageSize,(int)totalItems,vm);

            return result;
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetModelByIdRoute")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductViewModel>> GetProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<ProductViewModel>(product);

            return model;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}", Name = UpdateProductById)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutProduct(long id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, null);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}", Name = DeleteProductById)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}
