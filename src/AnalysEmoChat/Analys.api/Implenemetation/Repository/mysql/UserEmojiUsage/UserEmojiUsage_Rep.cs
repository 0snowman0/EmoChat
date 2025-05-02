using Analys.api.config.database;
using Analys.api.contracts.Repository.mysql.UserEmojiUsage;
using Analys.api.model.user;

namespace Analys.api.Implenemetation.Repository.mysql.UserEmojiUsage
{
    public class UserEmojiUsage_Rep : MySqlRepository<UserEmojiUsage_E>,IUserEmojiUsage_Rep
    {
        private readonly AppDbContext _context;

        public UserEmojiUsage_Rep(AppDbContext context) : base(context)
        {
            _context = context;
        }



    }
}
