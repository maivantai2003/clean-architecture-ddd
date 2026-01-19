namespace Auth.Application.DTOs
{
    public sealed record TokenResponse(
        string AccessToken,
        int AccessTokenExpiresIn, 
        string RefreshToken,
        int RefreshTokenExpiresIn, 
        string TokenType = "Bearer"
    );
}
