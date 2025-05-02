using Analys.api.Dto.UserEmo;

namespace Analys.api.contracts.Repository.redis
{
    public interface IRedisUserEmoji
    {
        Task IncrementEmojiUsageAsync(string userId, string emoji);
        Task<Dictionary<string, long>> GetEmojiUsageAsync(string userId);
        Task<bool> ExistsAsync(string userId);
        Task RemoveAsync(string userId);
        Task SetExpirationAsync(string userId, TimeSpan expiration);
        IEnumerable<string> ExtractEmojis(string input);

        Task<List<UserEmojiUsageInRedis>> GetAllAsync();
        Task ClearAllAsync();
    }
}
