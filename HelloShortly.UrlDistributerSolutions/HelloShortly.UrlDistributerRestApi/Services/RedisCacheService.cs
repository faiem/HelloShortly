using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            if (key == null)
                return null;
            int charSum = 0;
            foreach (char c in key)
            {
                charSum += (int)c;
            }
            int index = charSum % 16;
            var db = _connectionMultiplexer.GetDatabase(index);
            return await db.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            if (key != null)
            {
                int charSum = 0;
                foreach (char c in key)
                {
                    charSum += (int)c;
                }
                int index = charSum % 16;
                var db = _connectionMultiplexer.GetDatabase(index);
                await db.StringSetAsync(key, value);
                await db.KeyExpireAsync(key, DateTime.UtcNow.AddDays(7));
            }
        }
    }
}
