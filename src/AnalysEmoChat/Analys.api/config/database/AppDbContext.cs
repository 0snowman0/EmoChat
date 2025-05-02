using Analys.api.model.user;
using Microsoft.EntityFrameworkCore;

namespace Analys.api.config.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<UserEmojiUsage_E> userEmojiUsage_Es { get; set; }

    }
}
