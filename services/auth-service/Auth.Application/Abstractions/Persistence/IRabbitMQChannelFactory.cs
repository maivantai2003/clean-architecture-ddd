
using RabbitMQ.Client;
namespace Auth.Application.Abstractions.Persistence
{
    public interface IRabbitMQChannelFactory
    {
        Task<IChannel> CreateChannelAsync();
    }
}
