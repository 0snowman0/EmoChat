using Analys.api.contracts.Repository.redis;
using Analys.api.Dto.UserEmo;
using StackExchange.Redis;

namespace Analys.api.Implenemetation.Repository.redis
{
    public class RedisUserEmoji : IRedisUserEmoji
    {
        private readonly IDatabase _database;
        private const string KEY_PREFIX = "userEmoji:";

        public RedisUserEmoji(ConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        private string GetKey(string userId) => $"{KEY_PREFIX}{userId}";

        public async Task IncrementEmojiUsageAsync(string userId, string emoji)
        {
            await _database.HashIncrementAsync(GetKey(userId), emoji, 1);
        }

        public async Task<List<UserEmojiUsageInRedis>> GetAllAsync()
        {
            var server = _database.Multiplexer.GetServer(_database.Multiplexer.GetEndPoints().First());
            var keys = server.Keys(pattern: $"{KEY_PREFIX}*").ToArray();

            var result = new List<UserEmojiUsageInRedis>();

            foreach (var key in keys)
            {
                string userIdStr = key.ToString().Replace(KEY_PREFIX, "");
                if (!int.TryParse(userIdStr, out int userId))
                    continue;

                var hashEntries = await _database.HashGetAllAsync(key);
                foreach (var entry in hashEntries)
                {
                    result.Add(new UserEmojiUsageInRedis
                    {
                        UserId = userId,
                        Emoji = entry.Name!,
                        Count = (int)entry.Value
                    });
                }
            }

            return result;
        }

        public async Task ClearAllAsync()
        {
            var server = _database.Multiplexer.GetServer(_database.Multiplexer.GetEndPoints().First());
            var keys = server.Keys(pattern: $"{KEY_PREFIX}*").ToArray();

            foreach (var key in keys)
            {
                await _database.KeyDeleteAsync(key);
            }
        }

        public async Task<Dictionary<string, long>> GetEmojiUsageAsync(string userId)
        {
            var emojiData = await _database.HashGetAllAsync(GetKey(userId));

            return emojiData.ToDictionary(
                x => x.Name.ToString(),
                x => (long)x.Value
            );
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            return await _database.KeyExistsAsync(GetKey(userId));
        }

        public async Task RemoveAsync(string userId)
        {
            await _database.KeyDeleteAsync(GetKey(userId));
        }

        public async Task SetExpirationAsync(string userId, TimeSpan expiration)
        {
            await _database.KeyExpireAsync(GetKey(userId), expiration);
        }

        public IEnumerable<string> ExtractEmojis(string input)
        {
            return input
                .Where(char.IsSurrogate)
                .Select(c => c.ToString());
        }
    }

}
