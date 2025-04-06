using ChatSystem_Application.Responses;
using MediatR;

namespace ChatSystem_Application.Features.message.Requests.Queries
{
    public class Message_GA_R : IRequest<BaseCommandResponse>
    {
        public int senderID { get; set; }
        public int ReceiverID { get; set; }
        public int order { get; set; }
    }
}
