namespace Auth.Application.Abstractions.Persistence
{
    public interface IPermissionChecker
    {
        //Task<bool> HasPermissionAsync(Guid userId,string apiPath,string httpMethod,CancellationToken token=default);
        Task<bool> HasPermissionAsync(Guid userId,string permission,CancellationToken ct = default);
    }
}
