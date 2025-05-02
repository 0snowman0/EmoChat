namespace Analys.api.Dto.UserEmo
{
    public class UserEmojiUsageInRedis
    {
        public int UserId { get; set; }

        public string Emoji { get; set; } = null!;

        public int Count { get; set; }

    }
}
