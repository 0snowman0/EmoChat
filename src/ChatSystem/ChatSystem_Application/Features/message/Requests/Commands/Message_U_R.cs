using ChatSystem_Application.Dto.Message.command;
using ChatSystem_Application.Responses;
using MediatR;

namespace ChatSystem_Application.Features.message.Requests.Commands
{
    public class Message_U_R : IRequest<BaseCommandResponse>
    {
        public Message_U_D Message_U_D { get; set; } = null!;
    }
}
