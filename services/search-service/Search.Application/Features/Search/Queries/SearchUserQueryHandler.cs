using MediatR;
using Search.Application.Abstractions.Persistence;
using Search.Domain.Entities;

namespace Search.Application.Features.Search.Queries
{
    public class SearchUserQueryHandler:IRequestHandler<SearchUserQuery, SearchUserResponse>
    {
        private readonly IUserSearchService _userSearchService;
        public SearchUserQueryHandler(IUserSearchService userSearchService)
        {
            _userSearchService = userSearchService;
        }
        public async Task<SearchUserResponse> Handle(SearchUserQuery request, CancellationToken cancellationToken)
        {
            var (data, total) = await _userSearchService.SearchAsync(
            request.Keyword,
            request.Page,
            request.Size);

            return new SearchUserResponse(data, total);
        }
    }
}
