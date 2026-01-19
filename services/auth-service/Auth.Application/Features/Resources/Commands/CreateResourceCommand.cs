using MediatR;
namespace Auth.Application.Features.Resources.Commands
{
    public record CreateResourceCommand(string Code, string Name, string Description) : IRequest<Guid>;
}
