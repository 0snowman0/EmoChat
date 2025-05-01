namespace ChatSystem_Application.Dto.Message.Queries
{
    public class Message_Get_Request_D
    {
        public int senderId { get; set; }
        public int order { get; set; }
        public int receiverId { get; set; }
    }
}
