namespace Auth.Application.Abstractions.Security
{
    public interface IPasswordResetTokenGenerator
    {
        string GenerateToken();

        string HashToken(string token);
    }
}
