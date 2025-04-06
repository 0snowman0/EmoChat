using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem_Application.Contracts.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        // ======== متدهای اصلی (استفاده از دیتابیس‌های پیش‌فرض) ========
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(string id);
        Task DeleteByIdsAsync(IEnumerable<string> ids);

        // ======== اورلودها با نام دیتابیس دلخواه ========
        Task<T> GetByIdAsync(string id, string databaseName);
        Task<IEnumerable<T>> GetAllAsync(
            string databaseName,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<bool> ExistsAsync(string databaseName, Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity, string databaseName);
        Task AddRangeAsync(IEnumerable<T> entities, string databaseName);
        Task UpdateAsync(T entity, string databaseName);
        Task DeleteByIdAsync(string id, string databaseName);
        Task DeleteByIdsAsync(IEnumerable<string> ids, string databaseName);
    }
}
