using ChatSystem_Application.Responses;
using MediatR;
using System.Security.Principal;

namespace Analys.api.Features.analysisProcessor.Requests
{
    public class GetSecondPageTopEmojiForUser_R : IRequest<BaseCommandResponse>
    {
        public int user_id { get; set; }
        public int pagesize { get; set; }
    }
}
