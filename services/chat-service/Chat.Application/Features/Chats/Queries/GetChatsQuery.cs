using Chat.Application.DTOs;
using MediatR;

namespace Chat.Application.Features.Chats.Queries
{
    public sealed class GetChatsQuery : IRequest<IReadOnlyCollection<ChatDto>>
    {
        public Guid ParticipantId { get; init; }
    }
}
