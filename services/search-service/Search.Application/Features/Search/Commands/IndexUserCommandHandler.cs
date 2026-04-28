using MediatR;
using Search.Application.Abstractions.Persistence;
using Search.Domain.Entities;

namespace Search.Application.Features.Search.Commands
{
    public class IndexUserCommandHandler : IRequestHandler<IndexUserCommand, Unit>
    {
        private readonly IUserSearchService _userSearchService;
        public IndexUserCommandHandler(IUserSearchService userSearchService)
        {
            _userSearchService = userSearchService;
        }

        public async Task<Unit> Handle(IndexUserCommand request, CancellationToken cancellationToken)
        {
            var doc = new UserDocument(request.UserId, request.Username, request.Email);
            await _userSearchService.IndexAsync(doc);
            return Unit.Value;
        }
    }
}
