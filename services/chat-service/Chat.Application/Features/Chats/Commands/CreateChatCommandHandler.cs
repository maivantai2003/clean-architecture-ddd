using Chat.Application.Abstractions.Persistence;
using Chat.Domain.Entities;
using Chat.Domain.Enums;
using MediatR;

namespace Chat.Application.Features.Chats.Commands
{
    public sealed class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Guid>
    {
        private readonly IChatRepository _repository;

        public CreateChatCommandHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            Chat chat = request.Type switch
            {
                ChatType.UserToUser => Chat.CreateUserToUserChat(request.ParticipantIds),
                ChatType.UserToBotAI when request.ParticipantIds.Count == 1 => Chat.CreateUserToBotChat(request.ParticipantIds.Single()),
                _ => throw new InvalidOperationException("Invalid chat type or participant count.")
            };

            await _repository.AddAsync(chat);
            return chat.ChatId;
        }
    }
}
