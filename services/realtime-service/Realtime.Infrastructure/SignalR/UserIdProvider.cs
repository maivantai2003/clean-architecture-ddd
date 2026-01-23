using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Realtime.Infrastructure.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? connection.User?.FindFirst("sub")?.Value;
        }
    }
}
