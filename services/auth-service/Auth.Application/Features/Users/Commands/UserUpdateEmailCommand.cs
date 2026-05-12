using Auth.Application.Common.Errors;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public record UserUpdateEmailCommand(Guid UserId, string NewEmail) : IRequest<Result<Domain.Entities.User>>;
}
