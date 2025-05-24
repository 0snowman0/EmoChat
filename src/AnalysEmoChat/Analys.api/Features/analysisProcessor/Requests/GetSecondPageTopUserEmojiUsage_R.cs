using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.analysisProcessor.Requests
{
    public class GetSecondPageTopUserEmojiUsage_R: IRequest<BaseCommandResponse>
    {
        public int pagesize { get; set; }
        public string emoji { get; set; } = null!;
    }
}
