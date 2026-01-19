using RabbitMQ.Client;

namespace User.Application.Abstractions.Persistence
{
    public interface IRabbitMQConnection
    {
        Task<IConnection> GetConnectionAsync();
    }
}
