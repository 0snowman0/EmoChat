using ChatSystem_Application.Dto.Base;
using ChatSystem_Application.Dto.Message.common;

namespace ChatSystem_Application.Dto.Message.Queries
{
    public class Message_G_D : Base_D,Message_CO_D
    {
        public string content { get; set; }
        public bool IsRead { get; set; }
        public string ReplyMessageId { get; set; }
    }
}
