using MediatR;
using User.Application.Authorization;
using User.Application.DTOs;

namespace User.Application.Features.Users.Queries
{
    [RequirePermission("User","View")]
    public record GetAllUserQuery() : IRequest<List<UserDTO>>;
}
