using User.Domain.Common;

namespace User.Application.Outbox
{
    public interface IOutboxRepository
    {
        Task SaveEventAsync(DomainEvent domainEvent);
    }
}
