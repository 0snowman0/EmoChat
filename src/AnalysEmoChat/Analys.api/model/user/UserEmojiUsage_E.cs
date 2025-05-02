using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Analys.api.model.user
{
    [Table("userEmojiUsage")]
    public class UserEmojiUsage_E
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Emoji { get; set; } = null!;

        public int Count { get; set; }

        public DateTime LastUpdated { get; set; } 

    }
}
