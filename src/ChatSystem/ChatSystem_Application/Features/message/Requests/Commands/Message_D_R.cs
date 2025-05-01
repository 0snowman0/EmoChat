using ChatSystem_Application.Responses;
using MediatR;

namespace ChatSystem_Application.Features.message.Requests.Commands
{
    public class Message_D_R : IRequest<BaseCommandResponse>
    {
        public string Id { get; set; } = null!;
    }
}
