using MediatR;
using User.Application.Common.Errors;
using User.Application.DTOs;

namespace User.Application.Features.Users.Queries
{
    public record GetUserQuery(Guid userId):IRequest<Result<UserDTO>>;
}
