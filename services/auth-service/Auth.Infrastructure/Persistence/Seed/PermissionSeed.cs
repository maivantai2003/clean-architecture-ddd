using Auth.Domain.Entities;

namespace Auth.Infrastructure.Persistence.Seed
{
    public static class PermissionSeed
    {
        public static IEnumerable<Permission> Get(
            Dictionary<string, Guid> resourceMap,
            Dictionary<string, Guid> actionMap)
        {
            return new List<Permission>
        {
            new(
                code: "USER_CREATE",
                resourceId: resourceMap["USER"],
                actionId: actionMap["CREATE"],
                apiEndpoint: "/api/users",
                httpMethod: "POST",
                isActive: true,
                description: "Create user"
            ),
            new(
                code: "USER_READ",
                resourceId: resourceMap["USER"],
                actionId: actionMap["READ"],
                apiEndpoint: "/api/users",
                httpMethod: "GET",
                isActive: true,
                description: "View users"
            ),
            new(
                code: "USER_ASSIGN_ROLE",
                resourceId: resourceMap["USER"],
                actionId: actionMap["ASSIGN_ROLE"],
                apiEndpoint: "/api/users/{id}/roles",
                httpMethod: "POST",
                isActive: true,
                description: "Assign role to user"
            ),
        };
        }
    }

}
