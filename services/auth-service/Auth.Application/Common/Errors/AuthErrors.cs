namespace Auth.Application.Common.Errors
{
    public static class AuthErrors
    {
        public static readonly Error InvalidCredentials =
            new(
                "Auth.InvalidCredentials",
                "Invalid email or password.");

        public static readonly Error RefreshTokenExpired =
            new(
                "Auth.RefreshTokenExpired",
                "Refresh token expired.");

        public static readonly Error RefreshTokenInvalid =
            new(
                "Auth.RefreshTokenInvalid",
                "Refresh token invalid.");
    }
}
