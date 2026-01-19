using MediatR;
using System.Security.Claims;
using User.Application.Authorization;
using User.Application.DTOs;

namespace User.Application.Features.Users.Commands
{
    //public record UserUpdateEmailCommand(Guid UserId, string NewEmail ):IRequest<Result<Domain.Entities.User>>;
    //public record UserUpdateEmailCommand(Guid UserId, string NewEmail) : SecuredRequest<Result<Domain.Entities.User>>
    //{
    //    public override Permission Permission => Permissions.Users.Update;
    //};
    [RequirePermission("Users", "Update")]
    public record UserUpdateEmailCommand(Guid UserId, string NewEmail) : IRequest<Result<Domain.Entities.User>>;
}
