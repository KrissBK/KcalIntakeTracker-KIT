using KcalIntakeTracker_KIT.Models;
using Microsoft.EntityFrameworkCore;

namespace KcalIntakeTracker_KIT.Data
{

    public class KITDbContext : DbContext
    {
        public KITDbContext(DbContextOptions<KITDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<DailyLog> DailyLogs { get; set; }
    }
}
