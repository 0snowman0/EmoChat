using MediatR;
using RabbitMQEventBus.Contracts;

namespace ChatSystem_Application.Event
{
    public class MessageRemove_EV : IEvent, INotification
    {
        public string Id { get; set; }
        public Guid EventId { get; set; }

        public DateTime OccurredOn { get; set; }
    }
}
