using Auth.Domain.Common;

namespace Auth.Application.Outbox
{
    public interface IOutboxRepository
    {
        Task SaveEventAsync(DomainEvent domainEvent);
    }
}
