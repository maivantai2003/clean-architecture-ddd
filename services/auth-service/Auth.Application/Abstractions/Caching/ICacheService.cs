namespace Auth.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        //Task<T?> GetAsync<T>(string key, CancellationToken ct = default);
        //Task SetAsync<T>(string key,T value, CancellationToken ct = default, TimeSpan? ttl = null);
        //Task RemoveAsync(string key, CancellationToken ct = default);
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? ttl = null);
        Task RemoveAsync(string key);
    }
}
