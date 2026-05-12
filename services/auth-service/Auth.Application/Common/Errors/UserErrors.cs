namespace Auth.Application.Common.Errors
{
    public static class UserErrors
    {
        public static readonly Error EmailAlreadyExists =
            new(
                "User.EmailAlreadyExists",
                "Email đã được sử dụng.");

        public static readonly Error UserNotFound =
            new(
                "User.NotFound",
                "Không tìm thấy user.");

        public static readonly Error InvalidPassword =
            new(
                "User.InvalidPassword",
                "Sai mật khẩu.");
    }
}
