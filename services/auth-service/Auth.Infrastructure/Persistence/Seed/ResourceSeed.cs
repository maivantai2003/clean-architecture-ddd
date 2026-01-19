using Auth.Domain.Entities;

namespace Auth.Infrastructure.Persistence.Seed
{
    public static class ResourceSeed
    {
        public static readonly Guid UserResourceId = Guid.NewGuid();
        public static readonly Guid RoleResourceId = Guid.NewGuid();
        public static readonly Guid PermissionResourceId = Guid.NewGuid();
        public static IEnumerable<Resource> Get()
        {
            return new List<Resource>
            {
                new("USER", "User", "User management"),
                new("ROLE", "Role", "Role management"),
                new("PERMISSION", "Permission", "Permission management")
            };
        }
    }
}
