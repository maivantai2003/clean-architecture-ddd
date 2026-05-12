namespace Chat.Infrastructure.Messaging
{
    public sealed class DomainEventMapper
    {
        public object Map(object domainEvent)
        {
            return domainEvent;
        }
    }
}
