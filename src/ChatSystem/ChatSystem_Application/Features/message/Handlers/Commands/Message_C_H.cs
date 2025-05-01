using AutoMapper;
using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_Application.Event;
using ChatSystem_Application.Features.message.Requests.Commands;
using ChatSystem_Application.Responses;
using ChatSystem_Domain.Model.message;
using MediatR;

namespace ChatSystem_Application.Features.message.Handlers.Commands
{
    public class Message_C_H : IRequestHandler<Message_C_R, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly Imessage_rep _message_rep;
        private readonly IPublisher _publisher;

        public Message_C_H
            (IMapper mapper,
            Imessage_rep message_rep,
            IPublisher publisher)
        {
            _mapper = mapper;
            _message_rep = message_rep;
            _publisher = publisher;
        }

        public async Task<BaseCommandResponse> Handle(Message_C_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            //ToDo
            //validation

            var newMessage = _mapper.Map<Message_E>(request.Message_C_D);
            newMessage.IsRead = false;
            newMessage.SenderId = 1;
            newMessage.ReceiverId = 1;

            await _message_rep.AddAsync(newMessage);

            var event_message = _mapper.Map<MessageSent_EV>(newMessage);
            event_message.Id = newMessage.Id;

            _ = Task.Run(async () => await _publisher.Publish(event_message));// publish MessageSent Event

            response.Data = newMessage.Id;
            response.Message = "با موفقیت اضافه شد";
            response.Success();
            return response;
        }
    }
}
