using ChatSystem_Domain.Model.Base;
using MongoDB.Bson.Serialization;

namespace ChatSystem_Domain.EntityMapping.Base
{
    public static class BaseEntityMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Base_E>(cm =>
            {
                cm.MapMember(b => b.Id)
                    .SetElementName("_id")
                    .SetIsRequired(true);

                cm.MapMember(b => b.CreatedAt)
                    .SetElementName("created_at")
                    .SetDefaultValue(DateTime.UtcNow);

                cm.MapMember(b => b.ModifiedAt)
                    .SetElementName("modified_at")
                    .SetIgnoreIfNull(true);
            });
        }
    }
}
