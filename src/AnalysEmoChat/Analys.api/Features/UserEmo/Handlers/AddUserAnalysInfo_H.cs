using Analys.api.contracts.Repository.mysql.UserEmojiUsage;
using Analys.api.Features.UserEmo.Requests;
using Analys.api.Implenemetation.Repository.mysql.UserEmojiUsage;
using Analys.api.model.user;
using AutoMapper;
using ChatSystem_Application.Responses;
using MediatR;

namespace Analys.api.Features.UserEmo.Handlers
{
    public class AddUserAnalysInfo_H : IRequestHandler<AddUserAnalysInfo_R, BaseCommandResponse>
    {
        private readonly IUserEmojiUsage_Rep _userEmojiUsage_Rep;
        private readonly IMapper _mapper;
        public AddUserAnalysInfo_H(IUserEmojiUsage_Rep userEmojiUsage_Rep, IMapper mapper)
        {
            this._userEmojiUsage_Rep = userEmojiUsage_Rep;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(AddUserAnalysInfo_R request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            try
            {
                var userEmojiUsage_Es = _mapper.Map<List<UserEmojiUsage_E>>(request.UserEmojiUsageInRedis);

                foreach (var ue in userEmojiUsage_Es)
                {
                    var target = await _userEmojiUsage_Rep.GetBy_emoji_userId(emoji: ue.Emoji, user_id: ue.UserId);

                    if (target is not null)
                    {
                        target.Count += ue.Count;
                        await _userEmojiUsage_Rep.SaveAsync();
                    }

                    else await _userEmojiUsage_Rep.AddAsync(ue);
                    await _userEmojiUsage_Rep.SaveAsync();
                }


                response.Success(data: userEmojiUsage_Es);
                response.Message = $"{userEmojiUsage_Es.Count} records inserted successfully.";

            }
            catch (Exception ex)
            {
                response.ServerError();
                response.Errors = new List<string> { ex.Message, ex.InnerException?.Message };
            }

            return response;
        }
    }
}
