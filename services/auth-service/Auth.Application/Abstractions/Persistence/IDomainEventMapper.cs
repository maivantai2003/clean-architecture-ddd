
namespace Auth.Application.Abstractions.Persistence
{
    public interface IDomainEventMapper
    {
        object Map(object domainEvent);
    }
}
