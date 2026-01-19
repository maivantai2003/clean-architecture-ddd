namespace Auth.Application.Abstractions.Cache
{
    public interface IPermissionCache
    {
        Task<IReadOnlyList<string>> GetAsync(Guid userId);
        Task SetAsync(Guid userId, IReadOnlyList<string> permissions);
        Task RemoveAsync(Guid userId);
    }
}
