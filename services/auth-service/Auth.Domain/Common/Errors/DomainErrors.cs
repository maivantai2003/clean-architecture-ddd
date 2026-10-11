namespace Auth.Domain.Common.Errors
{
    public static class DomainErrors
    {
        public static class User
        {
            public static readonly Error InvalidEmail =
                new(
                    "User.InvalidEmail",
                    "Email is invalid.");

            public static readonly Error InvalidPassword =
                new(
                    "User.InvalidPassword",
                    "Password is invalid.");

            public static readonly Error Inactive =
                new(
                    "User.Inactive",
                    "User is inactive.");

            public static readonly Error InvalidStatus =
                new(
                    "User.InvalidStatus",
                    "User status is invalid.");
        }
        public static readonly Error Expired =
        new(
            "RefreshToken.Expired",
            "Refresh token has expired.");

        public static readonly Error Revoked =
            new(
                "RefreshToken.Revoked",
                "Refresh token has been revoked.");
    }
}
