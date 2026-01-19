using Auth.Application.Abstractions.Caching;
using Auth.Application.Abstractions.Security;
using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Security;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AuthDbContext _db;
    private readonly ICacheService _cache;

    private static string CacheKey(string hash)
        => $"refresh_token:{hash}";

    public RefreshTokenRepository(AuthDbContext db, ICacheService cache){
        _db = db;
        _cache = cache;
    }

    public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
    {
        var cached = await _cache.GetAsync<RefreshToken>(CacheKey(tokenHash));
        if (cached != null)
            return cached;

        var entity = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == tokenHash);

        if (entity != null)
            await CacheAsync(entity);

        return entity;
    }

    public async Task AddAsync(RefreshToken token)
    {
        await _db.RefreshTokens.AddAsync(token);
        await CacheAsync(token);
    }

    public async Task RevokeAsync(RefreshToken token)
    {
        token.Revoke();
        await _cache.RemoveAsync(CacheKey(token.TokenHash));
    }

    public async Task RevokeAllByUserAsync(Guid userId)
    {
        await _db.RefreshTokens
            .Where(x => x.UserId == userId && !x.IsRevoked)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsRevoked, true));
    }

    private async Task CacheAsync(RefreshToken token)
    {
        var ttl = token.ExpiresAt - DateTime.UtcNow;
        if (ttl <= TimeSpan.Zero)
            return;
        await _cache.SetAsync(CacheKey(token.TokenHash),token,ttl);
    }
}
