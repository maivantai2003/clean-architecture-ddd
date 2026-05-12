using MediatR;

namespace Chat.Application.Features.Chats.Commands
{
    public sealed class AddChatMessageCommand : IRequest
    {
        public Guid ChatId { get; init; }
        public Guid SenderId { get; init; }
        public string Content { get; init; } = string.Empty;
        public bool IsBot { get; init; }
    }
}
