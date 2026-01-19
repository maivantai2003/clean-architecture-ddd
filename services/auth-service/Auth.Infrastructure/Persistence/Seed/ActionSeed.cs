namespace Auth.Infrastructure.Persistence.Seed
{
    public static class ActionSeed
    {
        public static readonly Guid CreateId = Guid.NewGuid();
        public static readonly Guid ReadId = Guid.NewGuid();
        public static readonly Guid UpdateId = Guid.NewGuid();
        public static readonly Guid DeleteId = Guid.NewGuid();
        public static readonly Guid AssignRoleId = Guid.NewGuid();

        public static IEnumerable<Auth.Domain.Entities.Action> Get()
        {
            return new List<Auth.Domain.Entities.Action>
            {
                new("CREATE", "Create", "Create resource"),
                new("READ", "Read", "Read resource"),
                new("UPDATE", "Update", "Update resource"),
                new("DELETE", "Delete", "Delete resource"),
                new("ASSIGN_ROLE", "Assign Role", "Assign role to user")
            };
        }
    }
}
