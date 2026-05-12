using Chat.Domain.Enums;
using MediatR;

namespace Chat.Application.Features.Chats.Commands
{
    public sealed class CreateChatCommand : IRequest<Guid>
    {
        public ChatType Type { get; init; }
        public IReadOnlyCollection<Guid> ParticipantIds { get; init; } = Array.Empty<Guid>();
    }
}
