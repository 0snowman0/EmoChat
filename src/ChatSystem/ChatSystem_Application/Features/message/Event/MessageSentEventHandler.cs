using AutoMapper;
using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_Application.Event;
using ChatSystem_Domain.Model.message;
using MediatR;
using System.Net.NetworkInformation;

namespace ChatSystem_Application.Features.message.Event
{
    public class MessageSentEventHandler : INotificationHandler<MessageSent_EV>
    {
        private readonly Imessage_rep _message_rep;
        private readonly IMapper _mapper;
        protected readonly string _defaultReadDbName;
        public MessageSentEventHandler(Imessage_rep message_rep, IMapper mapper)
        {
            _message_rep = message_rep;
            _defaultReadDbName = "ChatSystem_ReadDB"; // اینجا باید درست بشه اسم دیتابیس هارد کد داره نوشته میشه
            _mapper = mapper;
        }

        public async Task Handle(MessageSent_EV notification, CancellationToken cancellationToken)
        {
            var NewMessage = _mapper.Map<Message_E>(notification);

            await _message_rep.AddAsync(NewMessage , databaseName:_defaultReadDbName);
        }
    }
}
