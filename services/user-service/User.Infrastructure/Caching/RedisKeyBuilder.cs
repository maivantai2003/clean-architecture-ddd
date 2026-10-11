

namespace User.Infrastructure.Caching
{
    public sealed class RedisKeyBuilder
    {
        private readonly RedisOptions _options;

        public RedisKeyBuilder(RedisOptions options)
        {
            _options = options;
        }

        public string Build(string key)
        {
            return $"{_options.Instance}:{key}";
        }
    }
}
