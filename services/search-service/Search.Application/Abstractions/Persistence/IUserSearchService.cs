using Search.Domain.Entities;
namespace Search.Application.Abstractions.Persistence
{
    public interface IUserSearchService
    {
        Task IndexAsync(UserDocument doc);
        Task DeleteAsync(string id);
        Task<(IEnumerable<UserDocument> Data, long Total)> SearchAsync(string keyword,int page,int size);
    }
}
