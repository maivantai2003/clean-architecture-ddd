using Search.Domain.Common;

namespace Search.Application.Outbox
{
    public interface IOutboxRepository
    {
        Task SaveEventAsync(DomainEvent domainEvent);
    }
}
