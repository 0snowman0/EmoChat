using Analys.api.contracts.AnalysUtilies;
using Analys.api.Features.analysisProcessor.Requests;
using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.analysisProcessor.Handlers
{
    public class GetTopUserEmojiUsage_H : IRequestHandler<GetTopUserEmojiUsage_R, BaseCommandResponse>
    {
        private readonly IAnalysisProcessor _analysisProcessor;

        public GetTopUserEmojiUsage_H(IAnalysisProcessor analysisProcessor)
        {
            _analysisProcessor = analysisProcessor;
        }

        public async Task<BaseCommandResponse> Handle(GetTopUserEmojiUsage_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var result = await _analysisProcessor.Get_Top_Emoji_For_User
                    (user_id:request.user_id , top:request.top);
                response.Success(result);
                response.Message = "user data successfully recived";
            }
            catch (Exception ex)
            {
                response.ServerError();
                response.Errors = new List<string>() { ex.Message };
            }

            return response;
        }
    }
}
