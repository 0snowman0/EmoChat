using ChatSystem_Application.Contracts.IGenericRepository;
using ChatSystem_Domain.attribute;
using ChatSystem_persistence.DataBaseConfig;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ChatSystem_persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IMongoClient _mongoClient;
        protected readonly string _defaultWriteDbName;
        protected readonly string _defaultReadDbName;
        protected readonly string _collectionName;

        public GenericRepository(
            IMongoClient mongoClient,
            MongoDBSettings settings)
        {
            _mongoClient = mongoClient;
            _defaultWriteDbName = settings.WriteDatabase.DatabaseName;
            _defaultReadDbName = settings.ReadDatabase.DatabaseName;
            _collectionName = GetCollectionName();
        }

        // ======== Utility Methods ========
        private string GetCollectionName()
        {
            var collectionNameAttribute = typeof(T).GetCustomAttribute<CollectionNameAttribute>();

            return collectionNameAttribute?.TableName ??
                   typeof(T).Name.ToLower() + "s";
        }

        public IMongoCollection<T> GetCollection(string databaseName)
        {
            return _mongoClient.GetDatabase(databaseName)
                .GetCollection<T>(_collectionName);
        }

        // ======== Default Operations (Auto Read/Write Separation) ========
        public async Task<T> GetByIdAsync(string id)
            => await GetByIdAsync(id, _defaultReadDbName);

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
            => await GetAllAsync(_defaultReadDbName, filter, orderBy);

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
            => await ExistsAsync(_defaultReadDbName, predicate);

        public async Task AddAsync(T entity)
            => await AddAsync(entity, _defaultWriteDbName);

        public async Task AddRangeAsync(IEnumerable<T> entities)
            => await AddRangeAsync(entities, _defaultWriteDbName);

        public async Task UpdateAsync(T entity)
            => await UpdateAsync(entity, _defaultWriteDbName);

        public async Task DeleteByIdAsync(string id)
            => await DeleteByIdAsync(id, _defaultWriteDbName);

        public async Task DeleteByIdsAsync(IEnumerable<string> ids)
            => await DeleteByIdsAsync(ids, _defaultWriteDbName);

        // ======== Overloads with Database Name Parameter ========
        public async Task<T> GetByIdAsync(string id, string databaseName)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await GetCollection(databaseName)
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            string databaseName,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = GetCollection(databaseName).AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await Task.FromResult(query.ToList());
        }

        public async Task<bool> ExistsAsync(
            string databaseName,
            Expression<Func<T, bool>> predicate)
        {
            return await GetCollection(databaseName)
                .Find(predicate)
                .AnyAsync();
        }

        public async Task AddAsync(T entity, string databaseName)
        {
            await GetCollection(databaseName)
                .InsertOneAsync(entity);
        }

        public async Task AddRangeAsync(
            IEnumerable<T> entities,
            string databaseName)
        {
            await GetCollection(databaseName)
                .InsertManyAsync(entities);
        }

        public async Task UpdateAsync(T entity, string databaseName)
        {
            var id = typeof(T).GetProperty("Id")?.GetValue(entity)?.ToString();
            var filter = Builders<T>.Filter.Eq("_id", id);

            await GetCollection(databaseName)
                .ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteByIdAsync(string id, string databaseName)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await GetCollection(databaseName)
                .DeleteOneAsync(filter);
        }

        public async Task DeleteByIdsAsync(
            IEnumerable<string> ids,
            string databaseName)
        {
            var filter = Builders<T>.Filter.In("_id", ids);
            await GetCollection(databaseName)
                .DeleteManyAsync(filter);
        }

    }
}
