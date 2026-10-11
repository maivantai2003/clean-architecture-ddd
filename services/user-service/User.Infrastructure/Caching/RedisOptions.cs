namespace User.Infrastructure.Caching
{
    public class RedisOptions
    {
        public const string SectionName ="Redis";
        public string? ConnectionString { get; init; }
        public string? Instance { get; init; }
        public int DefaultTtlSeconds { get; init; }
    }
}
