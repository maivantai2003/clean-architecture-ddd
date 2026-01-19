using MediatR;
namespace Auth.Application.Features.Resources.Commands
{
    public record DeleteResourceCommand(Guid ResourceId) : IRequest<Unit>;
}
