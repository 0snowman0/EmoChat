using Analys.api.config.database;
using Analys.api.contracts.Repository.mysql.UserEmojiUsage;
using Analys.api.model.user;
using Microsoft.EntityFrameworkCore;

namespace Analys.api.Implenemetation.Repository.mysql.UserEmojiUsage
{
    public class UserEmojiUsage_Rep : MySqlRepository<UserEmojiUsage_E>, IUserEmojiUsage_Rep
    {
        private readonly AppDbContext _context;

        public UserEmojiUsage_Rep(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserEmojiUsage_E?> GetBy_emoji_userId(string emoji, int user_id)
            => await _context.userEmojiUsage_Es.Where(p => p.UserId == user_id && p.Emoji == emoji).FirstOrDefaultAsync();
    }
}
