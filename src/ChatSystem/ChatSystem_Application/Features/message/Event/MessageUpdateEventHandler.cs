using AutoMapper;
using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_Application.Event;
using ChatSystem_Domain.Model.message;
using MediatR;

namespace ChatSystem_Application.Features.message.Event
{
    public class MessageUpdateEventHandler : INotificationHandler<MessageUpdate_EV>
    {
        private readonly Imessage_rep _message_rep;
        private readonly IMapper _mapper;
        protected readonly string _defaultReadDbName;
        public MessageUpdateEventHandler(Imessage_rep message_rep, IMapper mapper)
        {
            _message_rep = message_rep;
            _defaultReadDbName = "ChatSystem_ReadDB"; // اینجا باید درست بشه اسم دیتابیس هارد کد داره نوشته میشه
            _mapper = mapper;
        }

        public async Task Handle(MessageUpdate_EV notification, CancellationToken cancellationToken)
        {
            var target = await _message_rep.GetByIdAsync(notification.message_id);

            if(target is not null)
            {
                _mapper.Map(notification , target);
                await _message_rep.UpdateAsync(target , databaseName: _defaultReadDbName);
            }
        }
    }
}
