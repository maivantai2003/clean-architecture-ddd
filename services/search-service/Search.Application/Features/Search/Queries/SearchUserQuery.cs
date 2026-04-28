using MediatR;
using Search.Domain.Entities;

namespace Search.Application.Features.Search.Queries
{
    public record SearchUserQuery(string Keyword, int Page, int Size)
    : IRequest<SearchUserResponse>;

    public record SearchUserResponse(IEnumerable<UserDocument> Data, long Total);
}
