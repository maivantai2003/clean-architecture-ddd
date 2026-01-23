using Auth.Application.Abstractions.Persistence;
using Dapper;
using System.Data;
namespace Auth.Infrastructure.Persistence.Identity
{
    public class PermissionChecker:IPermissionChecker
    {
        private readonly IDbConnection _connection;
        private readonly AuthDbContext _db;
        public PermissionChecker(AuthDbContext db,IDbConnection connection)
        {
            _db = db;
            _connection = connection;
        }

        public async Task<bool> HasPermissionAsync(Guid userId, string permission, CancellationToken ct = default)
        {

            var permissionCode = permission.Replace(".", ":").ToLowerInvariant();

            const string sql = """
                    SELECT TOP 1 1
                    FROM user_roles ur
                    JOIN role_permissions rp ON ur.RoleId = rp.RoleId
                    JOIN permissions p ON rp.PermissionId = p.PermissionId
                    WHERE ur.UserId = @UserId
                      AND p.Code = @PermissionCode
                """;
            var result = await _connection.ExecuteScalarAsync<int?>(sql,new { UserId = userId, PermissionCode = permissionCode });
            return result.HasValue;
        }
    }
}

