using ChatSystem_EventBus.abstract_;
using MassTransit;

public class RabbitMQEventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMQEventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
    {
        await _publishEndpoint.Publish(@event);
    }
}
