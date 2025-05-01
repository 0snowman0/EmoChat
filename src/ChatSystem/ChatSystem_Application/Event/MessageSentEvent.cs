using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RabbitMQEventBus.Contracts;

namespace ChatSystem_Application.Event
{
    public class MessageSent_EV : IEvent , INotification
    {
        //ToDo
        //public MessageSentEvent(string content)
        //{

        //}
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? content { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool IsRead { get; set; }
        public string? ReplyMessageId { get; set; }

        public Guid EventId { get; set; }

        public DateTime OccurredOn { get; set; }

    }
}
