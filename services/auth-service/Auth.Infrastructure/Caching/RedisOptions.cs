namespace Auth.Infrastructure.Caching
{
    public sealed class RedisOptions
    {
        public string ConnectionString { get; init; } = default!;
        public int DefaultTtlSeconds { get; init; } = 300;
        public string Prefix { get; init; } = "auth:";
    }
}
