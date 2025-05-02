namespace Analys.api.contracts.Repository.redis
{
    public interface IRedisRepository<T> where T : class
    {
        Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null);
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
    }
}
