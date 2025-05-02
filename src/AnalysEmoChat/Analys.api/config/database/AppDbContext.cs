using Analys.api.model.user;
using Microsoft.EntityFrameworkCore;

namespace Analys.api.config.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User_E> user_Es { get; set; }
    }
}
