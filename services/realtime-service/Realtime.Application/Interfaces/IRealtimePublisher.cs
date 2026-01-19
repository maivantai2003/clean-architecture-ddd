namespace Realtime.Application.Interfaces
{
    public interface IRealtimePublisher
    {
        Task BroadcastAsync(string eventName, object payload);
        Task PushToUserAsync(Guid userId, string eventName, object payload);
        Task PushToGroupAsync(string group, string eventName, object payload);
    }
}
