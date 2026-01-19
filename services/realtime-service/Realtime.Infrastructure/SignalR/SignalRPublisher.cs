using Microsoft.AspNetCore.SignalR;
using Realtime.Abstractions;
using Realtime.Application.Interfaces;

namespace Realtime.Infrastructure.SignalR
{
    public class SignalRPublisher : IRealtimePublisher
    {
        private readonly IHubContext<RealtimeHubBase> _hub;
        public SignalRPublisher(IHubContext<RealtimeHubBase> hub) { _hub = hub; }
        public Task BroadcastAsync(string eventName, object payload) => _hub.Clients.All.SendAsync(eventName, payload);
        public Task PushToGroupAsync(string group, string eventName, object payload) => _hub.Clients.Group(group).SendAsync(eventName, payload);
        public Task PushToUserAsync(Guid userId,string eventName,object payload ) => _hub.Clients.User(userId.ToString()).SendAsync(eventName, payload);
    }
}
