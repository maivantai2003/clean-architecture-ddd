using Auth.Application.Abstractions.Caching;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Auth.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _data;
        private readonly RedisOptions _options;
        public RedisCacheService(IConnectionMultiplexer redis,IOptions<RedisOptions> options)
        {
            _data = redis.GetDatabase();
            _options = options.Value;
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _data.StringGetAsync(key);
            return value.IsNull?default:JsonConvert.DeserializeObject<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            await _data.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            var json = JsonConvert.SerializeObject(value);
            var expiryTime = ttl ?? TimeSpan.FromMinutes(30);
            await _data.StringSetAsync(key, json, expiryTime, when: When.Always);
        }
    }
}
