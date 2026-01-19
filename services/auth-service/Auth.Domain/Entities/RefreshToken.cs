using System.Security.Cryptography;
using System.Text;

namespace Auth.Domain.Entities;

public class RefreshToken
{
    public Guid RefreshTokenId { get; private set; }
    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public string? ReplacedByTokenHash { get; private set; }

    private RefreshToken() { }

    public static RefreshToken Create(
        Guid userId,
        string tokenHash,
        DateTime expiresAt)
    {
        return new RefreshToken
        {
            RefreshTokenId = Guid.NewGuid(),
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = expiresAt
        };
    }

    public void Revoke(string? replacedByTokenHash = null)
    {
        IsRevoked = true;
        ReplacedByTokenHash = replacedByTokenHash;
    }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public static string Hash(string token)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(
            sha.ComputeHash(Encoding.UTF8.GetBytes(token)));
    }
}
