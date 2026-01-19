
using MediatR;

namespace Realtime.Application.Features.Commands
{
    public record UserCreatedCommand(Guid Id, string Name, string Email):IRequest<Unit>;
}
