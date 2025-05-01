using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatSystem_Domain.Model.Base
{
    public class Base_E
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; } 
    }
}
