namespace Notification.Infrastructure.Email.Razor
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderAsync<TModel>(string viewName,TModel model);
    }
}
