using MediatR;
namespace Auth.Application.Features.Actions.Commands
{
    public record UpdateActionCommand(Guid ActionId,string Name,string Description):IRequest<Unit>;
}
