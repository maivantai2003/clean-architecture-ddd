using Auth.Application.Abstractions.Persistence;
using RabbitMQ.Client;
namespace Auth.Infrastructure.RabbitMQ
{
    public class RabbitMQChannelFactory : IRabbitMQChannelFactory
    {
        private readonly IRabbitMQConnection _connection;

        public RabbitMQChannelFactory(IRabbitMQConnection connection)
        {
            _connection = connection;
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            var conn = await _connection.GetConnectionAsync();
            return await conn.CreateChannelAsync();
        }
    }
}
