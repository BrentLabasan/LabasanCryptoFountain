using Microsoft.EntityFrameworkCore;

namespace TST_Fountain.Models
{
    public class TST_FountainContext : DbContext
    {
        public TST_FountainContext(DbContextOptions<TST_FountainContext> options)
            : base(options)
        {
        }

        public DbSet<TST_Fountain.Models.Account> Account { get; set; }
        public DbSet<TST_Fountain.Models.Send> Send { get; set; }

    }
}