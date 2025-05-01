using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_Application.Event;
using MediatR;

namespace ChatSystem_Application.Features.message.Event
{
    public class MessageRemoveEventHandler : INotificationHandler<MessageRemove_EV>
    {
        private readonly Imessage_rep _message_rep;
        protected readonly string _defaultReadDbName;

        public MessageRemoveEventHandler(Imessage_rep message_rep)
        {
            _message_rep = message_rep;
            _defaultReadDbName = "ChatSystem_ReadDB";
        }

        public async Task Handle(MessageRemove_EV notification, CancellationToken cancellationToken)
        {
            await _message_rep.DeleteByIdAsync(notification.Id ,databaseName:_defaultReadDbName);
        }
    }
}
