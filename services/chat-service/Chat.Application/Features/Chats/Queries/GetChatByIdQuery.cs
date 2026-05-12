using Chat.Application.DTOs;
using MediatR;

namespace Chat.Application.Features.Chats.Queries
{
    public sealed class GetChatByIdQuery : IRequest<ChatDto?>
    {
        public Guid ChatId { get; init; }
    }
}
