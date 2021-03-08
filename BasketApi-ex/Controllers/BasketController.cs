using BasketApi_ex.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketApi_ex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository; 

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository; 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketByIdAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id)); 
        }

        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync([FromBody]CustomerBasket basket)
        {
            return Ok(await _basketRepository.UpdateBasketAsync(basket)); 
        }

        public async Task DeleteBasketByIdAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id); 
        }

    }
}
