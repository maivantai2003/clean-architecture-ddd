using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Seed
{
    public static class AuthDataSeeder
    {
        public static async Task SeedAsync(AuthDbContext context)
        {
            await context.Database.MigrateAsync();

            await SeedActions(context);
            await SeedResources(context);
            await SeedPermissions(context);
            await SeedRoles(context);
            await SeedRolePermissions(context);
            await SeedUserRoles(context);
        }

        // =======================
        // ACTION
        // =======================
        private static async Task SeedActions(AuthDbContext context)
        {
            var actions = new[]
            {
                new PermissionAction("VIEW", "View", "View resource"),
                new PermissionAction("CREATE", "Create", "Create resource"),
                new PermissionAction("UPDATE", "Update", "Update resource"),
                new PermissionAction("DELETE", "Delete", "Delete resource")
            };

            foreach (var action in actions)
            {
                if (!await context.AppActions.AnyAsync(x => x.ActionCode == action.ActionCode))
                {
                    context.AppActions.Add(action);
                }
            }

            await context.SaveChangesAsync();
        }

        // =======================
        // RESOURCE
        // =======================
        private static async Task SeedResources(AuthDbContext context)
        {
            var resources = new[]
            {
                new Resource("USER", "User", "User management"),
                new Resource("ROLE", "Role", "Role management")
            };

            foreach (var res in resources)
            {
                if (!await context.Resources.AnyAsync(x => x.ResourceCode == res.ResourceCode))
                {
                    context.Resources.Add(res);
                }
            }

            await context.SaveChangesAsync();
        }

        // =======================
        // PERMISSION
        // =======================
        private static async Task SeedPermissions(AuthDbContext context)
        {
            var actions = await context.AppActions.ToListAsync();
            var resources = await context.Resources.ToListAsync();

            Permission P(
                string code,
                string resourceCode,
                string actionCode,
                string api,
                string method)
            {
                var action = actions.Single(x => x.ActionCode == actionCode);
                var resource = resources.Single(x => x.ResourceCode == resourceCode);

                return new Permission(
                    code,
                    resource.ResourceId,
                    action.ActionId,
                    api,
                    method,
                    true,
                    $"{action.ActionName} {resource.ResourceName}"
                );
            }

            var permissions = new[]
            {
                P("user:view", "USER", "VIEW", "/api/users", "GET"),
                P("user:create", "USER", "CREATE", "/api/users", "POST"),
                P("user:update", "USER", "UPDATE", "/api/users/{id}", "PUT"),
                P("user:delete", "USER", "DELETE", "/api/users/{id}", "DELETE"),

                P("role:view", "ROLE", "VIEW", "/api/roles", "GET"),
                P("role:create", "ROLE", "CREATE", "/api/roles", "POST")
            };

            foreach (var p in permissions)
            {
                if (!await context.Permissions.AnyAsync(x => x.Code == p.Code))
                {
                    context.Permissions.Add(p);
                }
            }

            await context.SaveChangesAsync();
        }

        // =======================
        // ROLE
        // =======================
        private static async Task SeedRoles(AuthDbContext context)
        {
            var roles = new[]
            {
                new Role("ADMIN", "Administrator", "System administrator"),
                new Role("USER", "User", "Normal user")
            };

            foreach (var role in roles)
            {
                if (!await context.Roles.AnyAsync(x => x.RoleCode == role.RoleCode))
                {
                    context.Roles.Add(role);
                }
            }

            await context.SaveChangesAsync();
        }

        // =======================
        // ROLE - PERMISSION
        // =======================
        private static async Task SeedRolePermissions(AuthDbContext context)
        {
            var adminRole = await context.Roles
                .SingleAsync(x => x.RoleCode == "ADMIN");

            var userRole = await context.Roles
                .SingleAsync(x => x.RoleCode == "USER");

            var permissions = await context.Permissions.ToListAsync();

            foreach (var p in permissions)
            {
                if (!await context.RolePermissions.AnyAsync(rp =>
                    rp.RoleId == adminRole.RoleId &&
                    rp.PermissionId == p.PermissionId))
                {
                    context.RolePermissions.Add(
                        new RolePermission(adminRole.RoleId, p.PermissionId)
                    );
                }
            }

            var userView = permissions.Single(x => x.Code == "user:view");

            if (!await context.RolePermissions.AnyAsync(rp =>
                rp.RoleId == userRole.RoleId &&
                rp.PermissionId == userView.PermissionId))
            {
                context.RolePermissions.Add(
                    new RolePermission(userRole.RoleId, userView.PermissionId)
                );
            }

            await context.SaveChangesAsync();
        }

        // =======================
        // USER - ROLE (USER EXIST)
        // =======================
        private static async Task SeedUserRoles(AuthDbContext context)
        {
            var adminUserId = Guid.Parse("B496B47F-B6C3-4E7F-8DB9-1683B10B8A6B");
            var normalUserId = Guid.Parse("744D3337-B270-49A1-8156-3F0D28D8F2A3");

            var adminRoleId = await context.Roles
                .Where(x => x.RoleCode == "ADMIN")
                .Select(x => x.RoleId)
                .SingleAsync();

            var userRoleId = await context.Roles
                .Where(x => x.RoleCode == "USER")
                .Select(x => x.RoleId)
                .SingleAsync();

            if (await context.Users.AnyAsync(x => x.UserId == adminUserId))
            {
                if (!await context.UserRoles.AnyAsync(ur =>
                    ur.UserId == adminUserId &&
                    ur.RoleId == adminRoleId))
                {
                    context.UserRoles.Add(
                        new UserRole(adminUserId, adminRoleId)
                    );
                }
            }

            if (await context.Users.AnyAsync(x => x.UserId == normalUserId))
            {
                if (!await context.UserRoles.AnyAsync(ur =>
                    ur.UserId == normalUserId &&
                    ur.RoleId == userRoleId))
                {
                    context.UserRoles.Add(
                        new UserRole(normalUserId, userRoleId)
                    );
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
