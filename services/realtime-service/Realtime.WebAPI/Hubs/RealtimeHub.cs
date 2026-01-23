using Microsoft.AspNetCore.SignalR;
using Realtime.Abstractions;
using Realtime.Application.Interfaces;

namespace Realtime.WebAPI.Hubs
{
    public class RealtimeHub:RealtimeHubBase
    {
        private readonly IUserConnectionStore _store;
        public RealtimeHub(IUserConnectionStore store)
        {
            _store = store;
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("🔥 OnConnectedAsync");
            Console.WriteLine($"UserIdentifier = {Context.UserIdentifier}");
            Console.WriteLine($"ConnectionId = {Context.ConnectionId}");

            if (Context.UserIdentifier == null)
            {
                Console.WriteLine("❌ UserIdentifier NULL → HANDSHAKE FAIL");
                Context.Abort();
                return;
            }

            var userId = Guid.Parse(Context.UserIdentifier);
            Console.WriteLine("User connected: " + userId + " With connection: " + Context.UserIdentifier);
            await _store.AddAsync(userId, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId}");

            await base.OnConnectedAsync();
            
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Guid.Parse(Context.UserIdentifier!);
            await _store.RemoveAsync(userId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
