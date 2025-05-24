using Analys.api.contracts.Repository.redis;
using Analys.api.Dto.UserEmo;
using Analys.api.Features.UserEmo.Requests;
using Analys.api.model.user;
using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.UserEmo.Handlers
{
    public class SyncUserEmojisFromRedis_H : IRequestHandler<SyncUserEmojisFromRedis_R, BaseCommandResponse>
    {
        private readonly IRedisUserEmoji _redisUserEmoji;
        private readonly IMediator _mediator;

        public SyncUserEmojisFromRedis_H(IRedisUserEmoji redisUserEmoji, IMediator mediator)
        {
            _redisUserEmoji = redisUserEmoji;
            _mediator = mediator;
        }

        public async Task<BaseCommandResponse> Handle(SyncUserEmojisFromRedis_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var redisData = await _redisUserEmoji.GetAllAsync();

                var mappedData = redisData.Select(x => new UserEmojiUsageInRedis
                {
                    UserId = x.UserId,
                    Emoji = x.Emoji,
                    Count = x.Count,
                }).ToList();

                var mysqlResponse = await _mediator.Send(new AddUserAnalysInfo_R
                {
                    UserEmojiUsageInRedis = mappedData
                });

                if (mysqlResponse.IsSuccess)
                {
                    await _redisUserEmoji.ClearAllAsync(); 
                    response.Success();
                    response.Message = "redis data is clear";
                }
                else
                {
                    response.ServerError();
                    response.Message = mysqlResponse.Message;
                    response.Errors = mysqlResponse.Errors;
                    response.Data = mysqlResponse.Data;
                }
            }
            catch (Exception ex)
            {
                response.ServerError();
                response.Errors = new List<string> { ex.Message};
            }

            return response;
        }
    }

}
