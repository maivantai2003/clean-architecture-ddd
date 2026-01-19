using MediatR;

namespace Auth.Application.Features.Actions.Commands
{
    public record CreateActionCommand(string Code,string Name,string Description):IRequest<Guid>;
}
