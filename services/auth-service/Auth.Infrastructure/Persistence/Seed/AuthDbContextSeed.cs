using Auth.Domain.Entities;

namespace Auth.Infrastructure.Persistence.Seed
{
    public static class AuthDbContextSeed
    {
        public static async Task SeedAsync(AuthDbContext context)
        {
            if (context.Users.Any()) return;

            var resources = ResourceSeed.Get().ToList();
            var actions = ActionSeed.Get().ToList();

            context.Resources.AddRange(resources);
            context.Actions.AddRange(actions);

            await context.SaveChangesAsync();

            var resourceMap = resources.ToDictionary(r => r.ResourceCode, r => r.ResourceId);
            var actionMap = actions.ToDictionary(a => a.ActionCode, a => a.ActionId);

            var permissions = PermissionSeed.Get(resourceMap, actionMap).ToList();
            context.Permissions.AddRange(permissions);

            var roles = RoleSeed.Get().ToList();
            context.Roles.AddRange(roles);

            await context.SaveChangesAsync();

            var adminRole = roles.First(r => r.RoleCode == "ADMIN");

            var rolePermissions = permissions
                .Select(p => new RolePermission(adminRole.RoleId, p.PermissionId));

            context.RolePermissions.AddRange(rolePermissions);

            //var adminUser = UserSeed.CreateAdmin("HASHED_PASSWORD");
            //context.Users.Add(adminUser);

            await context.SaveChangesAsync();
        }
    }

}
