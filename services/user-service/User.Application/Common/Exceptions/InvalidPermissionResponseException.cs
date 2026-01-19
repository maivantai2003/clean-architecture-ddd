namespace User.Application.Common.Exceptions
{
    public sealed class InvalidPermissionResponseException : Exception
    {
        public InvalidPermissionResponseException(string response) : base($"Invalid permission response: {response}") { }
    }
}
