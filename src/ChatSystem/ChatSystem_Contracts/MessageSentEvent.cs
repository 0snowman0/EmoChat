namespace ChatSystem_Contracts
{
    public class MessageSentEvent : Event
    {
        public Guid MessageId { get; }
        public string Content { get; }
        public string Sender { get; }

        public MessageSentEvent(Guid messageId, string content, string sender)
        {
            MessageId = messageId;
            Content = content;
            Sender = sender;
        }
    }
}
