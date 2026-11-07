using Chat.Application.Abstractions.Persistence;
using Chat.Application.DTOs;
using MediatR;

namespace Chat.Application.Features.Chats.Queries
{
    public sealed class GetChatByIdQueryHandler : IRequestHandler<GetChatByIdQuery, ConversationDto?>
    {
        private readonly IConversationRepository _repository;

        public GetChatByIdQueryHandler(IConversationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConversationDto?> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await _repository.GetByIdAsync(request.ChatId);
            return chat == null ? null : MapToDto(chat);
        }

        private static ConversationDto MapToDto(Domain.Entities.Conversation chat)
        {
            return new ConversationDto
            {
                ChatId = chat.ChatId,
                ChatType = chat.Type.ToString(),
                ParticipantIds = chat.ParticipantIds,
                CreatedAt = chat.CreatedAt,
                Messages = chat.Messages.Select(m => new MessageDto
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
