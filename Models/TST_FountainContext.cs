using Microsoft.EntityFrameworkCore;

namespace LabasanCryptoFountain.Models
{
    public class LabasanCryptoFountainContext : DbContext
    {
        public LabasanCryptoFountainContext(DbContextOptions<LabasanCryptoFountainContext> options)
            : base(options)
        {
        }

        public DbSet<LabasanCryptoFountain.Models.Account> Account { get; set; }
        public DbSet<LabasanCryptoFountain.Models.Send> Send { get; set; }

    }
}