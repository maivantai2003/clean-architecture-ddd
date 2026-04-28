namespace Search.Application.Abstractions.Persistence
{
    public interface IDomainEventMapper
    {
        object Map(object domainEvent);
    }
}
