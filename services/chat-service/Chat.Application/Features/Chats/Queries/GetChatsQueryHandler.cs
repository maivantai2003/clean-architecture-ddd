using Chat.Application.Abstractions.Persistence;
using Chat.Application.DTOs;
using MediatR;

namespace Chat.Application.Features.Chats.Queries
{
    public sealed class GetChatsQueryHandler : IRequestHandler<GetChatsQuery, IReadOnlyCollection<ChatDto>>
    {
        private readonly IChatRepository _repository;

        public GetChatsQueryHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ChatDto>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            var chats = await _repository.GetByParticipantIdAsync(request.ParticipantId);
            return chats.Select(chat => new ChatDto
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
            }).ToArray();
        }
    }
}
