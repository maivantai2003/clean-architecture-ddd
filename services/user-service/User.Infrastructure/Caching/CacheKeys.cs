

namespace User.Infrastructure.Caching
{
    public static class CacheKeys
    {
        public static string User(Guid id) => $"user:{id}";
    }
}
