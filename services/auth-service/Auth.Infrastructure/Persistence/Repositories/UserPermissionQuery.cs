using Auth.Application.Abstractions.Persistence;
using Dapper;
using System.Data;

namespace Auth.Infrastructure.Persistence.Repositories
{
    public class UserPermissionQuery:IUserPermissionQuery
    {
        private readonly IDbConnection _connection;
        public UserPermissionQuery(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyList<string>> GetPermissionsAsync(Guid userId)
        {
            const string sql = @"
                SELECT DISTINCT p.Code
                FROM Users u
                JOIN UserRoles ur ON u.UserId = ur.UserId
                JOIN Roles r ON ur.RoleId = r.RoleId
                JOIN RolePermissions rp ON r.RoleId = rp.RoleId
                JOIN Permissions p ON rp.PermissionId = p.PermissionId
                WHERE u.UserId = @UserId
                  AND p.IsActive = 1
                ";
            var result = await _connection.QueryAsync<string>(sql, new { UserId = userId });
            return result.ToList();
        }
    }
}
