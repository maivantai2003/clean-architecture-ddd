namespace User.Application.Common.Exceptions
{
    public sealed class AuthorizationServiceUnavailableException : Exception
    {
        public AuthorizationServiceUnavailableException()
            : base("Authorization service is unavailable.") { }

        public AuthorizationServiceUnavailableException(string message)
            : base(message) { }

        public AuthorizationServiceUnavailableException(string message, Exception inner)
            : base(message, inner) { }
    }
}
