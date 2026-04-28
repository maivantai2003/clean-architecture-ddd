namespace Search.Infrastructure.Messaging.Idempotent
{
    public interface IIdempotentExecutor
    {
        Task ExecuteAsync(string messageId,string consumer,Func<Task> handler,CancellationToken ct);
    }
}
