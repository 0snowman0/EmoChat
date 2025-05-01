using AutoMapper;
using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_Application.Event;
using ChatSystem_Application.Features.message.Requests.Commands;
using ChatSystem_Application.Responses;
using MediatR;
using RabbitMQ.Client.Logging;

namespace ChatSystem_Application.Features.message.Handlers.Commands
{
    public class Message_U_H : IRequestHandler<Message_U_R, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly Imessage_rep _message_rep;
        private readonly IPublisher _publisher;
        protected readonly string _defaultWriteDbName;
        public Message_U_H(IMapper mapper, Imessage_rep message_rep, IPublisher publisher)
        {
            _mapper = mapper;
            _message_rep = message_rep;
            _publisher = publisher;
            _defaultWriteDbName = "ChatSystem_WriteDB";
        }

        public async Task<BaseCommandResponse> Handle(Message_U_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var Target = await _message_rep.GetByIdAsync
                (request.Message_U_D.message_id , databaseName:_defaultWriteDbName);

            if(Target is null)
            {
                response.NotFound();
                response.Message = "پیامی یافت نشد";
                return response;
            }

            _mapper.Map(request.Message_U_D , Target);

            try
            {
                await _message_rep.UpdateAsync(Target);

                MessageUpdate_EV messageUpdate_EV = _mapper.Map<MessageUpdate_EV>(Target);
                messageUpdate_EV.message_id = Target.Id;

                _ = Task.Run(async ()=> await _publisher.Publish(messageUpdate_EV));
            }
            catch(Exception ex)
            {
                response.ServerError();
                response.Errors = new List<string>() {ex.ToString() };
            }
            return response;
        }
    }
}
