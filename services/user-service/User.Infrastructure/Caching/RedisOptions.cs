namespace User.Infrastructure.Caching
{
    public class RedisOptions
    {
        public string ConnectionString { get; init; }
        public string Instance { get; init; }
        public int DefaultTtlSeconds { get; init; }
    }
}
