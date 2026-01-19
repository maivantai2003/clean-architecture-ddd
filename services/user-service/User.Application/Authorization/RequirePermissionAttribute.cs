namespace User.Application.Authorization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class RequirePermissionAttribute : Attribute
    {
        public Permission Permission { get; }

        public RequirePermissionAttribute(string resource, string action)
        {
            Permission = new Permission(resource, action);
        }
    }
}
