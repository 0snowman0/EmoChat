using MassTransit;
using RabbitMQEventBus.Contracts;

namespace RabbitMQEventBus.Implementations
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RabbitMQEventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}
