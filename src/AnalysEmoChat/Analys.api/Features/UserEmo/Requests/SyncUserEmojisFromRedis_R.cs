using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.UserEmo.Requests
{
    public class SyncUserEmojisFromRedis_R : IRequest<BaseCommandResponse>
    {
    }
}
