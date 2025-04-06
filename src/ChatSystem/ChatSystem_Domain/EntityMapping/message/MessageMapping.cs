using ChatSystem_Domain.Model.message;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem_Domain.EntityMapping.message
{
    public static class MessageMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Message_E>(cm =>
            {
                cm.AutoMap(); 
                cm.MapMember(m => m.content)
                    .SetElementName("content")
                    .SetIsRequired(true);

                cm.MapMember(m => m.SenderId)
                    .SetElementName("sender_id");

                cm.MapMember(m => m.ReceiverId)
                    .SetElementName("receiver_id");

                cm.MapMember(m => m.IsRead)
                    .SetElementName("is_read")
                    .SetDefaultValue(false);

                cm.MapMember(m => m.ReplyMessageId)
                    .SetElementName("reply_to")
                    .SetIgnoreIfNull(true);
            });
        }
    }
}
