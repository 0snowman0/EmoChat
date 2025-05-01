namespace Analys.api.contracts.Repository
{
    public interface IRedisRepository<T> where T : class
    {
        Task SetAsync(string key, T value, TimeSpan? expiry = null);
        Task<T> GetAsync(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task<bool> ExtendExpiryAsync(string key, TimeSpan? expiry = null);
        Task SetInHashAsync(string hashKey, string field, T value);
        Task<T> GetFromHashAsync(string hashKey, string field);
    }
}
