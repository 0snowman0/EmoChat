using ChatSystem_Application.Dto.Message.command;
using ChatSystem_Application.Responses;
using MediatR;

namespace ChatSystem_Application.Features.message.Requests.Commands
{
    public class Message_C_R : IRequest<BaseCommandResponse>
    {
        public Message_C_D Message_C_D { get; set; } = null!;
    }
}
