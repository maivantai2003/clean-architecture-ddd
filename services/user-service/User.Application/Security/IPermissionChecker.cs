namespace User.Application.Security
{
    public interface IPermissionChecker
    {
        Task<bool> HasPermissionAsync(Guid userId,string permission,CancellationToken ct);
    }
}
