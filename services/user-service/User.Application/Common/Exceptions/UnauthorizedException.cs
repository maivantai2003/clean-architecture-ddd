namespace User.Application.Common.Exceptions
{
    public sealed class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = "Unauthorized"): base(message) { }
    }

    public sealed class ForbiddenException : Exception
    {
        public ForbiddenException(string permission): base($"Forbidden: missing permission {permission}") { }
    }
    
}
