namespace Auth.Application.Abstractions.Persistence
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
    }
}
