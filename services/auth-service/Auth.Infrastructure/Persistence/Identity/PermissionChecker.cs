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
            var parts = permission.Split('.');
            var resource = parts[0];
            var action = parts[1];
            const string sql = """
                        SELECT TOP 1 1
                        FROM UserRoles ur
                        JOIN RolePermissions rp ON ur.RoleId = rp.RoleId
                        JOIN Permissions p ON rp.PermissionId = p.PermissionId
                        WHERE ur.UserId = @UserId
                          AND (
                                p.Code = @Permission
                             OR p.Code = @Resource + '.*'
                             OR p.Code = '*.' + @Action
                             OR p.Code = '*'
                          )
                        ORDER BY
                            CASE p.Code
                                WHEN @Permission THEN 1
                                WHEN @Resource + '.*' THEN 2
                                WHEN '*.' + @Action THEN 3
                                WHEN '*' THEN 4
                            END
                        """;
            var result = await _connection.ExecuteScalarAsync<int?>(sql,new { UserId = userId, Permission = permission });
            return result.HasValue;
        }
    }
}
