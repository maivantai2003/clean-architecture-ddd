using MediatR;
using User.Application.DTOs;

namespace User.Application.Features.Users.Queries
{
    public record GetUserQuery(Guid userId):IRequest<UserDTO>;
}
