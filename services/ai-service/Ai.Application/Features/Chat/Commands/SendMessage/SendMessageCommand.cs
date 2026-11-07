using Ai.Application.Common.Results;
using MediatR;
namespace Ai.Application.Features.Chat.Commands.SendMessage
{
    public sealed record SendMessageCommand(Guid ConversationId,string Content): IRequest<Result<ChatResponse>>;

    public sealed record ChatResponse(Guid UserMessageId,Guid AssistantMessageId,string Answer,IReadOnlyList<SourceReference> Sources);

    public sealed record SourceReference(Guid DocumentId,string? SourceName);
}
