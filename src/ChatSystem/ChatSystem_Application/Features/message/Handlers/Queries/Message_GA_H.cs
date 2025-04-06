using AutoMapper;
using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_Application.Features.message.Requests.Queries;
using ChatSystem_Application.Responses;
using ChatSystem_Domain.Model.message;
using MediatR;

namespace ChatSystem_Application.Features.message.Handlers.Queries
{
    public class Message_GA_H : IRequestHandler<Message_GA_R, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly Imessage_rep _message_rep;

        public Message_GA_H(IMapper mapper, Imessage_rep message_rep)
        {
            _mapper = mapper;
            _message_rep = message_rep;
        }

        public async Task<BaseCommandResponse> Handle(Message_GA_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var messages = await _message_rep.GetByOrder
                (
                sender:request.senderID,
                reciver:request.ReceiverID,
                order:request.order
                );
            if (!messages.Any())
            {
                response.NotFound();
                response.Message = "پیامی یافت نشد";
                return response;
            }

            //ToDo
            response.Data = messages;
            response.Success();
            return response;
        }
    }
}
