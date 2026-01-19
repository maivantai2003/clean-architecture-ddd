
using RabbitMQ.Client;
namespace User.Application.Abstractions.Persistence
{
    public interface IRabbitMQChannelFactory
    {
        Task<IChannel> CreateChannelAsync();
    }
}
