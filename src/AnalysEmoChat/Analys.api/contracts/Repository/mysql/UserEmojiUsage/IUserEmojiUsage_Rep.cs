using Analys.api.model.user;

namespace Analys.api.contracts.Repository.mysql.UserEmojiUsage
{
    public interface IUserEmojiUsage_Rep : IMySqlRepository<UserEmojiUsage_E>
    {
        Task<UserEmojiUsage_E?> GetBy_emoji_userId(string emoji , int user_id); 
    }
}
