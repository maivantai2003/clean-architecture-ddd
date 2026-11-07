using Chat.Application.DTOs;
using MediatR;

namespace Chat.Application.Features.Chats.Queries
{
    public sealed class GetChatByIdQuery : IRequest<ConversationDto?>
    {
        public Guid ChatId { get; init; }
    }
}
