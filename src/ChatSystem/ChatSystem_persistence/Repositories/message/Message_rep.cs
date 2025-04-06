using ChatSystem_Application.Contracts.IGenericRepository;
using ChatSystem_Domain.Model.message;
using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_persistence.DataBaseConfig;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;

namespace ChatSystem_persistence.Repositories.message
{
    public class Message_rep : GenericRepository<Message_E>, Imessage_rep
    {
        public Message_rep(
            IMongoClient mongoClient,
            MongoDBSettings settings)
            : base(mongoClient, settings)
        {
        }

        public async Task<List<Message_E>> GetByOrder(int senderId, int receiverId, int order)
        {
            int skipCount = (order - 1) * 100;
            int takeCount = 100;

            var filter = Builders<Message_E>.Filter.And(
                Builders<Message_E>.Filter.Eq(m => m.SenderId, senderId),
                Builders<Message_E>.Filter.Eq(m => m.ReceiverId, receiverId)
            );

            return await GetCollection(_defaultReadDbName)
                .Find(filter)
                .SortByDescending(m => m.CreatedAt)
                .Skip(skipCount)
                .Limit(takeCount)
                .ToListAsync();
        }
    }
}
