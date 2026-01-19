namespace Auth.WebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DynamicAuthorizeAttribute : Attribute
    {
    }
}
