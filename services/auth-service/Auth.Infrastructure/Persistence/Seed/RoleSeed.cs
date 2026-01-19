using Auth.Domain.Entities;

namespace Auth.Infrastructure.Persistence.Seed
{
    public static class RoleSeed
    {
        public static readonly Guid AdminRoleId = Guid.NewGuid();
        public static readonly Guid UserRoleId = Guid.NewGuid();

        public static IEnumerable<Role> Get()
        {
            return new List<Role>
            {
                new("ADMIN", "Administrator", "System administrator"),
                new("USER", "User", "Normal user")
            };
        }
    }

}
