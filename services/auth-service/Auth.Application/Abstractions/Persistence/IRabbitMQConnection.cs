using RabbitMQ.Client;

namespace Auth.Application.Abstractions.Persistence
{
    public interface IRabbitMQConnection
    {
        Task<IConnection> GetConnectionAsync();
    }
}
