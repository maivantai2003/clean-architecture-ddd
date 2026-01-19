using System.Security.Claims;
namespace User.Application.Authorization
{
    public interface IUserContext
    {
        ClaimsPrincipal User { get; }
    }
}
