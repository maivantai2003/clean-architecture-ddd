using Ai.Application.Common.Results;
using MediatR;
namespace Ai.Application.Features.Conversations.Commands
{
    public sealed record CreateConversationCommand(string Title): IRequest<Result<CreateConversationResponse>>;

    public sealed record CreateConversationResponse(Guid ConversationId,string Title,DateTimeOffset CreatedAt);
}
