using Analys.api.contracts.AnalysUtilies;
using Analys.api.Features.analysisProcessor.Requests;
using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.analysisProcessor.Handlers
{
    public class GetSecondPageTopUserEmojiUsage_H : IRequestHandler<GetSecondPageTopUserEmojiUsage_R, BaseCommandResponse>
    {
        private readonly IAnalysisProcessor _analysisProcessor;

        public GetSecondPageTopUserEmojiUsage_H(IAnalysisProcessor analysisProcessor)
        {
            _analysisProcessor = analysisProcessor;
        }

        public async Task<BaseCommandResponse> Handle(GetSecondPageTopUserEmojiUsage_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var result = await _analysisProcessor.Get_SecondPage_Top_UserEmojiUsage
                    (pageSize:request.pagesize , emoji:request.emoji);
                if (!result.Any())
                {
                    response.NotFound();
                    return response;
                }
                response.Success(result);
            }
            catch (Exception ex)
            {
                response.ServerError();
                response.Errors = new List<string>() { ex.Message};
            }
            return response;
        }
    }
}
