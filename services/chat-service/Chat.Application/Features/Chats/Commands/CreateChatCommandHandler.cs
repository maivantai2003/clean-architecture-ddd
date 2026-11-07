using Chat.Application.Abstractions.Persistence;
using Chat.Domain.Entities;
using Chat.Domain.Enums;
using MediatR;

namespace Chat.Application.Features.Chats.Commands
{
    public sealed class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Guid>
    {
        private readonly IConversationRepository _repository;

        public CreateChatCommandHandler(IConversationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            Conversation chat = request.Type switch
            {
                ChatType.UserToUser => Conversation.CreateUserToUserChat(request.ParticipantIds),
                ChatType.UserToBotAI when request.ParticipantIds.Count == 1 => Conversation.CreateUserToBotChat(request.ParticipantIds.Single()),
                _ => throw new InvalidOperationException("Invalid chat type or participant count.")
            };

            await _repository.AddAsync(chat);
            return chat.ChatId;
        }
    }
}
