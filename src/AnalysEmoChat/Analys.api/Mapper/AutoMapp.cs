using Analys.api.Dto.UserEmo;
using Analys.api.model.user;
using AutoMapper;

namespace Analys.api.Mapper
{
    public class AutoMapp : Profile
    {
        public AutoMapp()
        {
            #region User

            CreateMap<UserEmojiUsage_E , UserEmojiUsageInRedis>().ReverseMap();

            #endregion
        }
    }
}
