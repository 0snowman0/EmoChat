using Analys.api.Dto.UserEmo;
using Analys.api.model.user;
using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.UserEmo.Requests
{
    public class AddUserAnalysInfo_R : IRequest<BaseCommandResponse>
    {
        public List<UserEmojiUsageInRedis> UserEmojiUsageInRedis { get; set; } = null!;
    }
}
