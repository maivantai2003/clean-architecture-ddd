using Microsoft.Extensions.Options;
using StackExchange.Redis;
namespace User.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        //private readonly IDatabase _data;
        private readonly IConnectionMultiplexer _redis;
        private readonly RedisKeyBuilder _keyBuilder;
        private readonly IDatabase _database;
        private readonly RedisOptions _options;
        public RedisCacheService(IConnectionMultiplexer redis, IOptions<RedisOptions> options)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
            _keyBuilder = new RedisKeyBuilder(options.Value);
            _options = options.Value;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(_keyBuilder.Build(key));
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(
            _keyBuilder.Build(key));

            if (value.IsNull)
                return default;

            return System.Text.Json.JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(_keyBuilder.Build(key));
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(value);

            var expiry =
                ttl ??
                TimeSpan.FromSeconds(_options.DefaultTtlSeconds);

            await _database.StringSetAsync(
                _keyBuilder.Build(key),
                json,
                expiry);
        }
    }
}
