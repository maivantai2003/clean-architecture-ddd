using Ai.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai.Application.Features.Conversations.Queries
{
    public sealed record GetConversationHistoryQuery(
    Guid ConversationId,int Page,int PageSize): IRequest<Result<ConversationHistoryResponse>>;

    public sealed record MessageItem(Guid Id,string Role,string? Content,DateTimeOffset CreatedAt);

    public sealed record ConversationHistoryResponse(Guid ConversationId,IReadOnlyList<MessageItem> Messages,int Page,int PageSize);
}
