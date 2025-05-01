using ChatSystem_Application.Dto.Message.common;

namespace ChatSystem_Application.Dto.Message.command
{
    public class Message_U_D : Message_CO_D
    {
        public string message_id { get; set; } = null!;
        public string content { get; set; } = null!;
    }
}
