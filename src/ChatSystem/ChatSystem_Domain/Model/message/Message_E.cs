using ChatSystem_Domain.attribute;
using ChatSystem_Domain.Model.Base;

namespace ChatSystem_Domain.Model.message
{
    [CollectionNameAttribute("messages")]
    public class Message_E : Base_E
    {
        public string content { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public bool IsRead { get; set; }
        public string ReplyMessageId { get; set; }
    }
}
