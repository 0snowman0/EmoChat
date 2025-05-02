using Analys.api.Dto.UserEmo;
using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.UserEmo.Requests
{
    public class SendMessage_R : IRequest<BaseCommandResponse>
    {
        public SendMessage_D SendMessage_D { get; set; } = null!;
    }
}
