using BasketApi_ex.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketApi_ex.Infrastructure.Repositories
{
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ILogger<RedisBasketRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisBasketRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<RedisBasketRepository>();
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var data = await _database.StringGetAsync(customerId);

            if (data.IsNullOrEmpty)
            {
                return null; 
            }

            return JsonConvert.DeserializeObject<CustomerBasket>(data); 
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var created = await _database.StringSetAsync(customerBasket.BuyerId, JsonConvert.SerializeObject(customerBasket));

            if (!created)
            {
                _logger.LogError("redis set 문제"); 
                return null; 
            }

            _logger.LogInformation("redis set 성공");

            return await GetBasketAsync(customerBasket.BuyerId); 
        }

        public async Task<bool> DeleteBasketAsync(string customerId)
        {
            return await _database.KeyDeleteAsync(customerId);
        }

    }
}
