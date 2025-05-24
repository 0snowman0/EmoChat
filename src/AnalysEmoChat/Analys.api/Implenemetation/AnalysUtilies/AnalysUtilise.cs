using Analys.api.config.database;
using Analys.api.contracts.AnalysUtilies;
using Microsoft.EntityFrameworkCore;

namespace Analys.api.Implenemetation.AnalysUtilies
{
    public class AnalysisProcessor : IAnalysisProcessor
    {

        private readonly AppDbContext _context;

        public AnalysisProcessor(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<List<string>> Get_Top_Emoji_For_User(int user_id , int top)
            => await _context.userEmojiUsage_Es.Where(ui => ui.UserId == user_id)
                .OrderBy(p => p.Count).Take(top).Select(p=>p.Emoji).ToListAsync();

        public async Task<List<int>> Get_Top_UserEmojiUsage(string emoji, int top)
            => await _context.userEmojiUsage_Es.Where(p => p.Emoji == emoji)
            .OrderBy(p => p.Count).Take(top).Select(p => p.UserId).ToListAsync();


        public async Task<List<string>> Get_SecondPage_Top_Emoji_For_User(int user_id, int pageSize)
        => await _context.userEmojiUsage_Es
            .Where(ui => ui.UserId == user_id)
            .OrderByDescending(p => p.Count)
            .Skip(pageSize)
            .Take(pageSize)
            .Select(p => p.Emoji)
            .ToListAsync();


        public async Task<List<int>> Get_SecondPage_Top_UserEmojiUsage(string emoji, int pageSize)
        => await _context.userEmojiUsage_Es
            .Where(p => p.Emoji == emoji)
            .OrderByDescending(p => p.Count)
            .Skip(pageSize)
            .Take(pageSize)
            .Select(p => p.UserId)
            .ToListAsync();

    }
}
