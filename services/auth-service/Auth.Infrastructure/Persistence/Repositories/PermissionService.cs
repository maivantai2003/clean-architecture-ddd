using Auth.Application.Abstractions.Persistence;
using Microsoft.Extensions.Caching.Memory;
namespace Auth.Infrastructure.Persistence.Repositories
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserPermissionQuery _query;
        private readonly IMemoryCache _cache;

        public PermissionService(
            IUserPermissionQuery query,
            IMemoryCache cache)
        {
            _query = query;
            _cache = cache;
        }

        public async Task<IReadOnlyList<string>> GetPermissionsAsync(Guid userId)
        {
            var cacheKey = $"PERMISSIONS_{userId}";

            if (_cache.TryGetValue(cacheKey, out IReadOnlyList<string> cached))
                return cached;

            var permissions = await _query.GetPermissionsAsync(userId);

            _cache.Set(cacheKey, permissions, TimeSpan.FromMinutes(10));

            return permissions;
        }

        public Task ClearAsync(Guid userId)
        {
            _cache.Remove($"PERMISSIONS_{userId}");
            return Task.CompletedTask;
        }
    }
}
