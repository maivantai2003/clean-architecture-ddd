using User.Application.Authorization;

namespace User.Application.Abstractions.Persistence
{
    public interface IRequirePermission
    {
        Permission Permission { get; }
    }
}
