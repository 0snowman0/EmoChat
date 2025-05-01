using ChatSystem_Application.Dto.Message.command;
using MediatR;
using RabbitMQEventBus.Contracts;

namespace ChatSystem_Application.Event
{
    public class MessageUpdate_EV : Message_U_D, IEvent, INotification
    {
        public Guid EventId { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
