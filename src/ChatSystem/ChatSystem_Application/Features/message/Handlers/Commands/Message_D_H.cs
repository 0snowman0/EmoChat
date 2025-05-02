using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_Application.Event;
using ChatSystem_Application.Features.message.Requests.Commands;
using ChatSystem_Application.Responses;
using MediatR;

namespace ChatSystem_Application.Features.message.Handlers.Commands
{
    public class Message_D_H : IRequestHandler<Message_D_R, BaseCommandResponse>
    {
        private readonly Imessage_rep _message_Rep;
        private readonly IPublisher _publisher;
        public Message_D_H(Imessage_rep message_Rep, IPublisher publisher)
        {
            _message_Rep = message_Rep;
            _publisher = publisher;
        }

        public async Task<BaseCommandResponse> Handle(Message_D_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                await _message_Rep.DeleteByIdAsync(request.Id.Trim());

                MessageRemove_EV messageRemoveEvent = new MessageRemove_EV()
                {
                    Id = request.Id.Trim()
                };

                _ = Task.Run(async () => await _publisher.Publish(messageRemoveEvent));
                
                response.Delete();
            }
            catch (Exception ex)
            {
                response.ServerError();
                response.Errors = new List<string>() { ex.Message };
            }
            return response;
        }
    }
}
