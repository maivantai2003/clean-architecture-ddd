using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Inbox
{
    public interface IInboxMessageRepository
    {
        Task<bool> ExistsAsync(string messageId, string consumer, CancellationToken ct);
        Task AddAsync(InboxMessage message, CancellationToken ct);
    }
}
