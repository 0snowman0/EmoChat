using Analys.api.contracts.Repository.redis;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Analys.api.Implenemetation.Repository.redis
{
    public class RedisRepository<T> : IRedisRepository<T> where T : class
    {
        private readonly IDistributedCache _cache;

        public RedisRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
            };

            var serializedValue = JsonConvert.SerializeObject(value);
            await _cache.SetStringAsync(key, serializedValue, options);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var serializedValue = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(serializedValue))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(serializedValue);
        }


        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            var value = await _cache.GetStringAsync(key);
            return !string.IsNullOrEmpty(value);
        }
    }
}
