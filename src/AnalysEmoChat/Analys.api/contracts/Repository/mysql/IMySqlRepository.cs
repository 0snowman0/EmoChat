using System.Linq.Expressions;

namespace Analys.api.contracts.Repository.mysql
{
    public interface IMySqlRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task DeleteByIdAsync(object id);
        Task DeleteByIdsAsync(IEnumerable<object> ids);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        Task UpdateAsync(T entity);
        Task SaveAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
