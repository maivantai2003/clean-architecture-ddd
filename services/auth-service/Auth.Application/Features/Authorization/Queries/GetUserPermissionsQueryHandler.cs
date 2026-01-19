using MediatR;
namespace Auth.Application.Features.Authorization.Queries
{
    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, IReadOnlyList<string>>
    {
        //private readonly IPermissionCache _cache;
        //private readonly IUserPermissionQuery _query;
        public GetUserPermissionsQueryHandler()
        {
            //_cache = cache;
            //_query = query;
        }

        public Task<IReadOnlyList<string>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        //public async Task<IReadOnlyList<string>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        //{
        //    var cached = await _cache.GetAsync(request.UserId);
        //    if (cached.Any())
        //        return cached;

        //    var permissions = await _query.GetPermissionsAsync(request.UserId);

        //    await _cache.SetAsync(request.UserId, permissions);

        //    return permissions;
        //}
    }
}
