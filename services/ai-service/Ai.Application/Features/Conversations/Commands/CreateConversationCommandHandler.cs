using Ai.Application.Abstractions.Persistence;
using Ai.Application.Abstractions.Security;
using Ai.Application.Common.Results;
using Ai.Domain.Conversations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai.Application.Features.Conversations.Commands
{
    public sealed class CreateConversationCommandHandler
    : IRequestHandler<CreateConversationCommand,Result<CreateConversationResponse>>
    {
        private readonly IConversationRepository _repository;
        private readonly ICurrentUser _currentUser;

        public CreateConversationCommandHandler(IConversationRepository repository,ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<Result<CreateConversationResponse>> Handle(
            CreateConversationCommand request,CancellationToken cancellationToken)
        {
            var conversation = Conversation.Create(userId: _currentUser.UserId,title: request.Title,
                tenantId: _currentUser.TenantId);

            await _repository.AddAsync(conversation,cancellationToken);

            return Result<CreateConversationResponse>.Success(
                new CreateConversationResponse(conversation.Id,conversation.Title,conversation.CreatedAt));
        }
    }
}
