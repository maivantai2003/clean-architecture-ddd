using Chat.Application.Abstractions.Persistence;
using Chat.Application.DTOs;
using MediatR;

namespace Chat.Application.Features.Chats.Queries
{
    public sealed class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, ChatDto?>
    {
        private readonly IChatRepository _repository;

        public GetChatByIdQueryHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<ChatDto?> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await _repository.GetByIdAsync(request.ChatId);
            return chat == null ? null : MapToDto(chat);
        }

        private static ChatDto MapToDto(Domain.Entities.Chat chat)
        {
            return new ChatDto
            {
                ChatId = chat.ChatId,
                ChatType = chat.Type.ToString(),
                ParticipantIds = chat.ParticipantIds,
                CreatedAt = chat.CreatedAt,
                Messages = chat.Messages.Select(m => new ChatMessageDto
                {
                    MessageId = m.MessageId,
                    SenderId = m.SenderId,
                    Content = m.Content,
                    IsBot = m.IsBot,
                    SentAt = m.SentAt
                }).ToList()
            };
        }
    }
}
