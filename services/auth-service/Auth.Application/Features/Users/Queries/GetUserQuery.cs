using Auth.Application.DTOs;
using MediatR;

namespace Auth.Application.Features.Users.Queries
{
    public record GetUserQuery(Guid userId):IRequest<UserDTO>;
}
