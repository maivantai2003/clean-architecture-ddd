using MediatR;
using User.Application.Authorization;
using User.Application.DTOs;

namespace User.Application.Features.Users.Queries
{
    [RequirePermission("Users","Read")]
    public record GetAllUserQuery() : IRequest<List<UserDTO>>;
}
