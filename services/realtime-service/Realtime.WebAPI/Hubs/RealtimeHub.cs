using Microsoft.AspNetCore.SignalR;
using Realtime.Abstractions;

namespace Realtime.WebAPI.Hubs
{
    public class RealtimeHub:RealtimeHubBase
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier!;
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId}");
            await base.OnConnectedAsync();
        }
    }
}
