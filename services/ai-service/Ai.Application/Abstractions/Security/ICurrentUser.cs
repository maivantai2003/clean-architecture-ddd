namespace Ai.Application.Abstractions.Security
{
    public interface ICurrentUser
    {
        Guid UserId { get; }

        Guid? TenantId { get; }
    }
}
