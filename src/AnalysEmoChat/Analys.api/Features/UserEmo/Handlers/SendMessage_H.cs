using Analys.api.contracts.Repository.redis;
using Analys.api.Features.UserEmo.Requests;
using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.UserEmo.Handlers
{
    public class SendMessage_H : IRequestHandler<SendMessage_R, BaseCommandResponse>
    {
        private readonly IRedisUserEmoji _userEmoji;

        public SendMessage_H(IRedisUserEmoji userEmoji)
        {
            _userEmoji = userEmoji;
        }

        public async Task<BaseCommandResponse> Handle(SendMessage_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var userId = request.SendMessage_D.user_id;
            var message = request.SendMessage_D.message;

            var emojis = _userEmoji.ExtractEmojis(message);

            foreach (var emoji in emojis)
                await _userEmoji.IncrementEmojiUsageAsync(userId.ToString(), emoji);

            response.Success(); 
            response.Message = "Emojis processed successfully";

            return response;
        }
    }
}
