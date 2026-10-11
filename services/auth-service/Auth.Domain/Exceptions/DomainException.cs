using Auth.Domain.Common.Errors;

namespace Auth.Domain.Exceptions
{
    public sealed class DomainException : Exception
    {
        public Error Error { get; }

        public DomainException(Error error)
            : base(error.Message)
        {
            Error = error;
        }
    }
}
