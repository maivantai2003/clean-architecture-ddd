using Auth.Application.DTOs;
using MediatR;

namespace Auth.Application.Features.Users.Commands
{
    public record UserUpdateEmailCommand(Guid UserId, string NewEmail) : IRequest<Result<Domain.Entities.User>>;
}
