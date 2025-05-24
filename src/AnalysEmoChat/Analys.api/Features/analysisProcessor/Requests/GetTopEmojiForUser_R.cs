using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.analysisProcessor.Requests
{
    public class GetTopEmojiForUser_R : IRequest<BaseCommandResponse>
    {
        public int user_id { get; set; }
        public int top { get; set; }
    }
}
