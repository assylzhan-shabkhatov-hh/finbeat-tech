using FinBetApi.Infrastructure.DataAccess.SqlServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinBetApi.Infrastructure.DataAccess.SqlServer
{
    public class FinBetDbContext : DbContext
    {
        public FinBetDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
