using Microsoft.EntityFrameworkCore;
using Vloxy.SuperaRewards.Domain.models;

namespace Vloxy.SuperaRewards.Infrastructure.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ActivityReward> Activities { get; set; }
        public DbSet<TransactionHistory> Transactions { get; set; }
    }
}
