namespace Auth.Application.Abstractions.Persistence
{
    public interface IUserPermissionQuery
    {
        Task<IReadOnlyList<string>> GetPermissionsAsync(Guid userId);
    }
}
