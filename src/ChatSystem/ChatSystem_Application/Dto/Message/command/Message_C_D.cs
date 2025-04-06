using ChatSystem_Application.Dto.Message.common;

namespace ChatSystem_Application.Dto.Message.command
{
    public class Message_C_D : Message_CO_D
    {
        public string content { get; set; }
        public int Receiver_IdInApp { get; set; }
        public string ReplyMessageId { get; set; }
    }
}
