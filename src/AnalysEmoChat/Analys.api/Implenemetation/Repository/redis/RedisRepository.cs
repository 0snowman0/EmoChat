using Analys.api.config.settings;
using Analys.api.contracts.Repository.redis;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace Analys.api.Implenemetation.Repository.redis
{
    public class RedisRepository<T> : IRedisRepository<T> where T : class
    {
        private readonly IDatabase _redisDb;
        private readonly string _instanceName;
        private readonly int _defaultCacheTime;

        public RedisRepository(IConnectionMultiplexer redis, IOptions<RedisSettings> settings)
        {
            _redisDb = redis.GetDatabase();
            _instanceName = settings.Value.InstanceName;
            _defaultCacheTime = settings.Value.DefaultCacheTimeInMinutes;
        }

        private string GetKey(string key) => $"{_instanceName}{key}";

        public async Task SetAsync(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _redisDb.StringSetAsync(GetKey(key), serializedValue, expiry ?? TimeSpan.FromMinutes(_defaultCacheTime));
        }

        public async Task<T> GetAsync(string key)
        {
            var value = await _redisDb.StringGetAsync(GetKey(key));
            return value.IsNull ? null : JsonSerializer.Deserialize<T>(value);
        }
        public async Task<bool> RemoveAsync(string key)
            => await _redisDb.KeyDeleteAsync(GetKey(key));

        public async Task<bool> ExistsAsync(string key)
            => await _redisDb.KeyExistsAsync(GetKey(key));


        public async Task<bool> ExtendExpiryAsync(string key, TimeSpan? expiry = null)
            => await _redisDb.KeyExpireAsync(GetKey(key), expiry ?? TimeSpan.FromMinutes(_defaultCacheTime));


        public async Task SetInHashAsync(string hashKey, string field, T value)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _redisDb.HashSetAsync(GetKey(hashKey), field, serializedValue);
        }

        public async Task<T> GetFromHashAsync(string hashKey, string field)
        {
            var value = await _redisDb.HashGetAsync(GetKey(hashKey), field);
            return value.IsNull ? null : JsonSerializer.Deserialize<T>(value);
        }
    }
}
