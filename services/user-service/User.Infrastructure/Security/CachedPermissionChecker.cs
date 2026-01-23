using User.Application.Common.Exceptions;
using User.Application.Security;
using User.Infrastructure.Caching;

namespace User.Infrastructure.Security
{
    public class CachedPermissionChecker : IPermissionChecker
    {
        private readonly ICacheService _cache;
        private readonly HttpClient _authClient;
        public CachedPermissionChecker(ICacheService cache,IHttpClientFactory factory)
        {
            _cache = cache;
            _authClient = factory.CreateClient("AuthClient");
        }

        public async Task<bool> HasPermissionAsync(Guid userId,string permission,CancellationToken ct)
        {
            var key = $"perm:{userId}.{permission}";
            Console.WriteLine(permission);
            var cached = await _cache.GetAsync<bool?>(key);
            if (cached.HasValue)
                return cached.Value;

            var res = await _authClient.GetAsync(
                $"/permissions/check?userId={userId}&permission={permission}",
                ct);
            Console.WriteLine(res.StatusCode);
            Console.WriteLine(res.ToString());
            if (!res.IsSuccessStatusCode)
                throw new AuthorizationServiceUnavailableException();

            var content = await res.Content.ReadAsStringAsync(ct);

            if (!bool.TryParse(content, out var allowed))
                throw new InvalidPermissionResponseException(content);

            await _cache.SetAsync(key,allowed,TimeSpan.FromMinutes(2));

            return allowed;
        }
    }

}
