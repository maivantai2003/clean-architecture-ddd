using Chat.Application.Abstractions.Persistence;
using MediatR;

namespace Chat.Application.Features.Chats.Commands
{
    public sealed class AddChatMessageCommandHandler : IRequestHandler<AddChatMessageCommand>
    {
        private readonly IChatRepository _repository;

        public AddChatMessageCommandHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddChatMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _repository.GetByIdAsync(request.ChatId);
            if (chat is null)
            {
                throw new InvalidOperationException("Chat not found.");
            }

            chat.AddMessage(request.SenderId, request.Content, request.IsBot);
            _repository.Update(chat);
            return Unit.Value;
        }
    }
}
