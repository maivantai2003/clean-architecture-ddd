namespace User.Application.Authorization
{
    public static class Permissions
    {
        public static class Users
        {
            public static readonly Permission Create =
                new("Users", "Create");

            public static readonly Permission View =
                new("Users", "View");

            public static readonly Permission Update =
                new("Users", "Update");
            public static readonly Permission Delete =
               new("Users", "Delete");
        }
    }

}
